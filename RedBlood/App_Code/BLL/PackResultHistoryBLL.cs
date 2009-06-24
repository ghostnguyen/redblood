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

    public static void Insert(RedBloodDataContext db, Pack p, TestDef td, int? times, string actor, string note)
    {
        PackResultHistory e = new PackResultHistory();
        e.PackID = p.ID;
        e.TestDef = td;
        e.Times = times;
        e.Date = DateTime.Now;
        e.Actor = actor;
        e.Note = note;

        db.PackResultHistories.InsertOnSubmit(e);
    }
}
