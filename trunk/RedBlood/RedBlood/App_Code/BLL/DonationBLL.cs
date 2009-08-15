using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DonationBLL
/// </summary>
public class DonationBLL
{
    public DonationBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool IsTRLocked(Donation e)
    {
        return !(new Donation.TestResultStatusX[] { Donation.TestResultStatusX.Non, 
            Donation.TestResultStatusX.Negative, 
            Donation.TestResultStatusX.Positive}).Contains(e.TestResultStatus);
    }

    public static bool CanUpdateTestResult(Donation e)
    {
        return e.Pack != null
            //Need TR product: && ComponentID == TestDef.Component.Full
            && !IsTRLocked(e);
    }

    public static List<Donation> New(RedBloodDataContext db, int count)
    {
        Facility f = FacilityBLL.GetFirst(db);

        int autonum = f.CountingNumber.Value;

        Donation[] l = new Donation[count];

        for (int i = 0; i < l.Length; i++)
        {
            l[i] = new Donation();
            autonum++;
            l[i].DIN = f.FIN + f.CountingYY + autonum.ToString("D6");
            l[i].Status = Donation.StatusX.Init;
            l[i].InfectiousMarkers = 0.ToString("D" + BarcodeBLL.InfectiousMarkersLength.ToString());

        }

        f.CountingNumber = autonum;

        db.Donations.InsertAllOnSubmit(l);

        return l.ToList();
    }

    public static List<Donation> New(int count)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Donation> l = New(db, count);

        db.SubmitChanges();
        return l;
    }

    public static Donation Get(RedBloodDataContext db, string DIN)
    {
        return db.Donations.Where(r => r.DIN == DIN).FirstOrDefault();
    }

    public static Donation Get(string DIN)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(db, DIN);
    }

    public static DonationErr Assign(string DIN, Guid peopleID, int campaignID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Donation d = (from c in db.Donations
                      where c.DIN == DIN && c.PeopleID == null && c.CampaignID == null
                      select c).FirstOrDefault();

        if (d == null)
        {
            return DonationErrEnum.NonExist;
        }

        try
        {
            d.PeopleID = peopleID;
            d.CollectedDate = DateTime.Now;
            d.CampaignID = campaignID;
            d.Actor = RedBloodSystem.CurrentActor;

            UpdateStatus(db, d, Donation.StatusX.Assigned, "Assign peopleID=" + peopleID.ToString() + "&CampaignID=" + campaignID.ToString());

            db.SubmitChanges();

            CampaignBLL.SetStatus(campaignID);
        }
        catch (Exception ex)
        {
            return new DonationErr(ex.Message);
        }

        return DonationErrEnum.Non;
    }

    public static DonationStatusLog UpdateStatus(RedBloodDataContext db, Donation e, Donation.StatusX to, string note)
    {
        return UpdateStatus(db, e, to, RedBloodSystem.CurrentActor, note);
    }

    public static DonationStatusLog UpdateStatus(RedBloodDataContext db, Donation e, Donation.StatusX to, string actor, string note)
    {
        if (e.Status == to) return null;

        Donation.StatusX from = e.Status;

        e.Status = to;

        DonationStatusLog l = new DonationStatusLog(e, from, to, actor, note);

        db.DonationStatusLogs.InsertOnSubmit(l);

        return l;
    }

    public static Donation UpdateDefault(string DIN, string collector)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Donation e = Get(db, DIN);

        if (e == null) return null;

        if (e.Collector == null || string.IsNullOrEmpty(e.Collector.Trim()))
        {
            if (!string.IsNullOrEmpty(collector.Trim()))
            {
                e.Collector = collector;
                db.SubmitChanges();
                return e;
            }
        }

        return null;
    }

    public static DonationErr Update(RedBloodDataContext db, Donation e,
       string HIV, string HCV_Ab, string HBs_Ag, string Syphilis, string Malaria,
        string note)
    {
        if (e == null || !CanUpdateTestResult(e)) return DonationErrEnum.TRLocked;

        string old = e.InfectiousMarkers;

        e.Markers.HIV = TR.GetDefault(HIV).Name;
        e.Markers.HCV_Ab = TR.GetDefault(HCV_Ab).Name;
        e.Markers.HBs_Ag = TR.GetDefault(HBs_Ag).Name;
        e.Markers.Syphilis = TR.GetDefault(Syphilis).Name;
        e.Markers.Malaria = TR.GetDefault(Malaria).Name;

        if (old != e.InfectiousMarkers)
        {
            //Keep track
            //PackResultHistoryBLL.Insert(db, p, MalariaID, times, RedBloodSystem.CurrentActor, note);
            DonationTestLogBLL.Insert(db, e, typeof(InfectiousMarker), note);
        }

        return DonationErrEnum.Non;
    }

    public static List<Donation> Get(int campaignID)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        //return db.Packs.Where(r => r.CampaignID == campaignID
        //    && StatusListAllowEnterTestResult().Contains(r.Status)
        //    && AllowEnterTestResult().Contains(r.TestResultStatus)
        //    && r.ComponentID == TestDef.Component.Full
        //    ).ToList();
        return db.Donations.Where(r => r.CampaignID == campaignID).ToList();

    }

    //public static List<Pack> GetByCampaign(int campaignID, List<Pack.StatusX> status)
    //{
    //    //RedBloodDataContext db = new RedBloodDataContext();
    //    //return db.Packs.Where(r => r.CampaignID == campaignID && status.Contains(r.Status)).ToList();
    //    return new List<Pack>();

    //}
}
