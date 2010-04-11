using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductionBLL
/// </summary>
public class ProductionBLL
{
    public List<string> ProductCodeInList { get; set; }
    public List<string> ProductCodeOutList { get; set; }
    public List<string> DINInList { get; set; }

    public ProductionBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string ValidateAllList()
    {
        if (ProductCodeInList.Count == 0)
        {
            return "Không có sản phẩm đầu vào.";
        }

        if (ProductCodeOutList.Count == 0)
        {
            return "Không có sản phẩm đầu ra.";
        }

        if (DINInList.Count == 0)
        {
            return "Không có túi máu đầu vào.";
        }

        ReceiptBLL.ValidateOnTherapyReceipt(ProductCodeInList, ProductCodeOutList);

        if (ProductCodeInList.Where(r => ProductCodeOutList.Contains(r)).Count() != 0)
        {
            return "Danh sách sản phẩm đầu ra và đầu vào có sản phẩm trùng.";
        }

        return "";
    }



    public List<string> AddProductCodeIn(string productCode)
    {
        if (ProductCodeInList.Contains(productCode))
            throw new Exception("Sản phẩm đầu vào đã có trong danh sách đầu vào.");

        if (ProductCodeOutList.Contains(productCode))
            throw new Exception("Sản phẩm đầu vào đã có trong danh sách đầu ra.");

        if (ProductCodeInList.Count == 1)
            throw new Exception("Sản phẩm đầu vào chỉ được 1 loại.");

        List<string> tempList = ProductCodeInList.ToList();
        tempList.Add(productCode);
        ReceiptBLL.ValidateOnTherapyReceipt(tempList, ProductCodeOutList);

        RedBloodDataContext db = new RedBloodDataContext();
        int count = db.Packs.Where(r => r.ProductCode == productCode && DINInList.Contains(r.DIN)).Count();

        if (count < DINInList.Count)
            throw new Exception("Sản phẩm đầu vào không có túi máu.");

        if (count > DINInList.Count)
            throw new Exception("Sản phẩm đầu vào có túi máu bị trùng dữ liệu.");


        ProductCodeInList.Add(productCode);

        return ProductCodeInList;
    }

    public List<string> AddProductCodeOut(string productCode)
    {
        if (ProductCodeInList.Contains(productCode))
            throw new Exception("Sản phẩm đầu ra đã có trong danh sách đầu vào.");

        if (ProductCodeOutList.Contains(productCode))
            throw new Exception("Sản phẩm đầu ra đã có trong danh sách đầu ra.");

        List<string> tempList = ProductCodeOutList.ToList();
        tempList.Add(productCode);
        ReceiptBLL.ValidateOnTherapyReceipt(ProductCodeInList, tempList);

        RedBloodDataContext db = new RedBloodDataContext();
        int count = db.Packs.Where(r => r.ProductCode == productCode && DINInList.Contains(r.DIN)).Count();
        if (count > 0)
            throw new Exception("Sản phẩm đầu ra đã sản xuất.");

        ProductCodeOutList.Add(productCode);

        return ProductCodeOutList;
    }

    public List<string> AddDIN(string DIN)
    {
        if (DINInList.Contains(DIN))
            throw new Exception("Mã túi máu này đã có.");

        Donation d = DonationBLL.Get(DIN);

        if (d == null)
            throw new Exception("Không có mã túi máu này.");

        if (d.TestResultStatus == Donation.TestResultStatusX.Positive)
        {
            throw new Exception("Xét nghiệm sàng lọc: Dương tính.");
        }

        RedBloodDataContext db = new RedBloodDataContext();
        int count = db.Packs.Where(r => ProductCodeInList.Contains(r.ProductCode) && r.DIN == DIN).Count();
        if (count == 0)
            throw new Exception("Mã túi máu này không có sản phẩm đầu vào.");

        count = db.Packs.Where(r => ProductCodeOutList.Contains(r.ProductCode) && r.DIN == DIN).Count();
        if (count > 0)
            throw new Exception("Mã túi máu này đã có sản phẩm đầu ra.");

        DINInList.Add(DIN);

        return DINInList;
    }

    public void Extract()
    {
        string err = ValidateAllList();

        if (!string.IsNullOrEmpty(err))
            throw new Exception(err);

        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack> packList = db.Packs.Where(r => DINInList.Contains(r.DIN) && ProductCodeInList.Contains(r.ProductCode)).ToList();

        foreach (Pack item in packList)
        {
            foreach (string code in ProductCodeOutList)
            {
                //TODO: display all err pack.
                Extract(item.ID, code);
            }
        }
    }

    public PackErr Extract(Guid srcPackID, string productCode)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack pack = db.Packs.Where(r => r.ID == srcPackID).FirstOrDefault();
        Product p = db.Products.Where(r => r.Code == productCode).FirstOrDefault();

        //Validate
        if (pack == null || p == null) return PackErrEnum.DataErr;

        if (pack.Donation.TestResultStatus == Donation.TestResultStatusX.Positive)
        {
            return PackErrEnum.Positive;
        }

        if (db.Packs.Where(r => r.DIN == pack.DIN && r.ProductCode == productCode).FirstOrDefault() != null)
            return PackErrEnum.Existed;

        //TODO: Check to see if the pack is collector too late
        //Code check will be here.

        //Create new
        Pack toPack = new Pack();

        toPack.DIN = pack.DIN;
        toPack.ProductCode = productCode;
        toPack.Status = Pack.StatusX.Product;
        toPack.Date = DateTime.Now;
        toPack.Actor = RedBloodSystem.CurrentActor;
        //toPack.Volume = p.OriginalVolume;
        toPack.ExpirationDate = DateTime.Now.Add(p.Duration.Value - RedBloodSystem.RootTime);

        db.Packs.InsertOnSubmit(toPack);
        db.SubmitChanges();

        PackTransactionBLL.Add(toPack.ID, PackTransaction.TypeX.In_Product);

        //Update fromPack
        PackStatusHistory h = PackBLL.Update(db, pack, Pack.StatusX.Produced, "");
        if (h != null)
        {
            db.SubmitChanges();
            PackTransactionBLL.Add(pack.ID, PackTransaction.TypeX.Out_Product);
        }

        return PackErrEnum.Non;
    }
}

