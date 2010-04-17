using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderBLL
/// </summary>
public class OrderBLL
{
    public OrderBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Order Get(int ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(ID, db);
    }

    public static Order Get(int ID, RedBloodDataContext db)
    {
        Order e = db.Orders.Where(r => r.ID == ID).FirstOrDefault();

        if (e == null)
            throw new Exception("Không tìm thấy đợt cấp phát.");

        return e;
    }

    public static Order Get4Add(int ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Order r = Get(ID, db);

        if (r.Status == Order.StatusX.Done)
            throw new Exception("Đợt cấp phát này đã kết thúc.");

        return r;
    }

    //public static void Add(int ID, string DIN, string productCode)
    //{
    //    Order r = OrderBLL.Get(ID);

    //    if (r.Status == Order.StatusX.Done)
    //        throw new Exception("Đợt cấp phát này đã kết thúc.");

    //    Pack p = PackBLL.Get4Order(DIN, productCode);

    //    PackOrder po = new PackOrder();
    //    po.OrderID = r.ID;
    //    po.PackID = p.ID;
    //    po.Status = PackOrder.StatusX.Order;

    //    RedBloodDataContext db = new RedBloodDataContext();

    //    db.PackOrders.InsertOnSubmit(po);

    //    db.SubmitChanges();

    //    string fullNote = "Add to Order: " + po.OrderID.Value.ToString() + ".";

    //    PackBLL.Update(db, po.Pack, Pack.StatusX.Delivered, fullNote);

    //    PackTransaction.TypeX transType = r.Type == Order.TypeX.ForCR ? PackTransaction.TypeX.Out_Order4CR
    //        : r.Type == Order.TypeX.ForOrg ? PackTransaction.TypeX.Out_Order4Org
    //        : PackTransaction.TypeX.Out_OrderGen;

    //    PackTransactionBLL.Add(p.ID,
    //        transType,
    //        fullNote);
    //}

    //public static void Remove(int packOrderID, string note)
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    PackOrder po = db.PackOrders.Where(r => r.ID == packOrderID).FirstOrDefault();

    //    if (po == null
    //        || po.Pack == null
    //        || po.Order == null) return;

    //    string fullNote = DateTime.Now.ToStringVNLong() + ". " + RedBloodSystem.CurrentActor + ". Remove from Order: " + po.OrderID.Value.ToString() + ". " + note;

    //    PackBLL.Update(db, po.Pack, Pack.StatusX.Product, fullNote);

    //    po.Status = PackOrder.StatusX.Return;
    //    po.Note = fullNote;

    //    db.SubmitChanges();

    //    PackTransactionBLL.Add(po.Pack.ID, PackTransaction.TypeX.In_Return, fullNote);

    //}

    public static void CloseOrder()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var v = db.Orders.Where(r => r.Status == Order.StatusX.Init
            && r.Date.Value.Date < DateTime.Now.Date).ToList();

        foreach (var item in v)
        {
            item.Status = Order.StatusX.Done;
        }

        db.SubmitChanges();

        LogBLL.Logs();
    }

    public static List<Order> Get(DateTime? from, DateTime? to, Order.TypeX type)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return db.Orders.Where(r =>
            r.Type == type
            && r.Date != null
            && (from == null || r.Date.Value.Date >= from.Value.Date)
            && (to == null || r.Date.Value.Date <= to.Value.Date)
            ).ToList();
    }

    //public static Donation GetDIN4Order(string DIN)
    //{
    //    Donation d = DonationBLL.Get(DIN);

    //    if (d.TestResultStatus != Donation.TestResultStatusX.Negative)
    //    {
    //        throw new Exception("Không thể cấp phát túi máu này. KQ xét nghiệm sàng lọc: " + d.TestResultStatus);
    //    }

    //    return d;
    //}

    

   
}
