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
        return new Pack.StatusX[] { Pack.StatusX.CommitTestResult, Pack.StatusX.Delivered };
    }

    public static Pack.StatusX[] StatusList4Production()
    {
        return new Pack.StatusX[] { Pack.StatusX.Assign, Pack.StatusX.EnterTestResult, Pack.StatusX.CommitTestResult };
    }

    public static Pack.StatusX[] StatusList4Order()
    {
        return new Pack.StatusX[] { Pack.StatusX.CommitTestResult, Pack.StatusX.Production };
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

    /// <summary>
    /// Get pack -> Validate -> Change status if need
    /// </summary>
    /// <param name="autonum"></param>
    /// <returns></returns>
    public static Pack GetCarefully(RedBloodDataContext db, int autonum, string actor)
    {
        Pack pErr = new Pack();

        Pack p = Get(autonum, db);

        if (p == null)
        {
            pErr.Err = PackErrList.NonExist;
            return pErr;
        }

        PackErr err = ValidateAndChangeStatus(db, p, actor);
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
        return Get(autonum, db, Pack.StatusX.All);
    }

    public static List<Pack> Get(List<int> autonumList)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return Get(autonumList, db, new Pack.StatusX[] { Pack.StatusX.All }, true);
    }

    public static Pack Get(int autonum, RedBloodDataContext db)
    {
        return Get(autonum, db, Pack.StatusX.All, false);
    }

    public static Pack Get(int autonum, RedBloodDataContext db, Pack.StatusX status)
    {
        return Get(autonum, db, status, true);
    }

    public static Pack Get(int autonum, RedBloodDataContext db, Pack.StatusX status, bool allowPackErr)
    {
        return Get(autonum, db, new Pack.StatusX[] { status }, allowPackErr);
    }

    public static Pack Get(int autonum, Pack.StatusX[] status)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(autonum, db, status, false);
    }

    public static Pack Get(int autonum, RedBloodDataContext db, Pack.StatusX[] status, bool allowPackErr)
    {
        List<int> a = new List<int>();
        a.Add(autonum);

        List<Pack> l = Get(a, db, status, allowPackErr);

        return l.FirstOrDefault();
    }

    public static List<Pack> Get(List<int> autonumList, RedBloodDataContext db, Pack.StatusX[] status, bool allowPackErr)
    {
        if (autonumList.Count == 0) return new List<Pack>();

        List<Pack> pList;

        if (status.Count() == 1 && status[0] == Pack.StatusX.All)
        {
            pList = db.Packs.Where(e => autonumList.Contains(e.Autonum)).ToList();
        }
        else
        {
            pList = db.Packs.Where(e => autonumList.Contains(e.Autonum) && status.Contains(e.Status)).ToList();
        }

        if (allowPackErr) return pList;
        else
        {
            PackErr err = Validate(pList);
            if (err == PackErrList.Non) return pList;
        }

        return new List<Pack>();
    }

    public static Pack Get4Extract(int autonum, string actor)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return Get4Extract(db, autonum, actor);
    }

    public static Pack Get4Extract(RedBloodDataContext db, int autonum, string actor)
    {
        Pack p = GetCarefully(db, autonum, actor);

        if (p == null) throw new Exception("Get4Extract() catch Exception");

        if (p.Err != PackErrList.Non)
        {
            return p;
        }



        if (p.ComponentID == (int)TestDef.Component.RBC
           || p.ComponentID == (int)TestDef.Component.FFPlasma
           )
        {
            p.Err = PackErrList.ExtractedProduction;
            return p;
        }

        if (p.ComponentID == (int)TestDef.Component.Full)
        {
            int count = p.PackExtractsBySource
                .Where(r =>
                    r.ExtractPack.ComponentID == (int)TestDef.Component.RBC
                    || r.ExtractPack.ComponentID == (int)TestDef.Component.FFPlasma
                    )
                .Count();

            if (count == 2)
            {
                p.Err = PackErrList.Extracted;
                return p;
            }
            else if (count == 0)
            {
                if (!StatusList4Production().Contains(p.Status))
                {
                    p.Err = new PackErr("Túi máu: " + p.Status);
                    return p;
                }

                p.Err = PackErrList.Valid4Extract;
                return p;
            }
            else
            {
                p.Err = PackErrList.DataErr;
                return p;
            }
        }

        p.Err = new PackErr(PackErrList.Invalid4Extract.Message + " " + p.Component.Name);
        return p;
    }

    public static Pack GetByCode(string code)
    {
        return Get(CodabarBLL.ParsePackAutoNum(code));
    }

    public static List<Pack> Get(RedBloodDataContext db, Pack.StatusX[] status)
    {
        var rs = from c in db.Packs
                 where status.Contains(c.Status)
                 select c;

        return rs.ToList();
    }

    public static List<Pack> GetByCampaign(int campaignID, Pack.StatusX[] status)
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
            p.PeopleID = peopleID;
            p.CollectedDate = DateTime.Now;
            p.CampaignID = campaignID;
            p.ComponentID = (int)TestDef.Component.Full;

            PackStatusHistory h = ChangeStatus(p, Pack.StatusX.Assign, actor, "Assign peopleID=" + peopleID.ToString() + "&CampaignID=" + campaignID.ToString());
            db.PackStatusHistories.InsertOnSubmit(h);

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
                where c.PeopleID == peopleID && (c.Status == Pack.StatusX.Assign || c.Status == Pack.StatusX.EnterTestResult)
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

        PackStatusHistory h = ChangeStatus(p, Pack.StatusX.Delete, actor, note);
        db.PackStatusHistories.InsertOnSubmit(h);

        db.SubmitChanges();

        return null;
    }

    //public static void Delete_EnterPackErr(string actor)
    //{
    //    Pack[] l = GetEnterPackErr();
    //    foreach (Pack e in l)
    //    {
    //        DeletePack(null, e.Autonum, "Hủy túi máu bị lỗi khi thu.", actor);
    //    }
    //}

    //Only pack has status 0 can be remove, to re-assign to another people.
    public Pack RemovePeople(int autonum, string actor)
    {
        if (autonum == 0) return null;

        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get(autonum, db, Pack.StatusX.Assign);

        if (p == null && p.PeopleID != null) return p;

        //remove people
        p.PeopleID = null;
        p.CollectedDate = null;
        p.CampaignID = null;

        PackStatusHistory h = ChangeStatus(p, Pack.StatusX.Init, actor, "Remove peopleID=" + p.PeopleID.ToString() + "&CampaignID=" + p.CampaignID.ToString());
        db.PackStatusHistories.InsertOnSubmit(h);

        db.SubmitChanges();

        return p;
    }

    public static List<TestDef> ValidateTestResult(Pack p)
    {
        if (p.ComponentID == (int)TestDef.Component.Full)
        {
            return ValidateTestResult(p.TestResult2);
        }

        if (p.ComponentID == (int)TestDef.Component.RBC
            || p.ComponentID == (int)TestDef.Component.FFPlasma)
        {
            if (p.PackExtractsByExtract.Count == 0)
                throw new Exception("Lỗi dữ liệu.");

            return ValidateTestResult(p.PackExtractsByExtract.FirstOrDefault().SourcePack.TestResult2);
        }

        if (p.ComponentID == (int)TestDef.Component.Platelet)
        {
            if (p.PackExtractsByExtract.Count == 0)
                throw new Exception("Lỗi dữ liệu.");

            List<TestDef> r = new List<TestDef>();

            foreach (PackExtract item in p.PackExtractsByExtract)
            {
                r = ValidateTestResult(item.SourcePack.TestResult2);
                if (r.Count() > 0) return r;
            }

            return r;
        }

        throw new Exception("Không kiểm tra được KQXN.");
    }

    public static List<TestDef> ValidateTestResult(TestResult e)
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

        return r;
    }

    public static PackErr ValidateAndChangeStatus(RedBloodDataContext db, Pack p, string actor)
    {
        PackErr err = Validate(p);

        if (err != PackErrList.Non)
        {
            PackStatusHistory h = PackBLL.ChangeStatus(p, err.ToStatusX, actor, err.Message);
            db.PackStatusHistories.InsertOnSubmit(h);
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
        else if (p.ComponentID == (int)TestDef.Component.Full)
        {
            if (p.PeopleID == null
                || p.CampaignID == null
                || p.CollectedDate == null
                || p.CollectedDate >= DateTime.Now)
                return PackErrList.DataErr;
        }

        //is expired
        if (p.CollectedDate != null
            && !(p.CollectedDate.Value.Date >= LowerLimDate())
            )
            if (p.PackExtractsBySource.Count > 0)
            {
                return PackErrList.ExpiredAndProduced;
            }
            else return PackErrList.Expired;

        if (StatusListEnteringTestResult().Contains(p.Status))
        {
            //Data error
            //if (p.BloodTypes.Count > 1)
            //    return PackErrList.DataErr;

            //Data error
            //if (p.BloodTypes.Count == 1)
            //{
            //    try
            //    {
            //        //Validate BloodType
            //        BloodType e = p.BloodTypes[0];
            //    }
            //    catch (Exception ex)
            //    {
            //        return new PackErr(ex.Message, Pack.StatusX.Delete);
            //    }
            //}
        }

        return PackErrList.Non;
    }

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

    public static void VerifyCommitTestResult(int autonum, string actor)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Pack p = PackBLL.Get(autonum, db);

        if (p != null
            && StatusListEnteringTestResult().Contains(p.Status))
        {
            if (
                (p.ComponentID != null
                && p.ComponentID.Value == (int)TestDef.Component.PlateletApheresis)

                ||

                (p.ComponentID != null && p.Volume != null
                && p.BloodType2 != null
                && p.BloodType2.aboID != null && p.BloodType2.rhID != null
                && p.TestResult2 != null
                && p.TestResult2.HIVID != null && p.TestResult2.HCVID != null && p.TestResult2.HBsAgID != null && p.TestResult2.SyphilisID != null && p.TestResult2.MalariaID != null)
                )
            {
                PackStatusHistory h = ChangeStatus(p, Pack.StatusX.CommitTestResult, actor, "Manually Enter");
                db.PackStatusHistories.InsertOnSubmit(h);
            }
            else
            {
                PackStatusHistory h = ChangeStatus(p, Pack.StatusX.EnterTestResult, actor, "Manually Enter");
                db.PackStatusHistories.InsertOnSubmit(h);
            }
        }

        db.SubmitChanges();
    }

    public static PackErr Extract(int autonum, string actor)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get4Extract(db, autonum, actor);

        if (p == null) return PackErrList.NonExist;

        if (p.Err != PackErrList.Valid4Extract)
            return p.Err;

        //Extract
        Pack packRBC = new Pack();
        packRBC.ComponentID = (int)TestDef.Component.RBC;
        packRBC.CollectedDate = DateTime.Now;
        packRBC.Status = Pack.StatusX.Production;
        packRBC.Actor = actor;
        db.Packs.InsertOnSubmit(packRBC);

        Pack packPlasma = new Pack();
        packPlasma.ComponentID = (int)TestDef.Component.FFPlasma;
        packPlasma.CollectedDate = DateTime.Now;
        packPlasma.Status = Pack.StatusX.Production;
        packPlasma.Actor = actor;
        db.Packs.InsertOnSubmit(packPlasma);

        db.SubmitChanges();

        PackExtract peRBC = new PackExtract();
        peRBC.SourcePackID = p.ID;
        peRBC.ExtractPackID = packRBC.ID;
        db.PackExtracts.InsertOnSubmit(peRBC);

        PackExtract pePlasma = new PackExtract();
        pePlasma.SourcePackID = p.ID;
        pePlasma.ExtractPackID = packPlasma.ID;
        db.PackExtracts.InsertOnSubmit(pePlasma);

        db.SubmitChanges();

        return PackErrList.Non;
    }

    public static PackErr Combine2Platelet(List<int> autonumListIn, int autonumOut, string actor, string note)
    {
        List<Pack> pInList = new List<Pack>();
        foreach (int item in autonumListIn)
        {
            Pack p = Get4Combined2Platelet(item, actor);
            if (p.Err == PackErrList.Valid4Platelet)
                pInList.Add(p);
            else
                return p.Err;
        }

        RedBloodDataContext db = new RedBloodDataContext();
        Pack pOut = Get4Combined2Platelet(db, autonumOut, actor);
        if (pOut.Err != PackErrList.Init4Platelet)
            return pOut.Err;

        if (autonumListIn.Count != pInList.Count
            || pOut == null)
        {
            return PackErrList.Invalid4Platelet;
        }

        pOut.ComponentID = (int)TestDef.Component.Platelet;
        pOut.CollectedDate = DateTime.Now;
        pOut.Note = note;

        PackStatusHistory h = ChangeStatus(pOut, Pack.StatusX.Production, actor, "Combine2Platelet");
        db.PackStatusHistories.InsertOnSubmit(h);

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

    public static Pack Get4Combined2Platelet(int autonum, string actor)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get4Combined2Platelet(db, autonum, actor);
    }

    public static Pack Get4Combined2Platelet(RedBloodDataContext db, int autonum, string actor)
    {
        Pack p = GetCarefully(db, autonum, actor);

        if (p == null) throw new Exception("GetCarefully() catch Exception");

        if (p.Err != PackErrList.Non)
        {
            return p;
        }

        if (p.Status == Pack.StatusX.Init)
        {
            p.Err = PackErrList.Init4Platelet;
            return p;
        }

        if (p.ComponentID == (int)TestDef.Component.Platelet)
        {
            p.Err = PackErrList.IsPlatelet;
            return p;
        }



        if (p.ComponentID == (int)TestDef.Component.Full)
        {
            int count = p.PackExtractsBySource
                .Where(r =>
                    r.ExtractPack.ComponentID == (int)TestDef.Component.Platelet)
                .Count();

            if (count == 1)
            {
                p.Err = PackErrList.Combined2Platelet;
                return p;
            }

            if (count == 0)
            {
                if (!StatusList4Production().Contains(p.Status))
                {
                    p.Err = new PackErr("Túi máu: " + p.Status);
                    return p;
                }

                p.Err = PackErrList.Valid4Platelet;
                return p;
            }

            if (count > 1)
            {
                p.Err = PackErrList.DataErr;
                return p;
            }
        }

        p.Err = new PackErr(PackErrList.Invalid4Platelet.Message + " " + p.Component.Name);
        return p;
    }

    public static Pack IsExtracted(int autonum)
    {
        Pack p = Get(autonum);

        if (p == null) return null;

        if (p.ComponentID == (int)TestDef.Component.Full)
        {
            if (p.PackExtractsBySource
                .Where(r =>
                    r.ExtractPack.ComponentID == (int)TestDef.Component.RBC
                    || r.ExtractPack.ComponentID == (int)TestDef.Component.FFPlasma
                    )
                .Count() > 0)
                return p;
        }

        if (p.ComponentID == (int)TestDef.Component.RBC
            || p.ComponentID == (int)TestDef.Component.FFPlasma
            )
        {
            return p;
        }

        return null;
    }
}

