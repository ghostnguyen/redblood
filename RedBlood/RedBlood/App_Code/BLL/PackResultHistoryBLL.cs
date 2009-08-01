using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PackResultHistoryBLL
/// </summary>
public class PackResultHistoryBLL
{
    public PackResultHistoryBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Insert(RedBloodDataContext db, Pack p, int tdID, int? times, string actor, string note)
    {
        if (tdID == 0) return;

        PackResultHistory e = new PackResultHistory();
        e.PackID = p.ID;
        e.TestDefID = tdID;
        e.Times = times;
        e.Date = DateTime.Now;
        e.Actor = actor;
        e.Note = note;

        db.PackResultHistories.InsertOnSubmit(e);
    }
}
