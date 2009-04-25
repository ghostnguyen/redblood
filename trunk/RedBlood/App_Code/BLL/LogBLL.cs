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

    public static bool IsScanEpxDone()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var e = (from r in db.Logs
                 where r.TaskID == Task.TaskX.ScanExp
                 && r.Date == DateTime.Now.Date
                 && r.Actor == SystemActor.SOD
                 select r).Take(1);

        return e.Count() != 0;
    }
}
