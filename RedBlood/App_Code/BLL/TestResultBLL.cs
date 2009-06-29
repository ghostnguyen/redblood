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


    //public static void Update(RedBloodDataContext db, Pack p, int times,
    //   int? hivID, int? hcvID, int? HBsAgID, int? syphilisID, int? malariaID, 
    //    string actor, string note)
    public static void Update(RedBloodDataContext db, Pack p, int times,
       TestDef HIV, TestDef HCV, TestDef HBsAg, TestDef Syphilis, TestDef Malaria,
        string actor, string note)
    {
        if (p == null || !PackBLL.AllowEnterTestResult().Contains(p.TestResultStatus))
            return;

        if (p.TestResults.Count == 0)
        {
            TestResult e = new TestResult();
            e.PackID = p.ID;

            e.HIV = HIV;
            e.HCV = HCV;
            e.HBsAg = HBsAg;
            e.Syphilis = Syphilis;
            e.Malaria = Malaria;
            e.CommitDate = DateTime.Now;
            e.Actor = actor;

            e.Times = times;

            PackResultHistoryBLL.Insert(db, p, HIV, times, actor, note);
            PackResultHistoryBLL.Insert(db, p, HCV, times, actor, note);
            PackResultHistoryBLL.Insert(db, p, HBsAg, times, actor, note);
            PackResultHistoryBLL.Insert(db, p, Syphilis, times, actor, note);
            PackResultHistoryBLL.Insert(db, p, Malaria, times, actor, note);

            db.TestResults.InsertOnSubmit(e);
            return;
        }

        if (p.TestResults.Count == 1)
        {
            if (p.TestResults[0].Times == times)
            {
                if (p.TestResults[0].HIV != HIV)
                {
                    p.TestResults[0].HIV = HIV;
                    //p.TestResults[0].HIV = db.TestDefs.Where(r => r.ID == hivID).FirstOrDefault();
                    PackResultHistoryBLL.Insert(db, p, HIV, times, actor, note);
                }

                if (p.TestResults[0].HCV != HCV)
                {
                    p.TestResults[0].HCV = HCV;
                    PackResultHistoryBLL.Insert(db, p, HCV, times, actor, note);
                }

                if (p.TestResults[0].HBsAg != HBsAg)
                {
                    p.TestResults[0].HBsAg = HBsAg;
                    PackResultHistoryBLL.Insert(db, p, HBsAg, times, actor, note);
                }

                if (p.TestResults[0].Syphilis != Syphilis)
                {
                    p.TestResults[0].Syphilis = Syphilis;
                    PackResultHistoryBLL.Insert(db, p, Syphilis, times, actor, note);
                }

                if (p.TestResults[0].Malaria != Malaria)
                {
                    p.TestResults[0].Malaria = Malaria;
                    PackResultHistoryBLL.Insert(db, p, Malaria, times, actor, note);
                }

                p.TestResults[0].CommitDate = DateTime.Now;
                p.TestResults[0].Actor = actor;
            }
        }

        if (p.BloodTypes.Count == 2)
        {

        }

        //PackBLL.UpdateTestResultStatus4Full(db, p);
    }

    public static List<TestDef> GetNonNegative(TestResult e)
    {
        List<TestDef> r = new List<TestDef>();

        if (e == null || e.HIVID == null || e.HBsAgID == null || e.HCVID == null || e.SyphilisID == null || e.MalariaID == null)
            throw new Exception("Chưa nhập kết quả túi máu.");

        if (e.HIVID == TestDef.HIV.Pos || e.HIVID == TestDef.HIV.NA)
            r.Add(e.HIV);

        if (e.HBsAgID == TestDef.HBsAg.Pos || e.HBsAgID == TestDef.HBsAg.NA)
            r.Add(e.HBsAg);

        if (e.HCVID == TestDef.HCV.Pos || e.HCVID == TestDef.HCV.NA)
            r.Add(e.HCV);

        if (e.SyphilisID == TestDef.Syphilis.Pos || e.SyphilisID == TestDef.Syphilis.NA)
            r.Add(e.Syphilis);

        if (e.MalariaID == TestDef.Malaria.Pos || e.MalariaID == TestDef.Malaria.NA)
            r.Add(e.Malaria);

        return r;
    }


}
