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

    public static PackErr Add(int ID, int autonum, string actor)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        //Check order
        Order r = OrderBLL.Get(ID);
        if (r == null) return PackErrList.NonExistOrder;

        if (r.Status == Order.StatusX.Done)
            return PackErrList.OrderClose;

        //Check Pack
        Pack p = PackBLL.Get(autonum, db);

        if (p == null
            || p.DeliverStatus != Pack.DeliverStatusX.Non)
            return PackErrList.NonExist;

        PackErr err = PackBLL.ValidateAndUpdateStatus(db, p, "Order");

        if (!PackBLL.StatusList4Order().Contains(p.Status))
            return new PackErr("Không thể cấp phát. Túi máu: " + p.Status);

        //if (p.PackOrders.Count >= 1) return PackErrList.DataErr;

        if (p.TestResultStatus == Pack.TestResultStatusX.Positive
            || p.TestResultStatus == Pack.TestResultStatusX.PositiveLocked
            || p.TestResultStatus == Pack.TestResultStatusX.Non)
        {
            return new PackErr("Không thể cấp phát. KQXN: " + p.TestResultStatus);
        }
        else
        {
            p.DeliverStatus = Pack.DeliverStatusX.Yes;

            if (p.TestResultStatus == Pack.TestResultStatusX.Negative)
            {
                List<Pack> l = PackBLL.GetSourcePacks_AllLevel(p)
                    .Where(rp => rp.ComponentID == TestDef.Component.Full).ToList();
                foreach (Pack item in l)
                {
                    if (item.TestResultStatus == Pack.TestResultStatusX.Negative)
                    {
                        PackBLL.UpdateTestResultStatus4Extracts(db, item);
                    }
                }

                //p.TestResultStatus = Pack.TestResultStatusX.NegativeLocked;
            }

            PackOrder po = new PackOrder();
            po.OrderID = r.ID;
            po.PackID = p.ID;
            po.Status = PackOrder.StatusX.Order;

            db.PackOrders.InsertOnSubmit(po);

            //PackStatusHistory h = PackBLL.ChangeStatus(p, Pack.StatusX.Delivered, actor, "Add to Order: " + po.OrderID.Value.ToString());
            //db.PackStatusHistories.InsertOnSubmit(h);
        }

        db.SubmitChanges();

        return err;
    }

    public static void Remove(int packOrderID, string actor, string note)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        PackOrder po = db.PackOrders.Where(r => r.ID == packOrderID).FirstOrDefault();

        if (po == null
            || po.Pack == null
            || po.Order == null) return;

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


        po.Status = PackOrder.StatusX.Return;
        po.Actor = actor;
        po.Note = note;

        po.Pack.DeliverStatus = Pack.DeliverStatusX.Non;

        //db.PackOrders.DeleteOnSubmit(po);

        db.SubmitChanges();

        PackErr err = PackBLL.ValidateAndUpdateStatus(db, po.Pack, actor);
        db.SubmitChanges();
    }

    public static void CloseOrder(RedBloodDataContext db)
    {
        List<Order> r = db.Orders.Where(e => e.Status == Order.StatusX.Init).ToList();

        foreach (Order item in r)
        {
            TimeSpan tsp = DateTime.Now.Date - item.Date.Value.Date;
            if (tsp.Days > 0)
                item.Status = Order.StatusX.Done;
        }
    }
}
