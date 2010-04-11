using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Reflection;

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

    public static List<PackTransaction.TypeX> InTypeList = new List<PackTransaction.TypeX>() { PackTransaction.TypeX.In_Collect, PackTransaction.TypeX.In_Product, PackTransaction.TypeX.In_Return };
    public static List<PackTransaction.TypeX> OutTypeList = new List<PackTransaction.TypeX>() { PackTransaction.TypeX.Out_Delete, PackTransaction.TypeX.Out_Product, PackTransaction.TypeX.Out_Order };

    public static PackTransaction Add(Guid packID, PackTransaction.TypeX type, string note)
    {
        RedBloodSystemBLL.SOD();

        RedBloodDataContext db = new RedBloodDataContext();

        PackTransaction e = new PackTransaction();
        e.PackID = packID;
        e.Type = type;
        e.Note = note.Trim();

        db.PackTransactions.InsertOnSubmit(e);

        db.SubmitChanges();

        return e;
    }

    public static PackTransaction Add(Guid packID, PackTransaction.TypeX type)
    {
        return Add(packID, type, MyMethodBase.Current.Caller.Name);
    }
}
