using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedBlood;
/// <summary>
/// Summary description for SexBLL
/// </summary>
public class PackRemainDailyBLL
{
    public PackRemainDailyBLL()
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

        if (db.PackRemainDailies.Where(r => r.Date.Value.Date > date.Date).Count() > 0)
        {
            LogBLL.LogsFailAndThrow(MyMethodBase.Current.Caller, err + "Existing newer data.");
        }

        return true;
    }

    public static void Clear(DateTime date)
    {
        Validate(date);

        RedBloodDataContext db = new RedBloodDataContext();

        var v = db.PackRemainDailies.Where(r => r.Date == date.Date);
        db.PackRemainDailies.DeleteAllOnSubmit(v);
        db.SubmitChanges();

        LogBLL.Logs();
    }

    public static void Backup(DateTime date)
    {
        Validate(date);

        string err = "Process for day: " + date.Date.ToShortDateString() + ". ";

        RedBloodDataContext db = new RedBloodDataContext();

        if (db.PackRemainDailies.Where(r => r.Date.Value.Date == date.Date).Count() > 0)
        {
            LogBLL.LogsFailAndThrow(err + "Existing data.");
        }

        IQueryable<Pack> rows = db.Packs.Where(r => r.Status == Pack.StatusX.Product);

        //Insert
        foreach (Pack item in rows)
        {
            PackRemainDaily r = new PackRemainDaily();
            r.PackID = item.ID;
            r.Status = item.Status;
            r.Date = date;
            r.Note = "Process on: " + DateTime.Now.ToString();

            db.PackRemainDailies.InsertOnSubmit(r);
        }

        db.SubmitChanges();

        LogBLL.Logs();
    }
}
