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
        if (!ProductCodeInList.IsValidProductCodeList())
            return "Danh sách sản phẩm đầu vào có lỗi.";

        if (!ProductCodeOutList.IsValidProductCodeList())
            return "Danh sách sản phẩm đầu ra có lỗi.";

        if (!ProductCodeOutList.IsValidDINList())
            return "Danh sách sản phẩm đầu ra có lỗi.";

        if (ProductCodeInList.Where(r => ProductCodeOutList.Contains(r)).Count() != 0)
        {
            return "Danh sách sản phẩm đầu ra và đầu vào có sản phẩm trùng.";
        }

        //TODO: Validate data in database

        return "";
    }

    public List<string> AddProductCodeIn(string productCode)
    {
        if (!productCode.IsValidProductCode())
            throw new Exception("Sai mã sản phẩm.");

        if (ProductCodeInList.Contains(productCode))
            throw new Exception("Sản phẩm đầu vào đã có trong danh sách đầu vào.");

        if (ProductCodeOutList.Contains(productCode))
            throw new Exception("Sản phẩm đầu vào đã có trong danh sách đầu ra.");

        //TODO: Only some product is allow as IN

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
        if (!productCode.IsValidProductCode())
            throw new Exception("Sai mã sản phẩm.");

        if (ProductCodeInList.Contains(productCode))
            throw new Exception("Sản phẩm đầu ra đã có trong danh sách đầu vào.");

        if (ProductCodeOutList.Contains(productCode))
            throw new Exception("Sản phẩm đầu ra đã có trong danh sách đầu ra.");

        //TODO: Only some product is allow as OUT

        RedBloodDataContext db = new RedBloodDataContext();
        int count = db.Packs.Where(r => r.ProductCode == productCode && DINInList.Contains(r.DIN)).Count();
        if (count > 0)
            throw new Exception("Sản phẩm đầu ra đã sản xuất.");

        ProductCodeOutList.Add(productCode);

        return ProductCodeInList;
    }

    public List<string> AddDIN(string DIN)
    {
        if (!DIN.IsValidDINCode())
            throw new Exception("Không phải mã túi máu.");

        if (DINInList.Contains(DIN))
            throw new Exception("Mã túi máu này đã có.");

        Donation d = DonationBLL.Get(DIN);
        
        if (d == null)
            throw new Exception("Không có mã túi máu này.");

        if (d.TestResultStatus == Donation.TestResultStatusX.Positive
            || d.TestResultStatus == Donation.TestResultStatusX.PositiveLocked)
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

    
}
