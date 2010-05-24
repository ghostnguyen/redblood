using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedBlood;
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

    public static bool IsLogged(string method, DateTime date)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return db.Logs.Count(r => r.Method == method && r.Date.Value.Date == date) > 0;
    }

    public static bool IsLogged()
    {
        return IsLogged(MyMethodBase.Current.Caller.Name, DateTime.Now.Date);
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
        e.Actor = RedBloodSystem.CurrentActor;
        e.Note = note;

        RedBloodDataContext db = new RedBloodDataContext();
        db.Logs.InsertOnSubmit(e);
        db.SubmitChanges();
    }
}
