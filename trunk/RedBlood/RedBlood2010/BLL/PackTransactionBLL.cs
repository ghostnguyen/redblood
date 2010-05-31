using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Reflection;

namespace RedBlood.BLL
{
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
        public static List<PackTransaction.TypeX> OutTypeList = new List<PackTransaction.TypeX>() { 
        PackTransaction.TypeX.Out_Delete, 
        PackTransaction.TypeX.Out_Product, 
        PackTransaction.TypeX.Out_OrderGen,
        PackTransaction.TypeX.Out_Order4CR,
        PackTransaction.TypeX.Out_Order4Org
    };

        public static List<PackTransaction.TypeX> OutOrderTypeList = new List<PackTransaction.TypeX>() { 
        PackTransaction.TypeX.Out_OrderGen,
        PackTransaction.TypeX.Out_Order4CR,
        PackTransaction.TypeX.Out_Order4Org
    };

        public static PackTransaction Add(Guid packID, Pack.StatusX fromStatus, Pack.StatusX toStatus, PackTransaction.TypeX type, string note)
        {
            //Always run SOD before new transaction
            RedBloodSystemBLL.SOD();

            RedBloodDataContext db = new RedBloodDataContext();

            if (fromStatus == toStatus)
                throw new Exception("Can not add pack transaction if not change pack status.");

            PackTransaction e = new PackTransaction();
            e.FromStatus = fromStatus;
            e.ToStatus = toStatus;
            e.PackID = packID;
            e.Type = type;
            e.Actor = RedBloodSystem.CurrentActor;
            e.Note = note.Trim();

            db.PackTransactions.InsertOnSubmit(e);

            db.SubmitChanges();

            return e;
        }

        public static PackTransaction Add(Guid packID, Pack.StatusX fromStatus, Pack.StatusX toStatus, PackTransaction.TypeX type)
        {
            return Add(packID, fromStatus, toStatus, type, MyMethodBase.Current.Caller.Name);
        }
    }
}