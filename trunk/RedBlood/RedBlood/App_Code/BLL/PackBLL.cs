using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PackBLL
{
    public PackBLL()
    {
    }

    public static Pack Get(RedBloodDataContext db, Guid ID)
    {
        if (db == null)
            throw new Exception("RedBloodDataContext null");

        Pack p = db.Packs.Where(r => r.ID == ID).FirstOrDefault();

        if (p == null)
            throw new Exception("Không tìm thấy túi máu.");

        return p;
    }

    public static Pack Get(string DIN, string productCode)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack> l = db.Packs.Where(r => r.DIN == DIN && r.ProductCode == productCode).ToList();

        if (l.Count > 1)
        {
            throw new Exception("Dữ liệu túi máu bị trùng.");
        }

        if (l.Count == 0)
        {
            throw new Exception("Không có túi máu.");
        }

        return l.FirstOrDefault();
    }

    public static Pack Get4ReportSideEffects(string DIN, string productCode)
    {
        Pack p = Get(DIN,productCode);

        if (p.Status != Pack.StatusX.Delivered)
        {
            throw new Exception("Túi máu chưa cấp phát.");
        }

        return p;
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

        db.SubmitChanges();

        return e;
    }

    public static PackStatusHistory Update(RedBloodDataContext db, Pack p, Pack.StatusX to, string note)
    {
        return Update(db, p, to, RedBloodSystem.CurrentActor, note);
    }



    //public static void LockEnterTestResult()
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    IQueryable<Donation> l = db.Donations.Where(r =>
    //        (r.TestResultStatus == Donation.TestResultStatusX.Negative
    //        || r.TestResultStatus == Donation.TestResultStatusX.Positive));

    //    foreach (Donation item in l)
    //    {
    //        if (item.TestResultStatus == Donation.TestResultStatusX.Negative)
    //            item.TestResultStatus = Donation.TestResultStatusX.NegativeLocked;

    //        if (item.TestResultStatus == Donation.TestResultStatusX.Positive)
    //            item.TestResultStatus = Donation.TestResultStatusX.PositiveLocked;
    //    }

    //    db.SubmitChanges();
    //}

    public static void CreateOriginal(string DIN, string productCode, int defaultVolume)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Donation d = db.Donations.Where(r => r.DIN == DIN && r.PeopleID != null).FirstOrDefault();
        Product product = db.Products.Where(r => r.Code == productCode).FirstOrDefault();

        if (d == null || product == null)
            throw new Exception(PackErrEnum.DataErr.Message);

        int countPack = db.Packs.Where(r => r.DIN == DIN && r.ProductCode == productCode).Count();

        if (countPack > 0)
            throw new Exception(PackErrEnum.Existed.Message);

        //TODO: Check to see valid product code in collection
        //Code will be here

        if (d.OrgPackID != null)
            throw new Exception(PackErrEnum.DonationGotPack.Message);

        Pack pack = new Pack();

        pack.DIN = DIN;
        pack.ProductCode = productCode;
        pack.Status = Pack.StatusX.Product;
        pack.Date = DateTime.Now;
        pack.Actor = RedBloodSystem.CurrentActor;

        if (d.Volume != null && d.Volume.Value > 0) throw new Exception(PackErrEnum.DataErr.Message);
        else
        {
            if (product.OriginalVolume != null && product.OriginalVolume.Value > 0)
            {
                d.Volume = product.OriginalVolume;
                pack.Volume = product.OriginalVolume;
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

        //TODO: Check to see if the pack is collector too late
        //Code check will be here.

        pack.ExpirationDate = DateTime.Now.Add(product.Duration.Value - RedBloodSystem.RootTime);

        db.Packs.InsertOnSubmit(pack);

        db.SubmitChanges();

        d.OrgPackID = pack.ID;
        db.SubmitChanges();

        PackTransactionBLL.Add(pack.ID, PackTransaction.TypeX.In_Collect);
    }
}

