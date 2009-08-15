using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DonationTestHistoryBLL
/// </summary>
public class DonationTestHistoryBLL
{
    public DonationTestHistoryBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Insert(RedBloodDataContext db, Donation p, Type type, string note)
    {
        
        //if (resultType == 0) return;

        DonationTestHistory e = new DonationTestHistory();

        e.DIN = p.DIN;
        e.ResultType = type.Name;

        //e. p.GetType().GetProperty(type.FullName).GetValue(p, null);

        e.Date = DateTime.Now;
        e.Actor = RedBloodSystem.CurrentActor;
        e.Note = note;

        db.DonationTestHistories.InsertOnSubmit(e);
    }
}
