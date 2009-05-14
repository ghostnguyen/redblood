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
}
