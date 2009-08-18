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
        if (db == null) return null;
        return db.Orders.Where(r => r.ID == ID).FirstOrDefault();
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

    public static PackErr Add(int ID, string DIN, string productCode)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        //Check order
        Order r = OrderBLL.Get(ID);
        if (r == null) return PackErrEnum.NonExistOrder;

        if (r.Status == Order.StatusX.Done)
            return PackErrEnum.OrderClose;

        //Check Donation
        Donation d = DonationBLL.Get(DIN);
        if (d == null) return PackErrEnum.DataErr;

        if (d.TestResultStatus == Donation.TestResultStatusX.Non
            || d.TestResultStatus == Donation.TestResultStatusX.Positive
            || d.TestResultStatus == Donation.TestResultStatusX.PositiveLocked)
        {
            return PackErrEnum.Positive;
        }

        //Check Pack
        Pack p = PackBLL.Get(DIN, productCode);

        if (p == null) return PackErrEnum.NonExist;
        if (p.Status != Pack.StatusX.Product) return new PackErr("Không thể cấp phát. Túi máu: " + p.Status);

        PackOrder po = new PackOrder();
        po.OrderID = r.ID;
        po.PackID = p.ID;
        po.Status = PackOrder.StatusX.Order;

        db.PackOrders.InsertOnSubmit(po);
        
        db.Packs.Attach(p);
        p.Status = Pack.StatusX.Delivered;

        db.SubmitChanges();

        return PackErrEnum.Non;
    }

    public static void Remove(int packOrderID, string note)
    {
        //RedBloodDataContext db = new RedBloodDataContext();

        //PackOrder po = db.PackOrders.Where(r => r.ID == packOrderID).FirstOrDefault();

        //if (po == null
        //    || po.Pack == null
        //    || po.Order == null) return;

        //PackStatusHistory h;

        //if (po.Pack.ComponentID == (int)TestDef.Component.Full
        //    || po.Pack.ComponentID == (int)TestDef.Component.PlateletApheresis)
        //{
        //    h = PackBLL.ChangeStatus(po.Pack, Pack.StatusX.Collected, actor, "Remove from Order: " + po.OrderID.Value.ToString() + ". " + note);
        //}
        //else
        //{
        //    h = PackBLL.ChangeStatus(po.Pack, Pack.StatusX.Production, actor, "Remove from Order: " + po.OrderID.Value.ToString() + ". " + note);
        //}

        //db.PackStatusHistories.InsertOnSubmit(h);


        //po.Status = PackOrder.StatusX.Return;
        //po.Actor = RedBloodSystem.CurrentActor;
        //po.Note = note;

        //po.Pack.DeliverStatus = Pack.DeliverStatusX.Non;

        ////db.PackOrders.DeleteOnSubmit(po);

        //db.SubmitChanges();

        //PackErr err = PackBLL.ValidateAndUpdateStatus(db, po.Pack);
        //db.SubmitChanges();
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
        //RedBloodDataContext db = new RedBloodDataContext();

        ////List<Guid> geoIDL = GeoBLL.Get(IDList, 1).Select(r => r.ID).ToList();
        ////if (geoIDL.Count == 0) return new List<Campaign>();

        //return db.Orders.Where(r =>
        //    r.Type == type
        //    && r.Date != null
        //    && (from == null || r.Date.Value.Date >= from.Value.Date)
        //    && (to == null || r.Date.Value.Date <= to.Value.Date)
        //    ).ToList();
        return null;

    }
}
