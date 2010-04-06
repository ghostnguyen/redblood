using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    public static void Clear(DateTime date)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        if (db.PackRemainDailies.Where(r => r.Date.Value.Date > date.Date).Count() > 0)
        {
            LogBLL.LogsFailAndThrow("Has data before " + date.Date.ToShortDateString());
        }

        var v = db.PackRemainDailies.Where(r => r.Date == date.Date);
        db.PackRemainDailies.DeleteAllOnSubmit(v);
        db.SubmitChanges();

        LogBLL.Logs();
    }
}
