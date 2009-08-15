using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PackBLL
/// </summary>
public class PackBLL
{

    public PackBLL()
    {
    }

    /// <summary>
    /// Return the list of pack status which pack had entered test result
    /// </summary>
    /// <returns></returns>
    public static Pack.StatusX[] StatusListAllowEnterTestResult()
    {
        return new Pack.StatusX[] { };
        //return new Pack.StatusX[] { Pack.StatusX.Collected, Pack.StatusX.Produced };
    }

    //public static Pack.StatusX[] StatusList4Production()
    //{
    //    return new Pack.StatusX[] { Pack.StatusX.Collected, Pack.StatusX.EnterTestResult, Pack.StatusX.CommitTestResult };
    //}

    public static Pack.StatusX[] StatusList4Order()
    {
        //return new Pack.StatusX[] { Pack.StatusX.Collected, Pack.StatusX.Product };
        return new Pack.StatusX[] { };
    }

    /// <summary>
    /// Return the list of pack status which pack had entered test result
    /// </summary>
    /// <returns></returns>
    //public static Pack.TestResultStatusX[] AllowEnterTestResult()
    //{
    //    return new Pack.StatusX[] { };
    //    //return new Pack.TestResultStatusX[] { Pack.TestResultStatusX.Non, 
    //    //    Pack.TestResultStatusX.Negative, 
    //    //    Pack.TestResultStatusX.Positive};
    //}

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
            pErr.Err = PackErrEnum.NonExist;
            return pErr;
        }

        PackErr err = ValidateAndUpdateStatus(db, p);
        if (err != PackErrEnum.Non)
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

    public static Pack Get(string DIN, string productCode)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return db.Packs.Where(r => r.DIN == DIN && r.ProductCode == productCode).FirstOrDefault();
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

        List<Pack> pList = new List<Pack>();

        //if (statusList.Count() == 1 && statusList[0] == Pack.StatusX.All)
        //{
        //    pList = db.Packs.Where(e => autonumList.Contains(e.Autonum)).ToList();
        //}
        //else
        //{
        //    pList = db.Packs.Where(e => autonumList.Contains(e.Autonum) && statusList.Contains(e.Status)).ToList();
        //}

        if (allowPackErr) return pList;
        else
        {
            PackErr err = Validate(pList);
            if (err == PackErrEnum.Non) return pList;
        }

        return new List<Pack>();
    }

    public static void LoadExtractInfo(Pack p)
    {
        if (p == null) return;

        //p.CanExtractToList = new List<int>();
        //p.CanExtractToRBC = -1;
        //p.CanExtractToWBC = -1;
        //p.CanExtractToPlatelet = -1;
        //p.CanExtractToFFPlasma = -1;
        //p.CanExtractToFFPlasma_Poor = -1;
        //p.CanExtractToFactorVIII = -1;

        //if (p.Status == Pack.StatusX.Collected
        //    || p.Status == Pack.StatusX.Product
        //    || p.Status == Pack.StatusX.Produced)
        //{ }
        //else return;

        //if (p.DeliverStatus == Pack.DeliverStatusX.Yes)
        //{
        //    p.Err = new PackErr(PackErrEnum.Invalid4Extract.Message + ".Túi máu: " + p.DeliverStatus);
        //    return;
        //}

        //if (p.TestResultStatus == Pack.TestResultStatusX.Positive
        //    || p.TestResultStatus == Pack.TestResultStatusX.PositiveLocked)
        //{
        //    p.Err = new PackErr(PackErrEnum.Invalid4Extract.Message + ".Túi máu: " + p.TestResultStatus);
        //    return;
        //}

        //if (p.ComponentID == TestDef.Component.Full
        //    || p.ComponentID == TestDef.Component.FFPlasma)
        //{ }
        //else return;

        ////Load extract information
        //List<Pack> l = p.ExtractedPacks;
        //p.Err = PackErrEnum.Valid4Extract;

        //if (p.ComponentID == TestDef.Component.Full)
        //{
        //    p.CanExtractToRBC = l.Where(r => r.ComponentID == TestDef.Component.RBC)
        //        .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

        //    p.CanExtractToWBC = l.Where(r => r.ComponentID == TestDef.Component.WBC)
        //        .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

        //    p.CanExtractToPlatelet = l.Where(r => r.ComponentID == TestDef.Component.Platelet)
        //        .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

        //    p.CanExtractToFFPlasma_Poor = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma_Poor)
        //        .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

        //    p.CanExtractToFFPlasma = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma)
        //        .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

        //    if (p.CanExtractToFFPlasma == 0
        //        && DateTime.Now - p.CollectedDate > SystemBLL.ExpTime4ProduceFFPlasma)
        //    {
        //        p.CanExtractToFFPlasma = -1;
        //    }

        //    if (p.CanExtractToFFPlasma_Poor > 0 && p.CanExtractToFFPlasma == 0)
        //        p.CanExtractToFFPlasma = -1;

        //    if (p.CanExtractToFFPlasma_Poor == 0 && p.CanExtractToFFPlasma > 0)
        //        p.CanExtractToFFPlasma_Poor = -1;
        //}

        //if (p.ComponentID == TestDef.Component.FFPlasma)
        //{
        //    p.CanExtractToFactorVIII = l.Where(r => r.ComponentID == TestDef.Component.FactorVIII)
        //        .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();

        //    p.CanExtractToFFPlasma_Poor = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma_Poor)
        //        .Select(r => r.Autonum).DefaultIfEmpty(0).FirstOrDefault();
        //}

        //if (p.CanExtractToRBC == 0) p.CanExtractToList.Add(TestDef.Component.RBC);
        //if (p.CanExtractToWBC == 0) p.CanExtractToList.Add(TestDef.Component.WBC);
        //if (p.CanExtractToPlatelet == 0) p.CanExtractToList.Add(TestDef.Component.Platelet);
        //if (p.CanExtractToFFPlasma == 0) p.CanExtractToList.Add(TestDef.Component.FFPlasma);
        //if (p.CanExtractToFFPlasma_Poor == 0) p.CanExtractToList.Add(TestDef.Component.FFPlasma_Poor);
        //if (p.CanExtractToFactorVIII == 0) p.CanExtractToList.Add(TestDef.Component.FactorVIII);

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

    public static List<Pack> Get4Extract(List<int> autonumList)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack> l = new List<Pack>();

        foreach (int item in autonumList)
        {
            Pack p = Get4Extract(item);
            if (p != null)
                l.Add(p);
        }

        return l;
    }

    public static Pack GetByCode(string code)
    {
        return null;
        //return Get(BarcodeBLL.ParsePackAutoNum(code));
    }

    public static List<Pack> Get(RedBloodDataContext db, List<Pack.StatusX> status)
    {
        var rs = from c in db.Packs
                 where status.Contains(c.Status)
                 select c;

        return rs.ToList();
    }

    //public static List<Pack> GetByCampaign(int campaignID)
    //{
    //    //RedBloodDataContext db = new RedBloodDataContext();
    //    //return db.Packs.Where(r => r.CampaignID == campaignID
    //    //    && StatusListAllowEnterTestResult().Contains(r.Status)
    //    //    && AllowEnterTestResult().Contains(r.TestResultStatus)
    //    //    && r.ComponentID == TestDef.Component.Full
    //    //    ).ToList();
    //    return new List<Pack>();
    //}

    //public static List<Pack> GetByCampaign(int campaignID, List<Pack.StatusX> status)
    //{
    //    //RedBloodDataContext db = new RedBloodDataContext();
    //    //return db.Packs.Where(r => r.CampaignID == campaignID && status.Contains(r.Status)).ToList();
    //    return new List<Pack>();

    //}

    //public static PackErr Assign(int autonum, Guid peopleID, int campaignID)
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    List<Pack> l = (from c in db.Packs
    //                    where c.Autonum == autonum && c.PeopleID == null && c.CampaignID == null
    //                    select c).ToList();

    //    if (l.Count != 1)
    //    {
    //        return PackErrList.DataErr;
    //    }

    //    Pack p = l.First();

    //    //PackErr err = Validate(p);
    //    //if (!err.Equals(PackErrList.Non)) return err;

    //    try
    //    {

    //        p.PeopleID = peopleID;
    //        p.CollectedDate = DateTime.Now;
    //        p.CampaignID = campaignID;
    //        p.Substance = TestDefBLL.Get(db, TestDef.Substance.for21days);
    //        p.Component = TestDefBLL.Get(db, TestDef.Component.Full);
    //        UpdateExpiredDate(p);

    //        PackStatusHistory h = ChangeStatus(db, p, Pack.StatusX.Collected, "Assign peopleID=" + peopleID.ToString() + "&CampaignID=" + campaignID.ToString());

    //        db.SubmitChanges();

    //        CampaignBLL.SetStatus(campaignID);
    //    }
    //    catch (Exception ex)
    //    {
    //        return new PackErr(ex.Message);
    //    }

    //    return PackErrList.Non;
    //}



    //public static Pack[] New(RedBloodDataContext db, int count)
    //{
    //    Pack[] l = new Pack[count];

    //    for (int i = 0; i < l.Length; i++)
    //    {
    //        l[i] = new Pack();
    //        l[i].Status = Pack.StatusX.Init;
    //        l[i].TestResultStatus = Pack.TestResultStatusX.Non;
    //        l[i].DeliverStatus = Pack.DeliverStatusX.Non;
    //    }

    //    db.Packs.InsertAllOnSubmit(l);

    //    return l;
    //}

    //public static Pack[] New(int count)
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    Pack[] l = New(db, count);
    //    db.Packs.InsertAllOnSubmit(l);


    //    db.SubmitChanges();
    //    return l;
    //}

    //public static Pack GetEnterPackByPeopleID(Guid peopleID)
    //{
    //    return GetEnterPackByPeopleID(peopleID, null);
    //}

    //public static Pack GetEnterPackByPeopleID(Guid peopleID, PackErr err)
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    var e = from c in db.Packs
    //            where c.PeopleID == peopleID
    //            && (c.Status == Pack.StatusX.Collected)
    //            && (c.TestResultStatus == Pack.TestResultStatusX.Non)

    //            orderby c.Status descending, c.CollectedDate descending
    //            select c;

    //    if (e.Count() == 1)
    //    {
    //        Pack p = e.First();

    //        if (Validate(p) != null)
    //            return p;
    //        else
    //            return null;
    //    }

    //    if (e.Count() > 1)
    //    {
    //        err = PackErrList.EnterPackMulti;
    //    }

    //    return null;
    //}

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

    //public static PackStatusHistory ChangeStatus(RedBloodDataContext db, Pack p, Pack.StatusX to, string note)
    //{
    //    return ChangeStatus(db, p, to, RedBloodSystem.CurrentActor, note);
    //}

    //public static PackStatusHistory ChangeStatus(RedBloodDataContext db, Pack p, Pack.StatusX to, string actor, string note)
    //{
    //    if (p.Status == to) return null;

    //    Pack.StatusX from = p.Status;

    //    p.Status = to;

    //    PackStatusHistory h = new PackStatusHistory(p, from, to, actor, note);

    //    db.PackStatusHistories.InsertOnSubmit(h);

    //    return h;
    //}

    public static PackErr DeletePack(int? campaignID, int autonum, string note)
    {
        if (autonum == 0) return null;

        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get(db, new List<int> { autonum }, new List<Pack.StatusX> { Pack.StatusX.All }, true).FirstOrDefault();

        if (p == null) return PackErrEnum.NonExist;

        //if (campaignID != null)
        //{
        //    if (p.CampaignID != campaignID) return PackErrEnum.NonExistInCam;
        //}

        //PackStatusHistory h = ChangeStatus(db, p, Pack.StatusX.Delete, note);

        db.SubmitChanges();

        return null;
    }

    //Only pack has status 0 can be remove, to re-assign to another people.
    public static Pack RemovePeople(int autonum)
    {
        if (autonum == 0) return null;

        RedBloodDataContext db = new RedBloodDataContext();

        //Pack p = Get(db, autonum, Pack.StatusX.Collected);
        Pack p = new Pack();

        //if (p == null && p.PeopleID != null) return p;
        //if (p.TestResultStatus != Pack.TestResultStatusX.Non) return p;

        ////remove people
        //p.PeopleID = null;
        //p.CollectedDate = null;
        //p.CampaignID = null;

        //PackStatusHistory h = ChangeStatus(db, p, Pack.StatusX.Init, "Remove peopleID=" + p.PeopleID.ToString() + "&CampaignID=" + p.CampaignID.ToString());
        //db.PackStatusHistories.InsertOnSubmit(h);

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

        //if (err != PackErrEnum.Non)
        //{
        //    PackStatusHistory h = PackBLL.ChangeStatus(db, p, err.ToStatusX, actor, err.Message);
        //}

        return err;
    }


    public static PackErr Validate(List<Pack> pList)
    {
        foreach (Pack p in pList)
        {
            PackErr err = Validate(p);
            if (err != PackErrEnum.Non)
                return err;
        }
        return PackErrEnum.Non;
    }

    public static PackErr Validate(Pack p)
    {
        if (p == null) return PackErrEnum.Non;

        if (p.Status == Pack.StatusX.Expired
            || p.Status == Pack.StatusX.Delete)
        {
            return PackErrEnum.Non;
        }

        //if (p.Status == Pack.StatusX.Init)
        //{
        //    if (p.PeopleID != null
        //        || p.CollectedDate != null
        //        || p.CampaignID != null)
        //        return PackErrEnum.DataErr;
        //}
        //else if (p.ComponentID == TestDef.Component.Full
        //    || p.ComponentID == TestDef.Component.PlateletApheresis)
        //{
        //    if (p.PeopleID == null
        //        || p.CampaignID == null
        //        || p.CollectedDate == null
        //        || p.CollectedDate >= DateTime.Now)
        //        return PackErrEnum.DataErr;
        //}

        //if (p.Status == Pack.StatusX.Collected)
        //{
        //    if (p.ComponentID != TestDef.Component.Full
        //        && p.ComponentID != TestDef.Component.PlateletApheresis)
        //        return PackErrEnum.DataErr;
        //}

        //if (p.Status == Pack.StatusX.Product)
        //{
        //    if (p.SourcePacks.Count() <= 0
        //        || p.ExtractedPacks.Count() > 0)
        //    {
        //        return PackErrEnum.DataErr;
        //    }
        //}

        //if (p.Status == Pack.StatusX.Produced)
        //{
        //    if (p.ExtractedPacks.Count() <= 0)
        //    {
        //        return PackErrEnum.DataErr;
        //    }
        //}

        //Check expired
        return CheckExpire(p);
    }

    public static PackErr CheckExpire(Pack p)
    {
        //if (p.DeliverStatus == Pack.DeliverStatusX.Yes
        //    || p.Status == Pack.StatusX.Produced)
        //    return PackErrEnum.Non;

        //if (p.Component != null)
        //{
        //    if (p.CollectedDate == null
        //    || p.CollectedDate >= DateTime.Now
        //        || p.ExtractedPacks == null)
        //    {
        //        return PackErrEnum.DataErr;
        //    }
        //}

        //if (DateTime.Now >= p.ExpiredDate)
        //{
        //    return PackErrEnum.Expired;
        //}

        return PackErrEnum.Non;
    }

    public static List<Pack> Get4Rpt(int campaignID, ReportType rptType)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        //var v = from r in db.Packs
        //        where r.CampaignID == campaignID && r.TestResultStatus != Pack.TestResultStatusX.Non
        //        select r;

        //if (rptType == ReportType.NegInCam)
        //{
        //    return v.ToList().Where(r => r.NonNegativeTestResult().Count() == 0).ToList();
        //}

        //if (rptType == ReportType.FourPosInCam)
        //{
        //    return v.ToList().Where(r =>
        //        r.NonNegativeTestResult().Count() > 0 &&
        //        r.HIVID == TestDef.HIV.Neg).ToList();
        //}

        //if (rptType == ReportType.HIVInCam)
        //{
        //    return v.Where(r => r.HIVID == TestDef.HIV.Pos || r.HIVID == TestDef.HIV.NA).ToList();
        //}

        return new List<Pack>();
    }



    public static PackErr Update(RedBloodDataContext db, Pack p, int componentID, int? volume, int substanceID)
    {
        if (p == null) return PackErrEnum.NonExist;

        //p.Component = TestDefBLL.Get(db, componentID);
        //p.Volume = volume;
        //p.Substance = TestDefBLL.Get(db, substanceID);

        return PackErrEnum.Non;
    }

    public static PackErr Update(RedBloodDataContext db, Pack p, int times, int ABOID, int RhID, string note)
    {
        //if (p == null || !p.CanUpdateTestResult) return PackErrEnum.NonExist;

        //if (p.ABOID != ABOID)
        //{
        //    p.ABO = TestDefBLL.Get(db, ABOID);
        //    PackResultHistoryBLL.Insert(db, p, ABOID, times, RedBloodSystem.CurrentActor, note);
        //}

        //if (p.RhID != RhID)
        //{
        //    p.Rh = TestDefBLL.Get(db, RhID);
        //    PackResultHistoryBLL.Insert(db, p, RhID, times, RedBloodSystem.CurrentActor, note);
        //}

        return PackErrEnum.Non;
    }

    //public static PackErr Update(RedBloodDataContext db, Pack p, int times,
    //   int HIVID, int HCVID, int HBsAgID, int SyphilisID, int MalariaID,
    //    string note)
    //{
    //    if (p == null || !p.CanUpdateTestResult) return PackErrEnum.NonExist;

    //    //if (p.HCVID != HIVID)
    //    //{
    //    //    p.HIV = TestDefBLL.Get(db, HIVID);
    //    //    PackResultHistoryBLL.Insert(db, p, HIVID, times, RedBloodSystem.CurrentActor, note);
    //    //}

    //    //if (p.HCVID != HCVID)
    //    //{
    //    //    p.HCV = TestDefBLL.Get(db, HCVID);
    //    //    PackResultHistoryBLL.Insert(db, p, HCVID, times, RedBloodSystem.CurrentActor, note);
    //    //}

    //    //if (p.HBsAgID != HBsAgID)
    //    //{
    //    //    p.HBsAg = TestDefBLL.Get(db, HBsAgID);
    //    //    PackResultHistoryBLL.Insert(db, p, HBsAgID, times, RedBloodSystem.CurrentActor, note);
    //    //}

    //    //if (p.SyphilisID != SyphilisID)
    //    //{
    //    //    p.Syphilis = TestDefBLL.Get(db, SyphilisID);
    //    //    PackResultHistoryBLL.Insert(db, p, SyphilisID, times, RedBloodSystem.CurrentActor, note);
    //    //}

    //    //if (p.MalariaID != MalariaID)
    //    //{
    //    //    p.Malaria = TestDefBLL.Get(db, MalariaID);
    //    //    PackResultHistoryBLL.Insert(db, p, MalariaID, times, RedBloodSystem.CurrentActor, note);
    //    //}

    //    return PackErrEnum.Non;
    //}

    public void Update(string collector, List<int> autonumList)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack> l = Get(db, autonumList);

        //foreach (Pack item in l)
        //{
        //    item.Collector = collector;
        //}

        db.SubmitChanges();
    }

    public static void UpdateExpiredDate(Pack p)
    {
        //if (p == null
        //    || p.ComponentID == null
        //    || p.Status == Pack.StatusX.Init
        //    || p.Status == Pack.StatusX.Delete
        //    || p.Status == Pack.StatusX.Expired
        //    || p.DeliverStatus == Pack.DeliverStatusX.Yes
        //    || p.CollectedDate == null)
        //{
        //    return;
        //}

        //TimeSpan ts = SystemBLL.GetExpire(p);

        //if (ts != TimeSpan.MinValue)
        //    p.ExpiredDate = p.CollectedDate.Value.Add(ts);
    }

    public static void UpdateTestResultStatus4Full(int autonum)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Pack p = PackBLL.Get(db, autonum);

        //if (p == null
        //    || !PackBLL.AllowEnterTestResult().Contains(p.TestResultStatus)
        //    || p.ComponentID == null
        //    || p.ComponentID != TestDef.Component.Full)
        //    return;

        //if (p.Volume == null
        //    || p.ABOID == null || p.RhID == null
        //    || p.HIVID == null
        //    || p.HCVID == null
        //    || p.HBsAgID == null
        //    || p.SyphilisID == null
        //    || p.MalariaID == null)
        //{
        //    p.TestResultStatus = Pack.TestResultStatusX.Non;
        //}
        //else
        //{
        //    try
        //    {
        //        List<TestDef> l = p.NonNegativeTestResult();
        //        if (l.Count == 0)
        //            p.TestResultStatus = Pack.TestResultStatusX.Negative;
        //        else p.TestResultStatus = Pack.TestResultStatusX.Positive;
        //    }
        //    catch (Exception)
        //    {
        //        p.TestResultStatus = Pack.TestResultStatusX.Non;
        //    }
        //}

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
        List<Pack> extractP = srcP.ExtractedPacks_All;

        //foreach (Pack item in extractP)
        //{
        //    item.TestResultStatus = item.TestResultStatusRoot;
        //}
    }

    public static void UpdateTestResultStatus4Extracts(int autonum)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = PackBLL.Get(db, autonum);

        UpdateTestResultStatus4Extracts(db, p);

        db.SubmitChanges();
    }

    public static PackErr Extract(List<int> autonumList, List<int> to)
    {
        foreach (int item in autonumList)
        {
            Extract(item, to);
        }

        return PackErrEnum.Non;
    }

    public static PackErr Extract(int autonum, List<int> to)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get4Extract(db, autonum);

        int count = 0;
        foreach (int item in p.CanExtractToList.Join(to, r => r, t => t, (r, t) => r))
        {
            //Extract
            //Pack extractP = PackBLL.New(db, 1).First();
            Pack extractP = new Pack();
            if (extractP == null) return PackErrEnum.DataErr;

            db.SubmitChanges();

            //extractP.Component = TestDefBLL.Get(db, item);
            //extractP.Actor = RedBloodSystem.CurrentActor;
            //extractP.CollectedDate = DateTime.Now;

            //PackStatusHistory h = ChangeStatus(db, extractP, Pack.StatusX.Product, "Extract");

            PackExtract pe = new PackExtract();
            pe.SourcePackID = p.ID;
            pe.ExtractPackID = extractP.ID;
            db.PackExtracts.InsertOnSubmit(pe);

            UpdateExpiredDate(extractP);

            count++;
        }

        if (count > 0)
        {
            //PackStatusHistory hi = ChangeStatus(db, p, Pack.StatusX.Produced, "Extract");

            //db.SubmitChanges();

            //PackBLL.UpdateTestResultStatus4Extracts(p.Autonum);
            return PackErrEnum.Non;
        }
        else
        {
            return PackErrEnum.DataErr;
        }
    }

    public static PackErr Combine2Platelet(List<int> autonumListIn, int autonumOut, string note)
    {
        List<Pack> pInList = new List<Pack>();
        foreach (int item in autonumListIn)
        {
            Pack p = Get4Combined2Platelet(item);
            if (p.Err == PackErrEnum.Valid4Platelet)
                pInList.Add(p);
            else
                return p.Err;
        }

        RedBloodDataContext db = new RedBloodDataContext();
        Pack pOut = Get4Combined2Platelet(db, autonumOut);
        if (pOut.Err != PackErrEnum.Init4Platelet)
            return pOut.Err;

        if (autonumListIn.Count != pInList.Count
            || pOut == null)
        {
            return PackErrEnum.Invalid4Platelet;
        }

        //pOut.ComponentID = TestDef.Component.Platelet;
        //pOut.CollectedDate = DateTime.Now;
        //pOut.Note = note;

        //PackStatusHistory h = ChangeStatus(db, pOut, Pack.StatusX.Product, "Combine2Platelet");


        foreach (Pack item in pInList)
        {
            PackExtract pe = new PackExtract();
            pe.SourcePackID = item.ID;
            pe.ExtractPackID = pOut.ID;
            db.PackExtracts.InsertOnSubmit(pe);
        }

        db.SubmitChanges();

        return PackErrEnum.Non;
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


    /// <summary>
    /// Lock All Test Result regarless of time
    /// </summary>
    public static void LockEnterTestResult()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        //This tricky code help load static const in class TestDef.
        TestDef td = new TestDef();

        //List<Pack> l = db.Packs.Where(r =>
        //    (r.TestResultStatus == Pack.TestResultStatusX.Negative
        //    || r.TestResultStatus == Pack.TestResultStatusX.Positive)
        //    && r.ComponentID == TestDef.Component.Full
        //    ).ToList();


        //foreach (Pack item in l)
        //{
        //    if (item.TestResultStatus == Pack.TestResultStatusX.Negative)
        //        item.TestResultStatus = Pack.TestResultStatusX.NegativeLocked;

        //    if (item.TestResultStatus == Pack.TestResultStatusX.Positive)
        //        item.TestResultStatus = Pack.TestResultStatusX.PositiveLocked;

        //    //Update for all related packs
        //    UpdateTestResultStatus4Extracts(db, item);
        //}
        db.SubmitChanges();
    }

    public static PackErr CreateOriginal(string DIN, string productCode, int defaultVolume)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Donation d = db.Donations.Where(r => r.DIN == DIN && r.PeopleID != null).FirstOrDefault();
        Product p = db.Products.Where(r => r.Code == productCode).FirstOrDefault();

        if (d == null || p == null) return PackErrEnum.DataErr;

        int countPack = db.Packs.Where(r => r.DIN == DIN && r.ProductCode == productCode).Count();

        if (countPack > 0) return PackErrEnum.Existed;


        //Check to see valid product code in collection
        //Code will be here

        if (d.OrgPackID != null) return PackErrEnum.DonationGotPack;

        Pack pack = new Pack();

        pack.DIN = DIN;
        pack.ProductCode = productCode;
        pack.Status = Pack.StatusX.Product;
        pack.Date = DateTime.Now;
        pack.Actor = RedBloodSystem.CurrentActor;

        if (d.Volume != null && d.Volume.Value > 0) return PackErrEnum.DataErr;

        if (d.Volume == null || d.Volume.Value < 0)
        {
            if (p.OriginalVolume != null && p.OriginalVolume.Value > 0)
            {
                d.Volume = p.OriginalVolume;
                pack.Volume = p.OriginalVolume;
            }
            else
            {
                if (defaultVolume > 0)
                {
                    d.Volume = defaultVolume;
                    pack.Volume = defaultVolume;
                }
            }
        }

        //Check to see if the pack is collector too late
        //Code check will be here.

        pack.ExpirationDate = DateTime.Now.Add(p.Duration.Value - RedBloodSystem.RootTime);

        db.Packs.InsertOnSubmit(pack);

        db.SubmitChanges();

        d.OrgPackID = pack.ID;
        db.SubmitChanges();

        return PackErrEnum.Non;

    }

    public static PackErr Create(string DIN, string productCode, int volume)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Donation d = db.Donations.Where(r => r.DIN == DIN && r.PeopleID != null).FirstOrDefault();
        Product p = db.Products.Where(r => r.Code == productCode).FirstOrDefault();

        if (d == null || p == null) return PackErrEnum.DataErr;

        int countPack = db.Packs.Where(r => r.DIN == DIN && r.ProductCode == productCode).Count();

        if (countPack > 0) return PackErrEnum.Existed;

        Pack pack = new Pack();

        pack.DIN = DIN;
        pack.ProductCode = productCode;
        pack.Status = Pack.StatusX.Product;
        pack.Date = DateTime.Now;
        pack.Actor = RedBloodSystem.CurrentActor;

        pack.Volume = volume;

        //Check to see if the pack is collector too late
        //Code check will be here.

        pack.ExpirationDate = DateTime.Now.Add(p.Duration.Value - RedBloodSystem.RootTime);

        db.Packs.InsertOnSubmit(pack);

        db.SubmitChanges();

        return PackErrEnum.Non;
    }
}

