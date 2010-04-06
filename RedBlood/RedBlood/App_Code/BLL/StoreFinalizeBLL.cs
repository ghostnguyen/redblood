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

    public static void Clear(DateTime date)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        if (db.StoreFinalizes.Where(r => r.Date.Value.Date > date.Date).Count() > 0)
        {
            LogBLL.LogsFailAndThrow("Has data before " + date.Date.ToShortDateString());
        }

        var v = db.StoreFinalizes.Where(r => r.Date == date.Date);
        db.StoreFinalizes.DeleteAllOnSubmit(v);
        db.SubmitChanges();

        LogBLL.Logs();
    }

    static void CountPackTransaction(DateTime date)
    {
        if (date.Date > DateTime.Now.Date) return;

        RedBloodDataContext db = new RedBloodDataContext();
        var v = db.StoreFinalizes.Where(r => r.Date == date.Date);

        if (v.Count() > 0)
        {
            if (overwrite)
            {
                db.StoreFinalizes.DeleteAllOnSubmit(v);
                db.SubmitChanges();

                LogBLL.Add(Task.TaskX.DeleteCountPackTransaction);
            }
            else
                return;
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

                int? count = trans.Where(rs => rs.Key == item).Select(rs => rs.Count).FirstOrDefault();
                r.Count = count != null ? count.Value : 0;

                db.StoreFinalizes.InsertOnSubmit(r);
            }
        }

        db.SubmitChanges();

        LogBLL.Add(Task.TaskX.CountPackTransaction);
    }
}
