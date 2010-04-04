using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Linq.Expressions;

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

    public static bool CanUpdateTestResult(Donation e)
    {
        if (e == null) throw new Exception(DonationErrEnum.NonExist.Message);

        return (new Donation.TestResultStatusX[] { Donation.TestResultStatusX.Non, 
            Donation.TestResultStatusX.Negative, 
            Donation.TestResultStatusX.Positive}).Contains(e.TestResultStatus);
    }

    public static List<Donation> New(int count)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Donation> l = New(db, count);

        db.SubmitChanges();
        return l;
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

    public static Donation Get(RedBloodDataContext db, string DIN)
    {
        return db.Donations.Where(r => r.DIN == DIN).FirstOrDefault();
    }

    public static Donation Get(string DIN)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(db, DIN);
    }

    public static DonationErr Assign(string DIN, Guid peopleID, int campaignID, DateTime? collectedDate, string actor)
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
            d.CollectedDate = collectedDate;
            d.CampaignID = campaignID;
            d.Actor = actor;

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

    public static DonationErr Assign(string DIN, Guid peopleID, int campaignID)
    {
        return Assign(DIN, peopleID, campaignID, DateTime.Now, RedBloodSystem.CurrentActor);
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

    public static void UpdateCollector(string DIN, string collector)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Donation e = Get(db, DIN);

        if (e != null 
            && string.IsNullOrEmpty(e.Collector)
            && !string.IsNullOrEmpty(collector)
            && !string.IsNullOrEmpty(collector.Trim()))
        {
            e.Collector = collector.Trim();
            db.SubmitChanges();
        }
    }

    public static DonationErr Update(string DIN,
       string HIV, string HCV_Ab, string HBs_Ag, string Syphilis, string Malaria,
        string note)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Donation e = DonationBLL.Get(db, DIN);

        if (e == null || !CanUpdateTestResult(e)) return DonationErrEnum.TRLocked;

        string old = e.InfectiousMarkers;

        e.Markers.HIV = TR.GetDefault(HIV).Name;
        e.Markers.HCV_Ab = TR.GetDefault(HCV_Ab).Name;
        e.Markers.HBs_Ag = TR.GetDefault(HBs_Ag).Name;
        e.Markers.Syphilis = TR.GetDefault(Syphilis).Name;
        e.Markers.Malaria = TR.GetDefault(Malaria).Name;

        if (old != e.InfectiousMarkers)
        {
            DonationTestLogBLL.Insert(db, e, Nameof<Donation>.Property(r => r.Markers), note);
        }

        //Have to save before updaye TestResult Status
        db.SubmitChanges();

        UpdateTestResultStatus(e);
        db.SubmitChanges();

        return DonationErrEnum.Non;
    }

    public static void Update(string DIN, string bloodGroup, string note)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Donation e = DonationBLL.Get(db, DIN);

        if (e == null )
            throw new Exception(DonationErrEnum.NonExist.Message);

        if (!CanUpdateTestResult(e))
            throw new Exception(DonationErrEnum.TRLocked.Message);

        if (bloodGroup.Trim() != e.BloodGroup)
        {
            e.BloodGroup = bloodGroup;
            DonationTestLogBLL.Insert(db, e, Nameof<Donation>.Property(r => r.BloodGroup), note);
        }

        //Have to save before updaye TestResult Status
        db.SubmitChanges();

        UpdateTestResultStatus(e);
        db.SubmitChanges();
    }

    public static DonationErr UpdateTestResultStatus(Donation e)
    {
        //RedBloodDataContext db = new RedBloodDataContext();

        //Donation e = Get(db, DIN);

        if (e == null || !CanUpdateTestResult(e)) return DonationErrEnum.TRLocked;

        if (string.IsNullOrEmpty(e.BloodGroup))
            e.TestResultStatus = Donation.TestResultStatusX.Non;
        else
            e.TestResultStatus = e.Markers.Status;

        return DonationErrEnum.Non;
    }

    public static void UpdateNegative(string DIN)
    {
        Update(DIN, TR.neg.Name, TR.neg.Name, TR.neg.Name, TR.neg.Name, TR.neg.Name, "UpdateNegative");
    }

    public static IQueryable<Donation> Get(int campaignID)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return db.Donations.Where(r => r.CampaignID == campaignID);
    }

    public static List<Donation> Get(int campaignID, ReportType rptType)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        IQueryable<Donation> v = from r in db.Donations
                                 where r.CampaignID == campaignID
                                     && r.Pack != null
                                 select r;

        if (rptType == ReportType.All)
        {
            return v.ToList();
        }

        List<Donation> l = v.Where(r => r.TestResultStatus != Donation.TestResultStatusX.Non).ToList();

        if (rptType == ReportType.NegInCam)
        {
            return l.Where(r => r.TestResultStatus == Donation.TestResultStatusX.Negative
                || r.TestResultStatus == Donation.TestResultStatusX.NegativeLocked).ToList();
        }

        if (rptType == ReportType.FourPosInCam)
        {
            return l.Where(r =>
                (r.TestResultStatus == Donation.TestResultStatusX.Positive
                || r.TestResultStatus == Donation.TestResultStatusX.PositiveLocked)
                &&
                r.Markers.HIV == TR.neg.Name).ToList();
        }

        if (rptType == ReportType.HIVInCam)
        {
            return l.Where(r => r.Markers.HIV == TR.pos.Name).ToList();
        }

        return new List<Donation>();
    }
}
