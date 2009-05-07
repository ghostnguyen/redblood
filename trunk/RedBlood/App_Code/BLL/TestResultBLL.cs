using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TestResultBLL
/// </summary>
public class TestResultBLL
{
    public TestResultBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static TestResult GetByID(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext(); 
        return (from r in db.TestResults
                        where r.ID == ID
                        select r).First();
    }
    public static void Update(RedBloodDataContext db, Pack p, int times,
       int? hivID, int? hcvID, int? HBsAgID, int? syphilisID, int? malariaID, string actor, string note)
    {
        if (p != null && PackBLL.StatusListEnteringTestResult().Contains(p.Status))
        { }
        else
        {
            return;
        }

        if (p.TestResults.Count == 0)
        {
            TestResult e = new TestResult();
            e.PackID = p.ID;

            e.HIVID = hivID;
            e.HCVID = hcvID;
            e.HBsAgID = HBsAgID;
            e.SyphilisID = syphilisID;
            e.MalariaID = malariaID;
            e.CommitDate = DateTime.Now;
            e.Actor = actor;

            e.Times = times;

            PackResultHistoryBLL.Insert(db, p, hivID, times, actor, note);
            PackResultHistoryBLL.Insert(db, p, hcvID, times, actor, note);
            PackResultHistoryBLL.Insert(db, p, HBsAgID, times, actor, note);
            PackResultHistoryBLL.Insert(db, p, syphilisID, times, actor, note);
            PackResultHistoryBLL.Insert(db, p, malariaID, times, actor, note);

            db.TestResults.InsertOnSubmit(e);
            return;
        }

        if (p.TestResults.Count == 1)
        {
            if (p.TestResults[0].Times == times)
            {
                if (p.TestResults[0].HIVID != hivID)
                {
                    p.TestResults[0].HIVID = hivID;
                    PackResultHistoryBLL.Insert(db, p, hivID, times, actor, note);
                }

                if (p.TestResults[0].HCVID != hcvID)
                {
                    p.TestResults[0].HCVID = hcvID;
                    PackResultHistoryBLL.Insert(db, p, hcvID, times, actor, note);
                }

                if (p.TestResults[0].HBsAgID != HBsAgID)
                {
                    p.TestResults[0].HBsAgID = HBsAgID;
                    PackResultHistoryBLL.Insert(db, p, HBsAgID, times, actor, note);
                }

                if (p.TestResults[0].SyphilisID != syphilisID)
                {
                    p.TestResults[0].SyphilisID = syphilisID;
                    PackResultHistoryBLL.Insert(db, p, syphilisID, times, actor, note);
                }

                if (p.TestResults[0].MalariaID != malariaID)
                {
                    p.TestResults[0].MalariaID = malariaID;
                    PackResultHistoryBLL.Insert(db, p, malariaID, times, actor, note);
                }

                p.TestResults[0].CommitDate = DateTime.Now;
                p.TestResults[0].Actor = actor;
            }
        }

        if (p.BloodTypes.Count == 2)
        {

        }
    }
}
