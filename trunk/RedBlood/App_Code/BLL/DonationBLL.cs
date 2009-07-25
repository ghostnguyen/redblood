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

    public static Donation[] New(RedBloodDataContext db, int count)
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
        }

        f.CountingNumber = autonum;

        db.Donations.InsertAllOnSubmit(l);

        return l;
    }

    public static Donation[] New(int count)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Donation[] l = New(db, count);

        db.SubmitChanges();
        return l;
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

    public static 
}
