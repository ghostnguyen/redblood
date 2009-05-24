using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PackBLL
/// </summary>
public class PackBLL
{
    CodabarBLL codabarBLL = new CodabarBLL();
    CampaignBLL campaignBLL = new CampaignBLL();
    public PackBLL()
    {
    }

    /// <summary>
    /// Return the list of pack status which pack had entered test result
    /// </summary>
    /// <returns></returns>
    public static Pack.StatusX[] StatusListHadTestResult()
    {
        return new Pack.StatusX[] { Pack.StatusX.CommitTestResult };
    }

    /// <summary>
    /// Return the list of pack status which pack had entered test result
    /// </summary>
    /// <returns></returns>
    public static Pack.StatusX[] StatusListEnteringTestResult()
    {
        return new Pack.StatusX[] { Pack.StatusX.Assign, Pack.StatusX.EnterTestResult, Pack.StatusX.CommitTestResult };
    }

    public static DateTime LowerLimDate()
    {
        //CollectDate + (ExpireCount - 1) >= Now
        //CollectDate >= Now + 1 - ExpireCount
        //CollectDate >= LowerLimDate
        return DateTime.Now.Date.AddDays(1 - Resources.Setting.EnterPackExpire.ToInt());
    }



    public static Pack Get(int autonum)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(autonum, db, Pack.StatusX.All);
    }

    public static Pack Get(int autonum, RedBloodDataContext db)
    {
        return Get(autonum, db, Pack.StatusX.All, false);
    }

    public static Pack Get(int autonum, RedBloodDataContext db, Pack.StatusX status)
    {
        return Get(autonum, db, status, false);
    }

    public static Pack Get(int autonum, RedBloodDataContext db, Pack.StatusX status, bool allowPackErr)
    {
        return Get(autonum, db, new Pack.StatusX[] { status }, allowPackErr);
    }
    public static Pack Get(int autonum, RedBloodDataContext db, Pack.StatusX[] status, bool allowPackErr)
    {
        if (autonum == 0) return null;

        if (status.Count() == 1 && status[0] == Pack.StatusX.All)
        {
            var v = (from c in db.Packs
                     where c.Autonum == autonum
                     select c);

            if (v.Count() == 0) return null;

            Pack p = v.First();

            if (allowPackErr) return p;

            if (Validate(p) != null)
                return p;
        }
        else
        {
            var v = from c in db.Packs
                    where c.Autonum == autonum && status.Contains(c.Status)
                    select c;

            if (v.Count() == 0) return null;

            Pack p = v.First();

            if (allowPackErr) return p;

            if (Validate(p) != null)
                return p;
        }
        return null;
    }

    public static Pack GetByCode(string code)
    {
        return Get(CodabarBLL.ParsePackAutoNum(code));
    }

    public static Pack[] GetByStatus(Pack.StatusX[] status, out RedBloodDataContext db)
    {
        db = new RedBloodDataContext();

        var rs = from c in db.Packs
                 where status.Contains(c.Status)
                 select c;

        return rs.ToArray();
    }

    public static List<Pack> Get(int campaignID, Pack.StatusX[] status)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return db.Packs.Where(r => r.CampaignID == campaignID && status.Contains(r.Status)).ToList();
    }

    public PackErr Assign(int autonum, Guid peopleID, string actor, int campaignID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var e = from c in db.Packs
                where c.Autonum == autonum && c.PeopleID == null && c.CampaignID == null
                select c;

        if (e.Count() != 1)
        {
            return PackErrList.DataErr;
        }

        Pack p = e.First();

        PackErr err = Validate(p);
        if (!err.Equals(PackErrList.Non)) return err;

        try
        {
            Pack.StatusX from = p.Status;

            p.PeopleID = peopleID;
            p.Status = Pack.StatusX.Assign;
            p.CollectedDate = DateTime.Now;
            p.CampaignID = campaignID;
            p.ComponentID = (int)TestDef.Component.Full;

            db.PackStatusHistories.InsertOnSubmit(new PackStatusHistory(p, from, p.Status, actor, "Assign peopleID=" + peopleID.ToString() + "&CampaignID=" + campaignID.ToString()));

            db.SubmitChanges();

            campaignBLL.SetStatus(campaignID);
        }
        catch (Exception ex)
        {
            return new PackErr(ex.Message);
        }

        return PackErrList.Non;
    }

    public Pack[] New(int count)
    {
        Pack[] l = new Pack[count];

        for (int i = 0; i < l.Length; i++)
        {
            l[i] = new Pack();
            l[i].Status = Pack.StatusX.Init;
        }

        RedBloodDataContext db = new RedBloodDataContext();

        db.Packs.InsertAllOnSubmit(l);

        db.SubmitChanges();
        return l;
    }



    public Pack GetEnterPackByPeopleID(Guid peopleID)
    {
        return GetEnterPackByPeopleID(peopleID, null);
    }

    public Pack GetEnterPackByPeopleID(Guid peopleID, PackErr err)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var e = from c in db.Packs
                where c.PeopleID == peopleID && (c.Status == Pack.StatusX.Assign || c.Status == Pack.StatusX.CommitReceived)
                orderby c.Status descending, c.CollectedDate descending
                select c;

        if (e.Count() == 1)
        {
            Pack p = e.First();

            if (Validate(p) != null)
                return p;
            else
                return null;
        }

        if (e.Count() > 1)
        {
            err = PackErrList.EnterPackMulti;
        }

        return null;
    }

    public static Pack[] GetEnterPackErr()
    {
        RedBloodDataContext db = new RedBloodDataContext();
        DateTime expDate = DateTime.Now.Date.AddDays(1 - Resources.Setting.EnterPackExpire.ToInt());

        DateTime lim = LowerLimDate();
        var e = from c in db.Packs
                where (c.Status == Pack.StatusX.Assign || c.Status == Pack.StatusX.CommitReceived) && (c.CollectedDate < lim || c.CollectedDate >= DateTime.Now)
                orderby c.Status descending, c.CollectedDate descending
                select c;

        return e.ToArray();
    }

    public Pack CommitEnterPack(int autonum, bool withABO, string actor)
    {
        if (autonum == 0) return null;

        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get(autonum, db, Pack.StatusX.Assign);

        if (p == null
            || p.ComponentID == null || p.Volume == null) return p;

        if (withABO)
        {
            if (p.BloodTypes.Count != 1) return p;

            BloodType bt = p.BloodTypes[0];

            if (bt.aboID == null || bt.rhID == null) return p;

            bt.Actor = actor;
            bt.CommitDate = DateTime.Now;
        }
        else
        {

        }

        Pack.StatusX from = p.Status;

        p.CollectedDate = DateTime.Now;
        p.Status = Pack.StatusX.CommitReceived;
        p.Actor = actor;

        db.PackStatusHistories.InsertOnSubmit(new PackStatusHistory(p, from, p.Status, actor, "Commit Received"));

        db.SubmitChanges();

        return p;
    }

    public static PackStatusHistory ChangeStatus(Pack p, Pack.StatusX to, string actor, string note)
    {
        Pack.StatusX from = p.Status;

        p.Status = to;

        return new PackStatusHistory(p, from, to, actor, note);
    }


    public static PackErr DeletePack(int? campaignID, int autonum, string note, string actor)
    {
        if (autonum == 0) return null;

        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get(autonum, db, Pack.StatusX.All, true);

        if (p == null) return PackErrList.NonExist;

        if (campaignID != null)
        {
            if (p.CampaignID != campaignID) return PackErrList.NonExistInCam;
        }

        Pack.StatusX from = p.Status;

        p.Status = Pack.StatusX.Delete;

        db.PackStatusHistories.InsertOnSubmit(new PackStatusHistory(p, from, p.Status, actor, note));

        db.SubmitChanges();

        return null;
    }

    public static void Delete_EnterPackErr(string actor)
    {
        Pack[] l = GetEnterPackErr();
        foreach (Pack e in l)
        {
            DeletePack(null, e.Autonum, "Hủy túi máu bị lỗi khi thu.", actor);
        }
    }

    //Only pack has status 0 can be remove, to re-assign to another people.
    public Pack RemovePeople(int autonum, string actor)
    {
        if (autonum == 0) return null;

        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get(autonum, db, Pack.StatusX.Assign);

        if (p == null && p.PeopleID != null) return p;


        Pack.StatusX from = p.Status;

        //remove people
        p.PeopleID = null;
        p.Status = Pack.StatusX.Init; ;
        p.CollectedDate = null;
        p.CampaignID = null;

        db.PackStatusHistories.InsertOnSubmit(new PackStatusHistory(p, from, p.Status, actor, "Remove peopleID=" + p.PeopleID.ToString() + "&CampaignID=" + p.CampaignID.ToString()));

        db.SubmitChanges();

        return p;
    }

    public static TestDef[] ValidateTestResult(TestResult e)
    {
        List<TestDef> r = new List<TestDef>();

        if (e == null || e.HIVID == null || e.HBsAgID == null || e.HCVID == null || e.SyphilisID == null || e.MalariaID == null)
            throw new Exception("Chưa nhập kết quả túi máu.");

        if (e.HIVID.Value == (int)TestDef.HIV.Pos || e.HIVID.Value == (int)TestDef.HIV.NA)
            r.Add(e.HIV);

        if (e.HBsAgID.Value == (int)TestDef.HBsAg.Pos || e.HBsAgID.Value == (int)TestDef.HBsAg.NA)
            r.Add(e.HBsAg);

        if (e.HCVID.Value == (int)TestDef.HCV.Pos || e.HCVID.Value == (int)TestDef.HCV.NA)
            r.Add(e.HCV);

        if (e.SyphilisID.Value == (int)TestDef.Syphilis.Pos || e.SyphilisID.Value == (int)TestDef.Syphilis.NA)
            r.Add(e.Syphilis);

        if (e.MalariaID.Value == (int)TestDef.Malaria.Pos || e.MalariaID.Value == (int)TestDef.Malaria.NA)
            r.Add(e.Malaria);

        return r.ToArray();
    }

    public static PackErr ValidateAndChangeStatus(RedBloodDataContext db, Pack p, string actor)
    {
        PackErr err = Validate(p);

        if (!err.Equals(PackErrList.Non))
        {
            PackStatusHistory h = PackBLL.ChangeStatus(p, err.ToStatusX, actor, err.Message);
            db.PackStatusHistories.InsertOnSubmit(h);
        }

        return err;
    }



    public static PackErr Validate(Pack p)
     {
        if (p == null) return PackErrList.Non;

        if (p.Status == Pack.StatusX.Init)
        {
            if (p.PeopleID != null
                || p.CollectedDate != null
                || p.CampaignID != null)
                return PackErrList.DataErr;
        }
        else
        {
            if (p.PeopleID == null
                || p.CampaignID == null
                || p.CollectedDate == null
                || p.CollectedDate >= DateTime.Now)
                return PackErrList.DataErr;
        }

        //Check exists entered pack 
        //if (p.Status == Pack.StatusX.Assign || p.Status == Pack.StatusX.CommitReceived)

        if (StatusListEnteringTestResult().Contains(p.Status))
        {
            //is expired
            if (!(p.CollectedDate.Value.Date >= LowerLimDate()))
                return PackErrList.Expired;

            //Data error
            if (p.BloodTypes.Count > 1)
                return PackErrList.DataErr;

            //Data error
            if (p.BloodTypes.Count == 1)
            {
                try
                {
                    //Validate BloodType
                    BloodType e = p.BloodTypes[0];
                }
                catch (Exception ex)
                {
                    return new PackErr(ex.Message, Pack.StatusX.Delete);
                }
            }
        }

        return PackErrList.Non;
    }

    //public static object GetByCampaingID4Manually(int campaignID)
    //{
    //    //Pack.StatusX[] s = new Pack.StatusX[] { Pack.StatusX.Assign, Pack.StatusX.EnterTestResult, Pack.StatusX.CommitTestResult };
    //    Pack.StatusX[] s = StatusListEnteringTestResult();
    //    return GetByCampaingID4Manually(campaignID, s);
    //}

    //public static object GetByCampaingID4Manually(int campaignID, Pack.StatusX[] status)
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    var v = from r in db.Packs
    //            where r.CampaignID == campaignID && status.Contains(r.Status)
    //            select new
    //            {
    //                BT = (from r1 in r.BloodTypes
    //                      where r1.Times == 2
    //                      select r1).First(),
    //                TR = (from r1 in r.TestResults
    //                      where r1.Times == 2
    //                      select r1).First(),
    //                r.ID,
    //                r.Autonum,
    //                PeopleName = r.People.Name,
    //                r.CollectedDate,
    //                r.Status,
    //                ComponentName = r.Component.Name,
    //                r.ComponentID,
    //                r.Volume,
    //                r.Note,
    //                DeleteNote = r.PackStatusHistories.Where(h => h.ToStatus == Pack.StatusX.Delete).First().Note
    //            };

    //    return v;
    //}

    public static List<Pack> Get4Rpt(int campaignID, ReportType rptType)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack.StatusX[] status = PackBLL.StatusListHadTestResult();
        var v = from r in db.Packs
                where r.CampaignID == campaignID && status.Contains(r.Status)
                select r;

        if (rptType == ReportType.NegInCam)
        {
            return v.ToList().Where(r => ValidateTestResult(r.TestResult2).Count() == 0).ToList();
        }

        if (rptType == ReportType.FourPosInCam)
        {
            return v.ToList().Where(r =>
                ValidateTestResult(r.TestResult2).Count() > 0 &&
                ValidateTestResult(r.TestResult2).Where(tdef => tdef.ID == (int)TestDef.HIV.Pos || tdef.ID == (int)TestDef.HIV.NA).Count() == 0).ToList();
        }

        if (rptType == ReportType.HIVInCam)
        {
            return v.ToList().Where(r => PackBLL.ValidateTestResult(r.TestResult2).Where(tdef => tdef.ID == (int)TestDef.HIV.Pos || tdef.ID == (int)TestDef.HIV.NA).Count() == 1).ToList();
        }

        return null;
    }

    public static PackErr Update(Pack p, int? componentID, int? volume)
    {
        if (p == null) return PackErrList.NonExist;

        if (p.ComponentID != componentID) p.ComponentID = componentID;
        if (p.Volume != volume) p.Volume = volume;

        return PackErrList.Non;
    }


    //public static PackErr Update4Manually(int autonum, int? componentID, int? volume, int? aboID, int? rhID,
    //    int? hivID, int? hcvID, int? HBsAgID, int? syphilisID, int? malariaID,
    //    string actor, string note)
    //{
    //    RedBloodDataContext db;

    //    Pack p = GetByAutonum(autonum, out db, StatusListEnteringTestResult(), true);

    //    if (p == null) return PackErrList.NonExist;

    //    Update(p, componentID, volume);
    //    BloodTypeBLL.Update(db, p, 2, aboID, rhID, actor, note);
    //    TestResultBLL.Update(p, 2, hivID, hcvID, HBsAgID, syphilisID, malariaID, db, actor, note);

    //    VerifyCommitTestResult(db, p, actor);

    //    db.SubmitChanges();

    //    return PackErrList.Non;
    //}

    public static void VerifyCommitTestResult(RedBloodDataContext db, Pack p, string actor)
    {
        if (p != null
            && StatusListEnteringTestResult().Contains(p.Status))
        {
            if (
                (p.ComponentID != null
                && p.ComponentID.Value == (int)TestDef.Component.Platelet)

                ||

                (p.ComponentID != null && p.Volume != null
                && p.BloodType2 != null
                && p.BloodType2.aboID != null && p.BloodType2.rhID != null
                && p.TestResult2 != null
                && p.TestResult2.HIVID != null && p.TestResult2.HCVID != null && p.TestResult2.HBsAgID != null && p.TestResult2.SyphilisID != null && p.TestResult2.MalariaID != null)
                )
            {
                Pack.StatusX from = p.Status;
                p.Status = Pack.StatusX.CommitTestResult;
                db.PackStatusHistories.InsertOnSubmit(new PackStatusHistory(p, from, Pack.StatusX.CommitTestResult, actor, "Manually Enter"));
            }
            else
            {
                Pack.StatusX from = p.Status;
                p.Status = Pack.StatusX.EnterTestResult;
                db.PackStatusHistories.InsertOnSubmit(new PackStatusHistory(p, from, Pack.StatusX.EnterTestResult, actor, "Manually Enter"));
            }
        }
    }
}

