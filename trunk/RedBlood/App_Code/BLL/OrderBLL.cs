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

        Order r = OrderBLL.Get(ID);
        if (r == null) return PackErrList.NonExistOrder;

        if (r.Status == Order.StatusX.Done)
            return PackErrList.OrderClose;

        Pack p = PackBLL.Get(autonum, db);

        if (p == null) return PackErrList.NonExist;

        if (p.ComponentID == (int)TestDef.Component.Full
            && p.PackExtractsBySource.Count > 0)
            return PackErrList.Extracted;

        if (!PackBLL.StatusList4Order().Contains(p.Status))
            return new PackErr("Không thể cấp phát. Túi máu: " + p.Status);

        int i = p.PackOrders.Count;

        if (i > 1) return PackErrList.DataErr;

        if (i == 1)
        {
            if (p.PackOrders.First().OrderID.Value != ID)
                return PackErrList.Dilivered;
            else
                return PackErrList.Non;
        }

        PackErr err = PackErrList.Non;
        if (i == 0)
        {
            err = PackBLL.ValidateAndChangeStatus(db, p, "Order");

            if (p.Status == Pack.StatusX.Expire)
            {
                return PackErrList.Expired;
            }
            else if (p.Status == Pack.StatusX.Delete)
            {
                return PackErrList.Deleted;
            }
            else if (p.Status == Pack.StatusX.EnterTestResult
                || p.Status == Pack.StatusX.Assign)
            {
                return PackErrList.CanNotOrder;
            }
            else if (p.Status == Pack.StatusX.CommitTestResult)
            {
                if (PackBLL.ValidateTestResult(p.TestResult2).Count() != 0)
                    return PackErrList.Positive;
            }
            else if (p.Status == Pack.StatusX.Production)
            {
                int count = 0;

                try
                {
                    count = PackBLL.ValidateTestResult(p).Count();
                }
                catch (Exception ex)
                {
                    return new PackErr(ex.Message);
                }

                if (count != 0)
                    return PackErrList.Positive;
            }

            PackOrder po = new PackOrder();
            po.OrderID = r.ID;
            po.PackID = p.ID;

            db.PackOrders.InsertOnSubmit(po);

            PackStatusHistory h = PackBLL.ChangeStatus(p, Pack.StatusX.Delivered, actor, "Add to Order: " + po.OrderID.Value.ToString());
            db.PackStatusHistories.InsertOnSubmit(h);
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

        PackStatusHistory h;

        if (po.Pack.ComponentID == (int)TestDef.Component.Full)
        {
            h = PackBLL.ChangeStatus(po.Pack, Pack.StatusX.CommitTestResult, actor, "Remove from Order: " + po.OrderID.Value.ToString() + ". " + note);
        }
        else
        {
            h = PackBLL.ChangeStatus(po.Pack, Pack.StatusX.Production, actor, "Remove from Order: " + po.OrderID.Value.ToString() + ". " + note);
        }

        db.PackStatusHistories.InsertOnSubmit(h);

        db.PackOrders.DeleteOnSubmit(po);

        db.SubmitChanges();

        PackErr err = PackBLL.ValidateAndChangeStatus(db, po.Pack, actor);
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
