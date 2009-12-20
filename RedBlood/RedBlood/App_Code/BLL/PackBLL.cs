using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PackBLL
{
    public PackBLL()
    {
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

    public static PackStatusHistory Update(RedBloodDataContext db, Pack p, Pack.StatusX to, string actor, string note)
    {
        if (p.Status == to) return null;

        PackStatusHistory e = new PackStatusHistory();

        e.PackID = p.ID;
        e.FromStatus = p.Status;
        e.ToStatus = to;
        e.Actor = actor;
        e.Note = note;
        e.Date = DateTime.Now;

        p.Status = to;

        db.PackStatusHistories.InsertOnSubmit(e);
        return e;
    }

    public static PackStatusHistory Update(RedBloodDataContext db, Pack p, Pack.StatusX to, string note)
    {
        return Update(db, p, to, RedBloodSystem.CurrentActor, note);
    }

    public static PackErr Extract(List<Guid> packIDList, List<string> productCodeList)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack> packList = db.Packs.Where(r => packIDList.Contains(r.ID)).ToList();

        foreach (Pack item in packList)
        {
            foreach (string code in productCodeList)
            {
                PackBLL.Extract(item.ID, code);
            }
        }

        return PackErrEnum.Non;
    }

    public static void LockEnterTestResult()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        IQueryable<Donation> l = db.Donations.Where(r =>
            (r.TestResultStatus == Donation.TestResultStatusX.Negative
            || r.TestResultStatus == Donation.TestResultStatusX.Positive));

        foreach (Donation item in l)
        {
            if (item.TestResultStatus == Donation.TestResultStatusX.Negative)
                item.TestResultStatus = Donation.TestResultStatusX.NegativeLocked;

            if (item.TestResultStatus == Donation.TestResultStatusX.Positive)
                item.TestResultStatus = Donation.TestResultStatusX.PositiveLocked;
        }

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

        PackTransactionBLL.Add(pack.ID, PackTransaction.TypeX.In_Collect, System.Reflection.MethodBase.GetCurrentMethod().Name);

        return PackErrEnum.Non;

    }

    public static PackErr Extract(Guid srcPackID, string productCode)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack pack = db.Packs.Where(r => r.ID == srcPackID).FirstOrDefault();
        Product p = db.Products.Where(r => r.Code == productCode).FirstOrDefault();

        //Validate
        if (pack == null || p == null) return PackErrEnum.DataErr;

        if (pack.Donation.TestResultStatus == Donation.TestResultStatusX.Positive
            || pack.Donation.TestResultStatus == Donation.TestResultStatusX.PositiveLocked)
        {
            return PackErrEnum.Positive;
        }

        if (db.Packs.Where(r => r.DIN == pack.DIN && r.ProductCode == productCode).FirstOrDefault() != null)
            return PackErrEnum.Existed;

        //TODO: Check to see if the pack is collector too late
        //Code check will be here.

        //Create new
        Pack toPack = new Pack();

        toPack.DIN = pack.DIN;
        toPack.ProductCode = productCode;
        toPack.Status = Pack.StatusX.Product;
        toPack.Date = DateTime.Now;
        toPack.Actor = RedBloodSystem.CurrentActor;
        //toPack.Volume = p.OriginalVolume;
        toPack.ExpirationDate = DateTime.Now.Add(p.Duration.Value - RedBloodSystem.RootTime);

        db.Packs.InsertOnSubmit(toPack);
        db.SubmitChanges();

        PackTransactionBLL.Add(toPack.ID, PackTransaction.TypeX.In_Product, System.Reflection.MethodBase.GetCurrentMethod().Name);

        //Update fromPack
        PackStatusHistory h = Update(db, pack, Pack.StatusX.Produced, "");
        if (h != null)
        {
            db.SubmitChanges();
            PackTransactionBLL.Add(pack.ID, PackTransaction.TypeX.Out_Product, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        return PackErrEnum.Non;
    }
}

