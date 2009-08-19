using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PackTransactionBLL
/// </summary>
public class PackTransactionBLL
{
    public PackTransactionBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static PackTransaction Add(RedBloodDataContext db, Guid packID, PackTransaction.TypeX type, string note)
    {
        PackTransaction e = new PackTransaction();
        e.PackID = packID;
        e.Type = type;
        e.Date = DateTime.Now;
        e.Note = note.Trim();

        db.PackTransactions.InsertOnSubmit(e);

        return e;
    }

    public static PackTransaction Add(Guid packID, PackTransaction.TypeX type, string note)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        PackTransaction e = Add(db, packID, type, note);

        db.SubmitChanges();

        return e;
    }

}
