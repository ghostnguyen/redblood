using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReceiptBLL
/// </summary>
public class ReceiptBLL
{
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
}
