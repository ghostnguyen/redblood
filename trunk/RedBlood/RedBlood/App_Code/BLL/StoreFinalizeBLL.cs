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

        var v = db.StoreFinalizes.Where(r => r.Date == date.Date);
        db.StoreFinalizes.DeleteAllOnSubmit(v);
        db.SubmitChanges();

        LogBLL.Logs();
    }



    public static IQueryable<StoreFinalize> CountPackTransaction(DateTime date)
    {
        // string err = "Process for day: " + date.Date.ToShortDateString() + ". ";

        //if (date.Date > DateTime.Now.Date)
        //{
        //    LogBLL.LogsFailAndThrow(MyMethodBase.Current.Caller, err + "Date is in future.");
        //}

        RedBloodDataContext db = new RedBloodDataContext();

        var trans = from r in db.PackTransactions
                    where r.Date.Value.Date == date.Date
                    group r by r.Type into rs
                    select new StoreFinalize() { Type = rs.Key, Count = rs.Count() };

        return trans;

        //foreach (PackTransaction.TypeX item in Enum.GetValues(typeof(PackTransaction.TypeX)))
        //{
        //    if (item != PackTransaction.TypeX.Remain)
        //    {
        //        //Insert 
        //        StoreFinalize r = new StoreFinalize();
        //        r.Date = date;
        //        r.Type = item;
        //        r.Note = DateTime.Now.ToString();

        //        int? count = trans.Where(rs => rs.Key == item).Select(rs => rs.Count).FirstOrDefault();
        //        r.Count = count != null ? count.Value : 0;

        //        db.StoreFinalizes.InsertOnSubmit(r);
        //    }
        //}

        //db.SubmitChanges();

        //LogBLL.Logs();
    }

    public static int CountPackRemainByStoreFinalize(DateTime date)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var v = db.StoreFinalizes.Where(r => r.Date == date.Date
                   && r.Type == PackTransaction.TypeX.Remain)
                   .Select(r => r.Count);

        if (v.Count() > 1)
            LogBLL.LogsFailAndThrow("Data Err.");

        int? i = v.FirstOrDefault();
        return i.HasValue ? i.Value : 0;
    }

    public static int CountPackRemainByPackStatus()
    {
        DateTime date = DateTime.Now.Date;

        string err = "Process for day: " + date.Date.ToShortDateString() + ". ";

        RedBloodDataContext db = new RedBloodDataContext();

        if (db.PackTransactions.Where(r => r.Date.Value.Date > date).Count() > 0)
        {
            LogBLL.LogsFailAndThrow(err + "Data error. Having pack transaction in future.");
        }

        if (db.Packs.Where(r => r.Date.Value.Date > date).Count() > 0)
        {
            LogBLL.LogsFailAndThrow(err + "Data error. Having pack in future.");
        }

        int count = db.Packs.Where(r => r.Status == Pack.StatusX.Product).Count();

        ////Insert 
        //StoreFinalize s = new StoreFinalize();
        //s.Date = date;
        //s.Type = PackTransaction.TypeX.Remain;
        //s.Note = DateTime.Now.ToString();
        //s.Count = rows.Count();

        //db.StoreFinalizes.InsertOnSubmit(s);

        //db.SubmitChanges();

        //LogBLL.Logs(count.ToString());

        return count;
    }

    public static int CountPackRemainByLastDayRemain(DateTime date)
    {
        string err = "Process for day: " + date.Date.ToShortDateString() + ". ";

        if (date.Date > DateTime.Now.Date)
        {
            LogBLL.LogsFailAndThrow(MyMethodBase.Current.Caller, err + "Date is in future.");
        }

        RedBloodDataContext db = new RedBloodDataContext();

        DateTime previousDate = date.Date.AddDays(-1);
        int previousRemain = CountPackRemainByStoreFinalize(previousDate);

        var v1 = CountPackTransaction(date);

        int? i = (previousRemain
            + v1.Where(r => r.Type > 0).Sum(r => r.Count)
            - v1.Where(r => r.Type < 0).Sum(r => r.Count)
            );

        return i.HasValue ? i.Value : 0;


        //foreach (StoreFinalize item in v1)
        //{
        //    if (item.Type > 0)
        //    {
        //        remaingOfPreDate += item.Count == null ? 0 : item.Count.Value;
        //    }
        //    else if (item.Type < 0)
        //    {
        //        remaingOfPreDate -= item.Count == null ? 0 : item.Count.Value;
        //    }
        //}

        //Insert 
        //StoreFinalize s = new StoreFinalize();
        //s.Date = date;
        //s.Type = PackTransaction.TypeX.Remain;
        //s.Count = remaingOfPreDate;

        //db.StoreFinalizes.InsertOnSubmit(s);

        //db.SubmitChanges();

        //LogBLL.Add(Task.TaskX.CountPackRemain);

        //LogBLL.Logs();
    }
}
