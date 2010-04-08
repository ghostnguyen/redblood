using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SexBLL
/// </summary>
public class StoreFinalizeBLL
{
    public StoreFinalizeBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static bool Validate(DateTime date)
    {
        string err = "Process for day: " + date.Date.ToShortDateString() + ". ";

        if (date.Date > DateTime.Now.Date)
        {
            LogBLL.LogsFailAndThrow(MyMethodBase.Current.Caller, err + "Date is in future.");
        }

        RedBloodDataContext db = new RedBloodDataContext();

        if (db.StoreFinalizes.Where(r => r.Date.Value.Date > date.Date).Count() > 0)
        {
            LogBLL.LogsFailAndThrow(MyMethodBase.Current.Caller, err + "Existing newer data.");
        }

        return true;
    }

    public static void Clear(DateTime date)
    {
        Validate(date);

        RedBloodDataContext db = new RedBloodDataContext();

        var v = db.StoreFinalizes.Where(r => r.Date == date.Date);
        db.StoreFinalizes.DeleteAllOnSubmit(v);
        db.SubmitChanges();

        LogBLL.Logs();
    }

    public static void FinalizePackTransaction(DateTime date)
    {
        Validate(date);

        string err = "Process for day: " + date.Date.ToShortDateString() + ". ";

        RedBloodDataContext db = new RedBloodDataContext();

        if (db.StoreFinalizes.Where(r => r.Date.Value.Date == date.Date && r.Type != PackTransaction.TypeX.Remain).Count() > 0)
        {
            LogBLL.LogsFailAndThrow(err + "Existing data.");
        }

        var trans = from r in db.PackTransactions
                    where r.Date.Value.Date == date.Date
                    group r by r.Type into rs
                    select new { rs.Key, Count = rs.Count() };

        foreach (PackTransaction.TypeX item in Enum.GetValues(typeof(PackTransaction.TypeX)))
        {
            if (item != PackTransaction.TypeX.Remain)
            {
                //Insert 
                StoreFinalize r = new StoreFinalize();
                r.Date = date;
                r.Type = item;
                r.Note = DateTime.Now.ToString();

                int? count = trans.Where(rs => rs.Key == item).Select(rs => rs.Count).FirstOrDefault();
                r.Count = count != null ? count.Value : 0;

                db.StoreFinalizes.InsertOnSubmit(r);
            }
        }

        db.SubmitChanges();

        LogBLL.Logs();
    }
    
    public static void FinalizePackRemain(DateTime date)
    {
        Validate(date);

        string err = "Process for day: " + date.Date.ToShortDateString() + ". ";

        RedBloodDataContext db = new RedBloodDataContext();

        if (db.StoreFinalizes.Where(r => r.Date.Value.Date == date.Date && r.Type == PackTransaction.TypeX.Remain).Count() > 0)
        {
            LogBLL.LogsFailAndThrow(err + "Existing data.");
        }

        IQueryable<Pack> rows = db.Packs.Where(r => r.Status == Pack.StatusX.Product);

        //Insert 
        StoreFinalize s = new StoreFinalize();
        s.Date = date;
        s.Type = PackTransaction.TypeX.Remain;
        s.Note = DateTime.Now.ToString();
        s.Count = rows.Count();

        db.StoreFinalizes.InsertOnSubmit(s);

        db.SubmitChanges();

        LogBLL.Logs();

        
    }
}
