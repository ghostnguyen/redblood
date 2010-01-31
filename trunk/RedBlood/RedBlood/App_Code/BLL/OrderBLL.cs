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

    //public static PackErr Add(int ID, int autonum)
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    //Check order
    //    Order r = OrderBLL.Get(ID);
    //    if (r == null) return PackErrEnum.NonExistOrder;

    //    if (r.Status == Order.StatusX.Done)
    //        return PackErrEnum.OrderClose;

    //    //Check Pack
    //    Pack p = PackBLL.Get(db, autonum);

    //    //if (p == null
    //    //    || p.DeliverStatus != Pack.DeliverStatusX.Non)
    //    //    return PackErrEnum.NonExist;

    //    //PackErr err = PackBLL.ValidateAndUpdateStatus(db, p);

    //    //if (!PackBLL.StatusList4Order().Contains(p.Status))
    //    //    return new PackErr("Không thể cấp phát. Túi máu: " + p.Status);


    //    //if (p.TestResultStatus == Pack.TestResultStatusX.Positive
    //    //    || p.TestResultStatus == Pack.TestResultStatusX.PositiveLocked
    //    //    || p.TestResultStatus == Pack.TestResultStatusX.Non)
    //    //{
    //    //    return new PackErr("Không thể cấp phát. KQXN: " + p.TestResultStatus);
    //    //}
    //    //else
    //    //{
    //    //    p.DeliverStatus = Pack.DeliverStatusX.Yes;

    //    //    if (p.TestResultStatus == Pack.TestResultStatusX.Negative)
    //    //    {
    //    //        List<Pack> l = p.SourcePacks_All
    //    //            .Where(rp => rp.ComponentID == TestDef.Component.Full).ToList();

    //    //        foreach (Pack item in l)
    //    //        {
    //    //            if (item.TestResultStatus == Pack.TestResultStatusX.Negative)
    //    //            {
    //    //                item.TestResultStatus = Pack.TestResultStatusX.NegativeLocked;
    //    //                PackBLL.UpdateTestResultStatus4Extracts(db, item);
    //    //            }
    //    //        }

    //    //    }

    //    //    PackOrder po = new PackOrder();
    //    //    po.OrderID = r.ID;
    //    //    po.PackID = p.ID;
    //    //    po.Status = PackOrder.StatusX.Order;

    //    //    db.PackOrders.InsertOnSubmit(po);

    //    //}

    //    db.SubmitChanges();

    //    //return err;
    //    return null;
    //}

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

        p.Status = Pack.StatusX.Delivered;

        db.SubmitChanges();

        PackTransactionBLL.Add(p.ID, PackTransaction.TypeX.Out_Order);
    }

    public static void Remove(int packOrderID, string note)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        PackOrder po = db.PackOrders.Where(r => r.ID == packOrderID).FirstOrDefault();

        if (po == null
            || po.Pack == null
            || po.Order == null) return;

        PackBLL.Update(db, po.Pack, Pack.StatusX.Product, "Remove from Order: " + po.OrderID.Value.ToString() + ". " + note);

        po.Status = PackOrder.StatusX.Return;
        po.Actor = RedBloodSystem.CurrentActor;
        po.ReturnDate = DateTime.Now;
        po.Note = note;

        db.SubmitChanges();

        PackTransactionBLL.Add(po.Pack.ID, PackTransaction.TypeX.In_Return);

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
            throw new Exception("Túi máu " + e.TestResultStatus);
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
