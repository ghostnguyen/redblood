using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

/// <summary>
/// Summary description for DonationTestLogBLL
/// </summary>
public class DonationTestLogBLL
{
    public DonationTestLogBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Insert(RedBloodDataContext db, Donation p, Type type, string note)
    {
        DonationTestLog e = new DonationTestLog();

        e.DIN = p.DIN;
        e.Type = type.Name;

        PropertyInfo prop = p.GetType().GetProperties().Where(r => r.PropertyType == type).FirstOrDefault();

        if (prop != null)
        {
            e.Result = prop.GetValue(p, null).ToString();
        }

        e.Date = DateTime.Now;
        e.Actor = RedBloodSystem.CurrentActor;
        e.Note = note;

        db.DonationTestLogs.InsertOnSubmit(e);
    }
}
