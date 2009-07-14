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
    public static Pack.StatusX[] StatusListAllowEnterTestResult()
    {
        return new Pack.StatusX[] { Pack.StatusX.Collected, Pack.StatusX.Produced };
    }

    //public static Pack.StatusX[] StatusList4Production()
    //{
    //    return new Pack.StatusX[] { Pack.StatusX.Collected, Pack.StatusX.EnterTestResult, Pack.StatusX.CommitTestResult };
    //}

    public static Pack.StatusX[] StatusList4Order()
    {
        return new Pack.StatusX[] { Pack.StatusX.Collected, Pack.StatusX.Production };
    }

    /// <summary>
    /// Return the list of pack status which pack had entered test result
    /// </summary>
    /// <returns></returns>
    public static Pack.TestResultStatusX[] AllowEnterTestResult()
    {
        return new Pack.TestResultStatusX[] { Pack.TestResultStatusX.Non, 
            Pack.TestResultStatusX.Negative, 
            Pack.TestResultStatusX.Positive};
    }

    /// <summary>
    /// Get pack -> Validate -> Change status if need
    /// </summary>
    /// <param name="autonum"></param>
    /// <returns></returns>
    public static Pack GetCarefully(RedBloodDataContext db, int autonum)
    {
        Pack pErr = new Pack();

        Pack p = Get(db, autonum);

        if (p == null)
        {
            pErr.Err = PackErrList.NonExist;
            return pErr;
        }

        PackErr err = ValidateAndUpdateStatus(db, p);
        if (err != PackErrList.Non)
        {
            db.SubmitChanges();
            pErr.Err = err;
            return pErr;
        }
        else
        {
            p.Err = err;
            return p;
        }
    }

    public static Pack Get(int autonum)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(db, autonum);
    }

    public static Pack Get(RedBloodDataContext db, int autonum)
    {
        return Get(db, new List<int> { autonum }).FirstOrDefault();
    }

    public static Pack Get(int autonum, Pack.StatusX status)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(db, autonum, status);
    }

    public static Pack Get(int autonum, List<Pack.StatusX> statusList)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(db, new List<int> { autonum }, statusList).FirstOrDefault();
    }

    public static Pack Get(RedBloodDataContext db, int autonum, Pack.StatusX status)
    {
        return Get(db, new List<int> { autonum }, new List<Pack.StatusX> { status }).FirstOrDefault();
    }

    public static List<Pack> Get(List<int> autonumList)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(db, autonumList);
    }

    public static List<Pack> Get(RedBloodDataContext db, List<int> autonumList)
    {
        return Get(db, autonumList, new List<Pack.StatusX> { Pack.StatusX.All });
    }

    public static List<Pack> Get(List<int> autonumList, List<Pack.StatusX> statusList)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(db, autonumList, statusList);
    }

    public static List<Pack> Get(RedBloodDataContext db, List<int> autonumList, List<Pack.StatusX> statusList)
    {
        return Get(db, autonumList, statusList, false);
    }

    public static List<Pack> Get(RedBloodDataContext db, List<int> autonumList, List<Pack.StatusX> statusList, bool allowPackErr)
    {
        if (autonumList.Count == 0) return new List<Pack>();

        List<Pack> pList;

        if (statusList.Count() == 1 && statusList[0] == Pack.StatusX.All)
        {
            pList = db.Packs.Where(e => autonumList.Contains(e.Autonum)).ToList();
        }
        else
        {
            pList = db.Packs.Where(e => autonumList.Contains(e.Autonum) && statusList.Contains(e.Status)).ToList();
        }

        if (allowPackErr) return pList;
        else
        {
            PackErr err = Validate(pList);
            if (err == PackErrList.Non) return pList;
        }

        return new List<Pack>();
    }

    public static void LoadExtractInfo(Pack p)
    {
        if (p == null) return;

        p.CanExtractToList = new List<int>();
        p.CanExtractToRBC = -1;
        p.CanExtractToWBC = -1;
        p.CanExtractToPlatelet = -1;
        p.CanExtractToFFPlasma = -1;
        p.CanExtractToFFPlasma_Poor = -1;
        p.CanExtractToFactorVIII = -1;

        if (p.Status == Pack.StatusX.Collected
            || p.Status == Pack.StatusX.Production
            || p.Status == Pack.StatusX.Produced)
        { }
        else return;

        if (p.DeliverStatus == Pack.DeliverStatusX.Yes)
        {
            p.Err = new PackErr(PackErrList.Invalid4Extract.Message + ".Túi máu: " + p.DeliverStatus);
            return;
        }

        if (p.TestResultStatus == Pack.TestResultStatusX.Positive
            || p.TestResultStatus == Pack.TestResultStatusX.PositiveLocked)
        {
            p.Err = new PackErr(PackErrList.Invalid4Extract.Message + ".Túi máu: " + p.TestResultStatus);
            return;
        }

        if (p.ComponentID == TestDef.Component.Full
            || p.ComponentID == TestDef.Component.FFPlasma)
        { }
        else return;

        //Load extract information
        List<Pack> l = p.ExtractedPacks;

        if (p.ComponentID == TestDef.Component.Full)
        {
            p.CanExtractToRBC = l.Where(r => r.ComponentID == TestDef.Component.RBC)
                .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

            p.CanExtractToWBC = l.Where(r => r.ComponentID == TestDef.Component.WBC)
                .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

            p.CanExtractToPlatelet = l.Where(r => r.ComponentID == TestDef.Component.Platelet)
                .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

            p.CanExtractToFFPlasma_Poor = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma_Poor)
                .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

            p.CanExtractToFFPlasma = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma)
                .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

            if (p.CanExtractToFFPlasma == 0
                && DateTime.Now - p.CollectedDate > SystemBLL.ExpTime4ProduceFFPlasma)
            {
                p.CanExtractToFFPlasma = -1;
            }
        }

        if (p.ComponentID == TestDef.Component.FFPlasma)
        {
            p.CanExtractToFactorVIII = l.Where(r => r.ComponentID == TestDef.Component.FactorVIII)
                .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

            p.CanExtractToFFPlasma_Poor = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma_Poor)
                .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();
        }

        if (p.CanExtractToRBC == 0) p.CanExtractToList.Add(TestDef.Component.RBC);
        if (p.CanExtractToWBC == 0) p.CanExtractToList.Add(TestDef.Component.WBC);
        if (p.CanExtractToPlatelet == 0) p.CanExtractToList.Add(TestDef.Component.Platelet);
        if (p.CanExtractToFFPlasma == 0) p.CanExtractToList.Add(TestDef.Component.FFPlasma);
        if (p.CanExtractToFFPlasma_Poor == 0) p.CanExtractToList.Add(TestDef.Component.FFPlasma_Poor);
        if (p.CanExtractToFactorVIII == 0) p.CanExtractToList.Add(TestDef.Component.FactorVIII);

        return;
    }

    public static Pack Get4Extract(int autonum)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return Get4Extract(db, autonum);
    }

    public static Pack Get4Extract(RedBloodDataContext db, int autonum)
    {
        Pack p = GetCarefully(db, autonum);

        LoadExtractInfo(p);

        return p;
    }

    public static Pack GetByCode(string code)
    {
        return Get(CodabarBLL.ParsePackAutoNum(code));
    }

    public static List<Pack> Get(RedBloodDataContext db, List<Pack.StatusX> status)
    {
        var rs = from c in db.Packs
                 where status.Contains(c.Status)
                 select c;

        return rs.ToList();
    }

    public static List<Pack> GetByCampaign(int campaignID)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return db.Packs.Where(r => r.CampaignID == campaignID
            && StatusListAllowEnterTestResult().Contains(r.Status)
            && AllowEnterTestResult().Contains(r.TestResultStatus)
            && r.ComponentID == TestDef.Component.Full
            ).ToList();
    }

    public static List<Pack> GetByCampaign(int campaignID, List<Pack.StatusX> status)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return db.Packs.Where(r => r.CampaignID == campaignID && status.Contains(r.Status)).ToList();
    }

    public static PackErr Assign(int autonum, Guid peopleID, int campaignID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack> l = (from c in db.Packs
                        where c.Autonum == autonum && c.PeopleID == null && c.CampaignID == null
                        select c).ToList();

        if (l.Count != 1)
        {
            return PackErrList.DataErr;
        }

        Pack p = l.First();

        //PackErr err = Validate(p);
        //if (!err.Equals(PackErrList.Non)) return err;

        try
        {

            p.PeopleID = peopleID;
            p.CollectedDate = DateTime.Now;
            p.CampaignID = campaignID;
            p.Substance = TestDefBLL.Get(db, TestDef.Substance.for21days);
            p.Component = TestDefBLL.Get(db, TestDef.Component.Full);
            UpdateExpiredDate(p);

            PackStatusHistory h = ChangeStatus(db, p, Pack.StatusX.Collected, "Assign peopleID=" + peopleID.ToString() + "&CampaignID=" + campaignID.ToString());

            db.SubmitChanges();

            CampaignBLL.SetStatus(campaignID);
        }
        catch (Exception ex)
        {
            return new PackErr(ex.Message);
        }

        return PackErrList.Non;
    }



    public static Pack[] New(RedBloodDataContext db, int count)
    {
        Pack[] l = new Pack[count];

        for (int i = 0; i < l.Length; i++)
        {
            l[i] = new Pack();
            l[i].Status = Pack.StatusX.Init;
            l[i].TestResultStatus = Pack.TestResultStatusX.Non;
            l[i].DeliverStatus = Pack.DeliverStatusX.Non;
        }

        db.Packs.InsertAllOnSubmit(l);

        return l;
    }

    public static Pack[] New(int count)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack[] l = New(db, count);
        db.Packs.InsertAllOnSubmit(l);


        db.SubmitChanges();
        return l;
    }



    public static Pack GetEnterPackByPeopleID(Guid peopleID)
    {
        return GetEnterPackByPeopleID(peopleID, null);
    }

    public static Pack GetEnterPackByPeopleID(Guid peopleID, PackErr err)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var e = from c in db.Packs
                where c.PeopleID == peopleID
                && (c.Status == Pack.StatusX.Collected)
                && (c.TestResultStatus == Pack.TestResultStatusX.Non)

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

    //public static Pack[] GetEnterPackErr()
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();
    //    DateTime expDate = DateTime.Now.Date.AddDays(1 - Resources.Setting.EnterPackExpire.ToInt());

    //    DateTime lim = LowerLimDate();
    //    var e = from c in db.Packs
    //            where (c.Status == Pack.StatusX.Assign || c.Status == Pack.StatusX.CommitReceived) && (c.CollectedDate < lim || c.CollectedDate >= DateTime.Now)
    //            orderby c.Status descending, c.CollectedDate descending
    //            select c;

    //    return e.ToArray();
    //}

    //public Pack CommitEnterPack(int autonum, bool withABO, string actor)
    //{
    //    if (autonum == 0) return null;

    //    RedBloodDataContext db = new RedBloodDataContext();

    //    Pack p = Get(autonum, db, Pack.StatusX.Assign);

    //    if (p == null
    //        || p.ComponentID == null || p.Volume == null) return p;

    //    if (withABO)
    //    {
    //        if (p.BloodTypes.Count != 1) return p;

    //        BloodType bt = p.BloodTypes[0];

    //        if (bt.aboID == null || bt.rhID == null) return p;

    //        bt.Actor = actor;
    //        bt.CommitDate = DateTime.Now;
    //    }
    //    else
    //    {

    //    }

    //    Pack.StatusX from = p.Status;

    //    p.CollectedDate = DateTime.Now;
    //    p.Status = Pack.StatusX.CommitReceived;
    //    p.Actor = actor;

    //    db.PackStatusHistories.InsertOnSubmit(new PackStatusHistory(p, from, p.Status, actor, "Commit Received"));

    //    db.SubmitChanges();

    //    return p;
    //}

    public static PackStatusHistory ChangeStatus(RedBloodDataContext db, Pack p, Pack.StatusX to, string note)
    {
        return ChangeStatus(db, p, to, RedBloodSystem.CurrentActor, note);
    }

    public static PackStatusHistory ChangeStatus(RedBloodDataContext db, Pack p, Pack.StatusX to, string actor, string note)
    {
        if (p.Status == to) return null;

        Pack.StatusX from = p.Status;

        p.Status = to;

        PackStatusHistory h = new PackStatusHistory(p, from, to, actor, note);

        db.PackStatusHistories.InsertOnSubmit(h);

        return h;
    }

    public static PackErr DeletePack(int? campaignID, int autonum, string note)
    {
        if (autonum == 0) return null;

        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get(db, new List<int> { autonum }, new List<Pack.StatusX> { Pack.StatusX.All }, true).FirstOrDefault();

        if (p == null) return PackErrList.NonExist;

        if (campaignID != null)
        {
            if (p.CampaignID != campaignID) return PackErrList.NonExistInCam;
        }

        PackStatusHistory h = ChangeStatus(db, p, Pack.StatusX.Delete, note);

        db.SubmitChanges();

        return null;
    }

    //Only pack has status 0 can be remove, to re-assign to another people.
    public static Pack RemovePeople(int autonum)
    {
        if (autonum == 0) return null;

        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get(db, autonum, Pack.StatusX.Collected);

        if (p == null && p.PeopleID != null) return p;
        if (p.TestResultStatus != Pack.TestResultStatusX.Non) return p;

        //remove people
        p.PeopleID = null;
        p.CollectedDate = null;
        p.CampaignID = null;

        PackStatusHistory h = ChangeStatus(db, p, Pack.StatusX.Init, "Remove peopleID=" + p.PeopleID.ToString() + "&CampaignID=" + p.CampaignID.ToString());
        db.PackStatusHistories.InsertOnSubmit(h);

        db.SubmitChanges();

        return p;
    }

    public static PackErr ValidateAndUpdateStatus(RedBloodDataContext db, Pack p)
    {
        return ValidateAndUpdateStatus(db, p, RedBloodSystem.CurrentActor);
    }

    public static PackErr ValidateAndUpdateStatus(RedBloodDataContext db, Pack p, string actor)
    {
        PackErr err = Validate(p);

        if (err != PackErrList.Non)
        {
            PackStatusHistory h = PackBLL.ChangeStatus(db, p, err.ToStatusX, actor, err.Message);
        }

        return err;
    }


    public static PackErr Validate(List<Pack> pList)
    {
        foreach (Pack p in pList)
        {
            PackErr err = Validate(p);
            if (err != PackErrList.Non)
                return err;
        }
        return PackErrList.Non;
    }

    public static PackErr Validate(Pack p)
    {
        if (p == null) return PackErrList.Non;

        if (p.Status == Pack.StatusX.Expire
            || p.Status == Pack.StatusX.Delete)
        {
            return PackErrList.Non;
        }

        if (p.Status == Pack.StatusX.Init)
        {
            if (p.PeopleID != null
                || p.CollectedDate != null
                || p.CampaignID != null)
                return PackErrList.DataErr;
        }
        else if (p.ComponentID == TestDef.Component.Full
            || p.ComponentID == TestDef.Component.PlateletApheresis)
        {
            if (p.PeopleID == null
                || p.CampaignID == null
                || p.CollectedDate == null
                || p.CollectedDate >= DateTime.Now)
                return PackErrList.DataErr;
        }

        if (p.Status == Pack.StatusX.Collected)
        {
            if (p.ComponentID != TestDef.Component.Full
                && p.ComponentID != TestDef.Component.PlateletApheresis)
                return PackErrList.DataErr;
        }

        if (p.Status == Pack.StatusX.Production)
        {
            int count = p.SourcePacks.Count();

            if (count > 0)
            {
                return PackErrList.DataErr;
            }
        }

        if (p.Status == Pack.StatusX.Produced)
        {
            int count = p.ExtractedPacks.Count();

            if (count > 0)
            {
                return PackErrList.DataErr;
            }
        }

        //Check expired
        return CheckExpire(p);
    }

    public static PackErr CheckExpire(Pack p)
    {
        if (p.DeliverStatus == Pack.DeliverStatusX.Yes
            || p.Status == Pack.StatusX.Produced)
            return PackErrList.Non;

        if (p.Component != null)
        {
            if (p.CollectedDate == null
            || p.CollectedDate >= DateTime.Now
                || p.ExtractedPacks == null)
            {
                return PackErrList.DataErr;
            }
        }

        if (DateTime.Now >= p.ExpiredDate)
        {
            return PackErrList.Expired;
        }

        return PackErrList.Non;
    }

    public static List<Pack> Get4Rpt(int campaignID, ReportType rptType)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var v = from r in db.Packs
                where r.CampaignID == campaignID && r.TestResultStatus != Pack.TestResultStatusX.Non
                select r;

        if (rptType == ReportType.NegInCam)
        {
            return v.ToList().Where(r => r.NonNegativeTestResult().Count() == 0).ToList();
        }

        if (rptType == ReportType.FourPosInCam)
        {
            return v.ToList().Where(r =>
                r.NonNegativeTestResult().Count() > 0 &&
                r.HIVID == TestDef.HIV.Neg).ToList();
        }

        if (rptType == ReportType.HIVInCam)
        {
            return v.Where(r => r.HIVID == TestDef.HIV.Pos || r.HIVID == TestDef.HIV.NA).ToList();
        }

        return new List<Pack>();
    }

    public static List<Pack> GetSourcePacks_AllLevel(Pack p)
    {
        List<Pack> l = p.SourcePacks;

        Pack[] temp = new Pack[l.Count];
        l.CopyTo(temp);

        foreach (Pack item in temp)
        {
            l.AddRange(GetSourcePacks_AllLevel(item));
        }

        l.Add(p);
        return l.Distinct().ToList();
    }

    public static List<Pack> GetExtractPacks_AllLevel(Pack p)
    {
        List<Pack> l = p.ExtractedPacks;

        Pack[] temp = new Pack[l.Count];
        l.CopyTo(temp);

        foreach (Pack item in temp)
        {
            l.AddRange(GetExtractPacks_AllLevel(item));
        }

        l.Add(p);
        return l.Distinct().ToList();
    }

    public static List<Pack> GetRelatedPacks_AllLevel(Pack p)
    {
        List<Pack> l = new List<Pack>();
        l.AddRange(GetSourcePacks_AllLevel(p));
        l.AddRange(GetExtractPacks_AllLevel(p));
        return l.Distinct().ToList();
    }

    public static PackErr Update(RedBloodDataContext db, Pack p, int componentID, int? volume, int substanceID)
    {
        if (p == null) return PackErrList.NonExist;

        p.Component = TestDefBLL.Get(db, componentID);
        p.Volume = volume;
        p.Substance = TestDefBLL.Get(db, substanceID);

        return PackErrList.Non;
    }

    public static PackErr Update(RedBloodDataContext db, Pack p, int times, int ABOID, int RhID, string note)
    {
        if (p == null || !p.CanUpdateTestResult) return PackErrList.NonExist;

        if (p.ABOID != ABOID)
        {
            p.ABO = TestDefBLL.Get(db, ABOID);
            PackResultHistoryBLL.Insert(db, p, ABOID, times, RedBloodSystem.CurrentActor, note);
        }

        if (p.RhID != RhID)
        {
            p.Rh = TestDefBLL.Get(db, RhID);
            PackResultHistoryBLL.Insert(db, p, RhID, times, RedBloodSystem.CurrentActor, note);
        }

        return PackErrList.Non;
    }

    public static PackErr Update(RedBloodDataContext db, Pack p, int times,
       int HIVID, int HCVID, int HBsAgID, int SyphilisID, int MalariaID,
        string note)
    {
        if (p == null || !p.CanUpdateTestResult) return PackErrList.NonExist;

        if (p.HCVID != HIVID)
        {
            p.HIV = TestDefBLL.Get(db, HIVID);
            PackResultHistoryBLL.Insert(db, p, HIVID, times, RedBloodSystem.CurrentActor, note);
        }

        if (p.HCVID != HCVID)
        {
            p.HCV = TestDefBLL.Get(db, HCVID);
            PackResultHistoryBLL.Insert(db, p, HCVID, times, RedBloodSystem.CurrentActor, note);
        }

        if (p.HBsAgID != HBsAgID)
        {
            p.HBsAg = TestDefBLL.Get(db, HBsAgID);
            PackResultHistoryBLL.Insert(db, p, HBsAgID, times, RedBloodSystem.CurrentActor, note);
        }

        if (p.SyphilisID != SyphilisID)
        {
            p.Syphilis = TestDefBLL.Get(db, SyphilisID);
            PackResultHistoryBLL.Insert(db, p, SyphilisID, times, RedBloodSystem.CurrentActor, note);
        }

        if (p.MalariaID != MalariaID)
        {
            p.Malaria = TestDefBLL.Get(db, MalariaID);
            PackResultHistoryBLL.Insert(db, p, MalariaID, times, RedBloodSystem.CurrentActor, note);
        }

        return PackErrList.Non;
    }

    public void Update(string collector, List<int> autonumList)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack> l = Get(db, autonumList);

        foreach (Pack item in l)
        {
            item.Collector = collector;
        }

        db.SubmitChanges();
    }

    public static void UpdateExpiredDate(Pack p)
    {
        if (p == null
            || p.Status == Pack.StatusX.Init
            || p.Status == Pack.StatusX.Delete
            || p.Status == Pack.StatusX.Expire
            || p.DeliverStatus == Pack.DeliverStatusX.Yes
            || p.CollectedDate == null)
        {
            return;
        }

        TimeSpan ts = SystemBLL.GetExpire(p);

        p.ExpiredDate = p.CollectedDate.Value.Add(ts);
    }

    public static void UpdateTestResultStatus4Full(int autonum)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Pack p = PackBLL.Get(db, autonum);

        if (p == null
            || !PackBLL.AllowEnterTestResult().Contains(p.TestResultStatus)
            || p.ComponentID == null
            || p.ComponentID != TestDef.Component.Full)
            return;

        if (p.Volume == null
            || p.ABOID == null || p.RhID == null
            || p.HIVID == null
            || p.HCVID == null
            || p.HBsAgID == null
            || p.SyphilisID == null
            || p.MalariaID == null)
        {
            p.TestResultStatus = Pack.TestResultStatusX.Non;
        }
        else
        {
            try
            {
                List<TestDef> l = p.NonNegativeTestResult();
                if (l.Count == 0)
                    p.TestResultStatus = Pack.TestResultStatusX.Negative;
                else p.TestResultStatus = Pack.TestResultStatusX.Positive;
            }
            catch (Exception)
            {
                p.TestResultStatus = Pack.TestResultStatusX.Non;
            }
        }

        //Update for all related packs
        UpdateTestResultStatus4Extracts(db, p);

        db.SubmitChanges();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="db">This parameter does nothing in the function. It is used to alert that the function depend on DataContext to submits.</param>
    /// <param name="srcP"></param>
    public static void UpdateTestResultStatus4Extracts(RedBloodDataContext db, Pack srcP)
    {
        List<Pack> extractP = srcP.PackExtractsBySource.Select(r => r.ExtractPack).ToList();

        foreach (Pack item in extractP)
        {
            item.TestResultStatus = item.TestResultStatusRoot;
            UpdateTestResultStatus4Extracts(db, item);
        }
    }

    public static void UpdateTestResultStatus4Extracts(int autonum)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = PackBLL.Get(db, autonum);

        UpdateTestResultStatus4Extracts(db, p);

        db.SubmitChanges();
    }

    public static PackErr Extract(int autonum, List<int> to)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get4Extract(db, autonum);

        p.CanExtractToList.Join(to, r => r, t => t, (r, t) => r);



        if (to.Count == 0) return PackErrList.SelectNoExtract;
        if (p.CanExtractToList.Count == 0) return PackErrList.Invalid4Extract;

        int count = 0;
        foreach (int item in to)
        {
            if (p.CanExtractToList.Contains(item))
            {
                //Extract
                Pack extractP = PackBLL.New(db, 1).First();
                if (extractP == null) return PackErrList.DataErr;

                db.SubmitChanges();

                extractP.Component = TestDefBLL.Get(db, item);
                extractP.Actor = RedBloodSystem.CurrentActor;
                //extractP.TestResultStatus = extractP.TestResultStatusRoot;
                extractP.CollectedDate = DateTime.Now;

                PackStatusHistory h = ChangeStatus(db, extractP, Pack.StatusX.Production, "Extract");


                PackExtract pe = new PackExtract();
                pe.SourcePackID = p.ID;
                pe.ExtractPackID = extractP.ID;
                db.PackExtracts.InsertOnSubmit(pe);

                count++;
            }
        }

        if (count > 0)
        {
            PackStatusHistory hi = ChangeStatus(db, p, Pack.StatusX.Produced, "Extract");

            db.SubmitChanges();

            PackBLL.UpdateTestResultStatus4Extracts(p.Autonum);
            return PackErrList.Non;
        }
        else
        {
            return PackErrList.DataErr;
        }


    }

    public static PackErr Combine2Platelet(List<int> autonumListIn, int autonumOut, string note)
    {
        List<Pack> pInList = new List<Pack>();
        foreach (int item in autonumListIn)
        {
            Pack p = Get4Combined2Platelet(item);
            if (p.Err == PackErrList.Valid4Platelet)
                pInList.Add(p);
            else
                return p.Err;
        }

        RedBloodDataContext db = new RedBloodDataContext();
        Pack pOut = Get4Combined2Platelet(db, autonumOut);
        if (pOut.Err != PackErrList.Init4Platelet)
            return pOut.Err;

        if (autonumListIn.Count != pInList.Count
            || pOut == null)
        {
            return PackErrList.Invalid4Platelet;
        }

        pOut.ComponentID = TestDef.Component.Platelet;
        pOut.CollectedDate = DateTime.Now;
        pOut.Note = note;

        PackStatusHistory h = ChangeStatus(db, pOut, Pack.StatusX.Production, "Combine2Platelet");


        foreach (Pack item in pInList)
        {
            PackExtract pe = new PackExtract();
            pe.SourcePackID = item.ID;
            pe.ExtractPackID = pOut.ID;
            db.PackExtracts.InsertOnSubmit(pe);
        }

        db.SubmitChanges();

        return PackErrList.Non;
    }

    public static Pack Get4Combined2Platelet(int autonum)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get4Combined2Platelet(db, autonum);
    }

    public static Pack Get4Combined2Platelet(RedBloodDataContext db, int autonum)
    {
        Pack p = GetCarefully(db, autonum);

        //if (p == null) throw new Exception("GetCarefully() catch Exception");

        //if (p.Err != PackErrList.Non)
        //{
        //    return p;
        //}

        //if (p.Status == Pack.StatusX.Init)
        //{
        //    p.Err = PackErrList.Init4Platelet;
        //    return p;
        //}

        //if (p.ComponentID == (int)TestDef.Component.Platelet)
        //{
        //    p.Err = PackErrList.IsPlatelet;
        //    return p;
        //}



        //if (p.ComponentID == (int)TestDef.Component.Full)
        //{
        //    int count = p.PackExtractsBySource
        //        .Where(r =>
        //            r.ExtractPack.ComponentID == (int)TestDef.Component.Platelet)
        //        .Count();

        //    if (count == 1)
        //    {
        //        p.Err = PackErrList.Combined2Platelet;
        //        return p;
        //    }

        //    if (count == 0)
        //    {
        //        if (!StatusList4Production().Contains(p.Status))
        //        {
        //            p.Err = new PackErr("Túi máu: " + p.Status);
        //            return p;
        //        }

        //        p.Err = PackErrList.Valid4Platelet;
        //        return p;
        //    }

        //    if (count > 1)
        //    {
        //        p.Err = PackErrList.DataErr;
        //        return p;
        //    }
        //}

        //p.Err = new PackErr(PackErrList.Invalid4Platelet.Message + " " + p.Component.Name);
        return p;
    }

    public static Pack IsExtracted(int autonum)
    {
        Pack p = Get(autonum);

        if (p == null) return null;

        if (p.ComponentID == TestDef.Component.Full)
        {
            if (p.PackExtractsBySource
                .Where(r =>
                    r.ExtractPack.ComponentID == TestDef.Component.RBC
                    || r.ExtractPack.ComponentID == TestDef.Component.FFPlasma
                    )
                .Count() > 0)
                return p;
        }

        if (p.ComponentID == TestDef.Component.RBC
            || p.ComponentID == TestDef.Component.FFPlasma
            )
        {
            return p;
        }

        return null;
    }

    /// <summary>
    /// Lock All Test Result regarless of time
    /// </summary>
    public static void LockEnterTestResult()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        //This tricky code help load static const in class TestDef.
        TestDef td = new TestDef();

        List<Pack> l = db.Packs.Where(r =>
            (r.TestResultStatus == Pack.TestResultStatusX.Negative
            || r.TestResultStatus == Pack.TestResultStatusX.Positive)
            && r.ComponentID == TestDef.Component.Full
            ).ToList();


        foreach (Pack item in l)
        {
            if (item.TestResultStatus == Pack.TestResultStatusX.Negative)
                item.TestResultStatus = Pack.TestResultStatusX.NegativeLocked;

            if (item.TestResultStatus == Pack.TestResultStatusX.Positive)
                item.TestResultStatus = Pack.TestResultStatusX.PositiveLocked;

            //Update for all related packs
            UpdateTestResultStatus4Extracts(db, item);
        }
        db.SubmitChanges();
    }
}

