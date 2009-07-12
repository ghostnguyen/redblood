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

    //public static void Update(RedBloodDataContext db, Pack p, int times, TestDef ABO, TestDef RH, string actor, string note)
    //{
    //    if (p == null
    //       || !PackBLL.AllowEnterTestResult().Contains(p.TestResultStatus)
    //       || p.ComponentID == null
    //       || p.ComponentID != TestDef.Component.Full)
    //        return;

    //    if (p.BloodTypes.Count == 0)
    //    {
    //        BloodType bt = new BloodType();
    //        bt.PackID = p.ID;
    //        bt.ABO = ABO;
    //        bt.RH = RH;
    //        bt.Times = times;

    //        PackResultHistoryBLL.Insert(db, p, ABO, times, actor, note);
    //        PackResultHistoryBLL.Insert(db, p, RH, times, actor, note);

    //        db.BloodTypes.InsertOnSubmit(bt);
    //        return;
    //    }

    //    if (p.BloodTypes.Count == 1)
    //    {
    //        if (p.BloodTypes[0].Times == times)
    //        {
    //            if (p.BloodTypes[0].ABO != ABO)
    //            {
    //                p.BloodTypes[0].ABO = ABO;
    //                PackResultHistoryBLL.Insert(db, p, ABO, times, actor, note);
    //            }
    //            if (p.BloodTypes[0].RH != RH)
    //            {
    //                p.BloodTypes[0].RH = RH;
    //                PackResultHistoryBLL.Insert(db, p, RH, times, actor, note);
    //            }
    //        }
    //    }

    //    if (p.BloodTypes.Count == 2)
    //    {

    //    }
    //}
}
