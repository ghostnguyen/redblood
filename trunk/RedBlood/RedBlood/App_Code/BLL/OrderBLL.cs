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
        if (db == null)
            throw new Exception("RedBloodDataContext Null.");

        Order e = db.Orders.Where(r => r.ID == ID).FirstOrDefault();

        if (e == null)
            throw new Exception("Không tìm thấy đợt cấp phát.");

        return e;
    }

    public static void Add(int ID, string DIN, string productCode)
    {
        Order r = OrderBLL.Get(ID);

        if (r.Status == Order.StatusX.Done)
            throw new Exception("Đợt cấp phát này đã kết thúc.");

        Pack p = GetPack4Order(DIN, productCode);

        PackOrder po = new PackOrder();
        po.OrderID = r.ID;
        po.PackID = p.ID;
        po.Status = PackOrder.StatusX.Order;

        RedBloodDataContext db = new RedBloodDataContext();

        db.PackOrders.InsertOnSubmit(po);

        db.SubmitChanges();

        string fullNote = "Add to Order: " + po.OrderID.Value.ToString() + ".";

        PackBLL.Update(db, po.Pack, Pack.StatusX.Delivered, fullNote);

        PackTransactionBLL.Add(p.ID, PackTransaction.TypeX.Out_Order, fullNote);
    }

    public static void Remove(int packOrderID, string note)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        PackOrder po = db.PackOrders.Where(r => r.ID == packOrderID).FirstOrDefault();

        if (po == null
            || po.Pack == null
            || po.Order == null) return;

        string fullNote = DateTime.Now.ToStringVNLong() + ". " + RedBloodSystem.CurrentActor + ". Remove from Order: " + po.OrderID.Value.ToString() + ". " + note;

        PackBLL.Update(db, po.Pack, Pack.StatusX.Product, fullNote);

        po.Status = PackOrder.StatusX.Return;
        po.Note = fullNote;

        db.SubmitChanges();

        PackTransactionBLL.Add(po.Pack.ID, PackTransaction.TypeX.In_Return, fullNote);

    }

    public static void CloseOrder(RedBloodDataContext db)
    {
        List<Order> r = db.Orders.Where(e => e.Status == Order.StatusX.Init).ToList();

        foreach (Order item in r)
        {
            if (DateTime.Now.Date > item.Date.Value.Date)
                item.Status = Order.StatusX.Done;
        }
    }

    public static List<Order> Get(DateTime? from, DateTime? to, Order.TypeX type)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        //List<Guid> geoIDL = GeoBLL.Get(IDList, 1).Select(r => r.ID).ToList();
        //if (geoIDL.Count == 0) return new List<Campaign>();

        return db.Orders.Where(r =>
            r.Type == type
            && r.Date != null
            && (from == null || r.Date.Value.Date >= from.Value.Date)
            && (to == null || r.Date.Value.Date <= to.Value.Date)
            ).ToList();
    }

    public static Donation GetDIN4Order(string DIN)
    {
        Donation e = DonationBLL.Get(DIN);
        if (e == null)
            throw new Exception("Không tìm thấy mã túi máu.");

        if (e.TestResultStatus == Donation.TestResultStatusX.Negative
            || e.TestResultStatus == Donation.TestResultStatusX.NegativeLocked)
        { }
        else
        {
            throw new Exception("Không thể cấp phát túi máu này. KQ xét nghiệm sàng lọc: " + e.TestResultStatus);
        }

        return e;
    }

    public static Pack GetPack4Order(string DIN, string productCode)
    {
        Donation d = GetDIN4Order(DIN);

        Pack p = d.Packs.Where(r => r.ProductCode == productCode).FirstOrDefault();

        if (p == null)
            throw new Exception("Không tìm thấy túi máu.");

        if (p.Status != Pack.StatusX.Product)
            throw new Exception("Không thể cấp phát. Túi máu: " + p.Status);

        return p;
    }
}
