using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedBlood;
/// <summary>
/// Summary description for OrderBLL
/// </summary>
public class PackOrderBLL
{
    public PackOrderBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Add(int orderID, string DIN, string productCode)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Order r = OrderBLL.Get4Add(orderID);
        Pack p = PackBLL.Get4Order(DIN, productCode);

        PackOrder po = new PackOrder();
        po.OrderID = r.ID;
        po.PackID = p.ID;

        db.PackOrders.InsertOnSubmit(po);
        db.SubmitChanges();

        PackTransaction.TypeX transType = r.Type == Order.TypeX.ForCR ? PackTransaction.TypeX.Out_Order4CR
            : r.Type == Order.TypeX.ForOrg ? PackTransaction.TypeX.Out_Order4Org
            : PackTransaction.TypeX.Out_OrderGen;

        PackBLL.ChangeStatus(p.ID, Pack.StatusX.Delivered, transType, "PackOrderID = " + po.ID.ToString());
    }

    public static void Return(int returnID, int packOrderID, string note)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        PackOrder po = Get4Return(db, packOrderID);
        po.ReturnID = returnID;

        db.SubmitChanges();

        PackBLL.ChangeStatus(po.Pack.ID, Pack.StatusX.Product, PackTransaction.TypeX.In_Return, note);
    }

    public static PackOrder Get(RedBloodDataContext db, int ID)
    {
        PackOrder r = db.PackOrders.Where(r1 => r1.ID == ID).FirstOrDefault();

        if (r == null)
            throw new Exception("Chưa có cấp phát túi máu này.");

        return r;
    }

    public static List<PackOrder> Get4Return(RedBloodDataContext db, List<int> IDList)
    {
        List<PackOrder> l = IDList.Select(r => Get4Return(db, r)).ToList();

        return l;
    }

    public static PackOrder Get4Return(RedBloodDataContext db, int ID)
    {
        PackOrder r = Get(db, ID);

        if (r.ReturnID > 0)
            throw new Exception("Đã thu hồi túi máu này.");

        return r;
    }

    public static PackOrder Get4Return(string DIN, string productCode)
    {
        Pack p = PackBLL.Get(DIN, productCode);

        var v = p.PackOrders.Where(r => !r.ReturnID.HasValue);

        if (v.Count() > 1)
            throw new Exception("Sai dữ liệu. Túi máu cấp phát 2 lần.");

        if (v.Count() == 0)
            throw new Exception("Chưa có cấp phát túi máu này.");

        return v.FirstOrDefault();
    }
}
