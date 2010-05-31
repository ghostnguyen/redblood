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
            LogBLL.LogsFailAndThrow(err + "Date is in future.");
        }

        RedBloodDataContext db = new RedBloodDataContext();

        if (db.StoreFinalizes.Where(r => r.Date.Value.Date > date.Date).Count() > 0)
        {
            LogBLL.LogsFailAndThrow(err + "Existing newer data.");
        }

        var v = db.StoreFinalizes.Where(r => r.Date == date.Date);
        db.StoreFinalizes.DeleteAllOnSubmit(v);
        db.SubmitChanges();

        LogBLL.Logs();
    }



    public static List<StoreFinalize> CountPackTransaction(DateTime date)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var trans = from r in db.PackTransactions
                    where r.Date.Value.Date == date.Date
                    group r by r.Type into rs
                    select rs;

        return trans.ToList().Select(r => new StoreFinalize() { Date = date.Date, Type = r.Key, Count = r.Count() }).ToList();
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

        return count;
    }

    public static int CountPackRemainByLastDayRemain(DateTime date)
    {
        string err = "Process for day: " + date.Date.ToShortDateString() + ". ";

        if (date.Date > DateTime.Now.Date)
        {
            LogBLL.LogsFailAndThrow(err + "Date is in future.");
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
    }

    public static int Add(DateTime date, PackTransaction.TypeX type, int count)
    {
        if (Get(date, type) != null)
        {
            LogBLL.LogsFailAndThrow("Existing datat.");
        }

        RedBloodDataContext db = new RedBloodDataContext();

        StoreFinalize s = new StoreFinalize();
        s.Date = date;
        s.Type = type;
        s.Count = count;
        s.Note = "Process on: " + DateTime.Now.ToString();

        db.StoreFinalizes.InsertOnSubmit(s);
        db.SubmitChanges();

        return 1;
    }

    public static StoreFinalize Get(DateTime date, PackTransaction.TypeX type)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return db.StoreFinalizes.Where(r => r.Date.Value.Date == date.Date && r.Type == type).FirstOrDefault();
    }

    public static void FinalizeStore(DateTime date, bool overwrite)
    {
        string err = "Process for day: " + date.Date.ToShortDateString() + ". ";

        DateTime? firstFinalizeDate;
        DateTime? lastFinalizeDate;
        DateTime? lastPackTransactionDate;
        DateTime? firstPackTransactionDate;
        DateTime? lastBackupPackRemainDate;

        RedBloodSystemBLL.GetLastFinalizeDate(out firstFinalizeDate, out lastFinalizeDate, out lastPackTransactionDate, out firstPackTransactionDate, out lastBackupPackRemainDate);

        if (date.Date > DateTime.Now.Date
            || lastFinalizeDate.HasValue && lastFinalizeDate.Value.Date > DateTime.Now.Date
            || (lastPackTransactionDate.HasValue && lastPackTransactionDate.Value.Date > DateTime.Now.Date)
            || (lastBackupPackRemainDate.HasValue && lastBackupPackRemainDate.Value.Date > DateTime.Now.Date)
            )
        {
            LogBLL.LogsFailAndThrow(err + "Error. Data or Date in future.");
        }

        //Data in DB is newer. Data error or system datetime error
        if (
            (lastFinalizeDate.HasValue && lastFinalizeDate.Value.Date > date.Date)
            || (lastPackTransactionDate.HasValue && lastPackTransactionDate.Value.Date > date.Date)
            || (lastBackupPackRemainDate.HasValue && lastBackupPackRemainDate.Value.Date > date.Date)
           )
        {
            LogBLL.LogsFailAndThrow(err + "Newer data in DB.");
        }

        if (lastFinalizeDate.HasValue)
        {
            int daysBefore = (date.Date - lastFinalizeDate.Value.Date).Days;

            if (daysBefore != 0
                && daysBefore != 1)
            {
                LogBLL.LogsFailAndThrow(err + "Data should be finalized with in or 1 day before.");
            }

            if (daysBefore == 0)
            {
                if (overwrite)
                {
                    //Clear StoreFinalizes
                    StoreFinalizeBLL.Clear(date);

                    //Clear PackRemainDailies
                    PackRemainDailyBLL.Clear(date);
                }
                else
                {
                    LogBLL.LogsFailAndThrow(err + "Aldready finilized. Set overwrite=true to re-finilized.");
                }
            }
        }

        StoreFinalizeBLL.CountPackTransaction(date).Select(r => StoreFinalizeBLL.Add(r.Date.Value, r.Type, r.Count.Value)).ToList();
        StoreFinalizeBLL.Add(date, PackTransaction.TypeX.Remain, StoreFinalizeBLL.CountPackRemainByPackStatus());
        PackRemainDailyBLL.Backup(date);

        LogBLL.Logs(err);
    }
}
