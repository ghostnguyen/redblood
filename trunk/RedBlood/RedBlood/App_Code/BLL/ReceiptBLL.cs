using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReceiptBLL
/// </summary>
public class ReceiptBLL
{
    public List<string> ProductCodeInList { get; set; }
    public List<string> ProductCodeOutList { get; set; }

    public ReceiptBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<Receipt> Find(string findStr)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        if (string.IsNullOrEmpty(findStr)
            || string.IsNullOrEmpty(findStr.Trim()))
        {
            return db.Receipts.ToList();
        }
        else
        {
            return db.Receipts.Where(r => r.Name.Contains(findStr.Trim())).ToList();
        }
    }

    public Guid InsertOrUpdate(Guid ID, Func<Receipt, Receipt> loadFromGUI)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Receipt r;

        if (ID == Guid.Empty)
        {
            r = new Receipt();
            db.Receipts.InsertOnSubmit(r);
        }
        else
        {
            r = ReceiptBLL.Get(ID, db);
        }

        loadFromGUI(r);

        //Product In
        IEnumerable<ReceiptProduct> existingProductCodeInList = r.ReceiptProducts.Where(r1 => r1.Type == ReceiptProduct.TypeX.In);

        db.ReceiptProducts.DeleteAllOnSubmit(
            existingProductCodeInList.Where(r1 => !ProductCodeInList.Contains(r1.ProductCode))
            );

        r.ReceiptProducts.AddRange(
            ProductCodeInList
                .Except(existingProductCodeInList.Select(r1 => r1.ProductCode))
                .Select(r1 => new ReceiptProduct() { ProductCode = r1, Type = ReceiptProduct.TypeX.In })
            );

        //Product Out
        IEnumerable<ReceiptProduct> existingProductCodeOutList = r.ReceiptProducts.Where(r1 => r1.Type == ReceiptProduct.TypeX.Out);

        db.ReceiptProducts.DeleteAllOnSubmit(
            existingProductCodeOutList.Where(r1 => !ProductCodeOutList.Contains(r1.ProductCode))
            );

        r.ReceiptProducts.AddRange(
            ProductCodeOutList
                .Except(existingProductCodeOutList.Select(r1 => r1.ProductCode))
                .Select(r1 => new ReceiptProduct() { Product = ProductBLL.Get(db, r1), Type = ReceiptProduct.TypeX.Out })
            );

        db.SubmitChanges();
        return r.ID;
    }

    public static Receipt Get(Guid ID, RedBloodDataContext db)
    {
        return db.Receipts.Where(r => r.ID == ID).FirstOrDefault();
    }

    public static Receipt Get(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return Get(ID, db);
    }

    public static bool IsExistName(string name, Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        int count = (from r in db.Receipts
                     where r.ID != ID && r.Name.Trim() == name.Trim()
                     select r).Count();

        return count != 0;
    }

    public static void Delete(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Receipt e = Get(ID, db);

        db.Receipts.DeleteOnSubmit(e);
        db.SubmitChanges();
    }

    public List<string> AddProductCodeIn(string productCode)
    {
        if (ProductCodeInList.Contains(productCode))
            throw new Exception("Sản phẩm đầu vào đã có trong danh sách đầu vào.");

        if (ProductCodeOutList.Contains(productCode))
            throw new Exception("Sản phẩm đầu vào đã có trong danh sách đầu ra.");

        if (ProductCodeInList.Count == 1)
            throw new Exception("Sản phẩm đầu vào chỉ được 1 loại.");

        ProductCodeInList.Add(productCode);

        return ProductCodeInList;
    }

    public List<string> AddProductCodeOut(string productCode)
    {
        if (ProductCodeInList.Contains(productCode))
            throw new Exception("Sản phẩm đầu ra đã có trong danh sách đầu vào.");

        if (ProductCodeOutList.Contains(productCode))
            throw new Exception("Sản phẩm đầu ra đã có trong danh sách đầu ra.");

        ProductCodeOutList.Add(productCode);

        return ProductCodeOutList;
    }

    public static bool ValidateOnTherapyReceipt(List<string> productCodeInList, List<string> productCodeOutList)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        List<Receipt> all = db.Receipts.ToList();

        if (all.Where(r =>
            productCodeInList
                .Except(
                    r.ReceiptProducts.Where(r1 => r1.Type == ReceiptProduct.TypeX.In).Select(r1 => r1.ProductCode))
                .Count() == 0
            && productCodeOutList
                .Except(
                    r.ReceiptProducts.Where(r1 => r1.Type == ReceiptProduct.TypeX.Out).Select(r1 => r1.ProductCode))
                .Count() == 0
            ).Count() == 0)
            throw new Exception("Không có công thức cho sản xuất chế phẩm thích hợp.");
        
        return true;
    }
}
