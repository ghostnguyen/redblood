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

    public static bool IsLog(Task.TaskX task)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var e = (from r in db.Logs
                 where r.TaskID == task
                 && r.Date.Value.Date == DateTime.Now.Date
                 && r.Actor == RedBloodSystem.SODActor
                 select r);

        return e.Count() != 0;
    }

    public static void Add(Task.TaskX task)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Add(db, task);
        db.SubmitChanges();
    }

    public static void Add(RedBloodDataContext db, Task.TaskX task)
    {
        if (!IsLog(task))
        {
            Log e = new Log();
            e.TaskID = task;
            e.Date = DateTime.Now;
            e.Actor = RedBloodSystem.SODActor;

            db.Logs.InsertOnSubmit(e);
        }
    }
}
