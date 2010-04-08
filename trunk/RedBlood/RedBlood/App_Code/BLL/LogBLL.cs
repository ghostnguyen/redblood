using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LogBLL
/// </summary>
public class LogBLL
{
    public LogBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool IsLoggedInToday(string method)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var e = (from r in db.Logs
                 where r.Method == method
                 && r.Date.Value.Date == DateTime.Now.Date
                 select r);

        return e.Count() != 0;
    }

    public static void Logs()
    {
        Logs(Log.StatusX.Success, MyMethodBase.Current.Caller.Name, "");
    }

    public static void Logs(string note)
    {
        Logs(Log.StatusX.Success, MyMethodBase.Current.Caller.Name, note);
    }

    public static void LogsFailAndThrow(MyMethodBase method, string note)
    {
        Logs(Log.StatusX.Fail, method.Name, note);

        throw new Exception(note);
    }

    public static void LogsFailAndThrow(string note)
    {
        Logs(Log.StatusX.Fail, MyMethodBase.Current.Caller.Name, note);

        throw new Exception(note);
    }

    static void Logs(Log.StatusX status, string method, string note)
    {
        Log e = new Log();
        e.Status = status;
        e.Method = method;
        e.Actor = RedBloodSystem.SODActor;
        e.Note = note;

        RedBloodDataContext db = new RedBloodDataContext();
        db.Logs.InsertOnSubmit(e);
        db.SubmitChanges();
    }
}
