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

    public static PackErr Order(int ID, int autonum)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Order r = OrderBLL.Get(ID);
        if (r == null) return PackErrList.NonExistOrder;

        Pack p = PackBLL.Get(autonum, db);

        if (p == null) return PackErrList.NonExist;

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
            err = PackBLL.ValidateAndChangeStatus(db,p,"Order");

            if (p.Status == Pack.StatusX.Delete)
            {
                return PackErrList.Deleted;
            }
            else if (p.Status == Pack.StatusX.EnterTestResult)
            {
                return PackErrList.CanNotOrder;
            }
            else if (p.Status == Pack.StatusX.CommitTestResult)
            {
                PackOrder po = new PackOrder();
                po.OrderID = r.ID;
                po.PackID = p.ID;

                db.PackOrders.InsertOnSubmit(po);
            }
        }

        db.SubmitChanges();

        return err;
    }
}
