using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BloodTypeBLL
/// </summary>
public class BloodTypeBLL
{
    public BloodTypeBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Update(RedBloodDataContext db, Pack p, int times, int? aboID, int? rhID, string actor, string note)
    {
        if (p != null && PackBLL.StatusListEnteringTestResult().Contains(p.Status))
        { }
        else
        {
            return;
        }

        if (p.BloodTypes.Count == 0)
        {
            BloodType bt = new BloodType();
            bt.PackID = p.ID;
            bt.aboID = aboID;
            bt.rhID = rhID;
            bt.Times = times;

            PackResultHistoryBLL.Insert(db, p, aboID, times, actor, note);
            PackResultHistoryBLL.Insert(db, p, rhID, times, actor, note);

            db.BloodTypes.InsertOnSubmit(bt);
            return;
        }

        if (p.BloodTypes.Count == 1)
        {
            if (p.BloodTypes[0].Times == times)
            {
                if (p.BloodTypes[0].aboID != aboID)
                {
                    p.BloodTypes[0].aboID = aboID;
                    PackResultHistoryBLL.Insert(db, p, aboID, times, actor, note);
                }
                if (p.BloodTypes[0].rhID != rhID)
                {
                    p.BloodTypes[0].rhID = rhID;
                    PackResultHistoryBLL.Insert(db, p, rhID, times, actor, note);
                }
            }
        }

        if (p.BloodTypes.Count == 2)
        {

        }
    }
}
