using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Linq.Expressions;
namespace RedBlood.BLL
{
    /// <summary>
    /// Summary description for DonationBLL
    /// </summary>
    public class DonationBLL
    {
        RedBloodDataContext db = new RedBloodDataContext();

        public DonationBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool CanUpdateTestResult(Donation e)
        {
            if (e == null) throw new Exception(DonationErrEnum.NonExist.Message);

            if (e.Packs.Count == 0)
                return false;

            if (e.Packs.Count(r => r.Status == Pack.StatusX.Delivered) > 0)
                return false;

            return true;
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
            Donation d = db.Donations.Where(r => r.DIN == DIN).FirstOrDefault();
            if (d == null)
                throw new Exception("Chưa tạo mã túi máu.");

            return d;
        }

        public static Donation Get(string DIN)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            return Get(db, DIN);
        }

        public List<Donation> Get(string[] DINList)
        {
            return db.Donations.Where(r => r.People != null && DINList.Contains(r.DIN)).ToList();
        }

        public static Donation GetAssigned(RedBloodDataContext db, string DIN)
        {
            Donation d = Get(db, DIN);

            if (d.PeopleID == null)
            {
                throw new Exception("Mã túi máu chưa cấp phát.");
            }

            return d;
        }

        public static Donation Get4CreateOriginal(RedBloodDataContext db, string DIN)
        {
            Donation d = GetAssigned(db, DIN);

            if (d.OrgPackID != null)
                throw new Exception(PackErrEnum.DonationGotPack.Message);

            return d;
        }

        public static Donation SetOriginalPack(string DIN, Guid packID)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            Donation d = Get4CreateOriginal(db, DIN);

            d.OrgPackID = packID;

            db.SubmitChanges();

            return d;
        }

        public static Donation Get4Order(string DIN)
        {
            Donation d = Get(DIN);
            if (d.TestResultStatus != Donation.TestResultStatusX.Negative)
            {
                throw new Exception("Không thể cấp phát túi máu này. KQ xét nghiệm sàng lọc: " + d.TestResultStatus);
            }
            return d;
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

        public static void RemoveOriginalPack(string DIN)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            Donation d = Get(db, DIN);

            if (d.CanRemoveOriginalPack)
            {
                Pack p = d.Pack;
                d.Pack = null;
                
                db.PackTransactions.DeleteAllOnSubmit(p.PackTransactions);
                db.PackRemainDailies.DeleteAllOnSubmit(p.PackRemainDailies);
                db.Packs.DeleteOnSubmit(p);

                db.SubmitChanges();
            }
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

            // Warning: As CR user requirement, value for both test result are always the same.
            e.InfectiousMarkers = Infection.HIV_Ab.Encode(e.InfectiousMarkers, HIV);
            e.InfectiousMarkers = Infection.HIV_Ag.Encode(e.InfectiousMarkers, HIV);

            e.InfectiousMarkers = Infection.HCV_Ab.Encode(e.InfectiousMarkers, HCV_Ab);
            e.InfectiousMarkers = Infection.HBs_Ag.Encode(e.InfectiousMarkers, HBs_Ag);
            e.InfectiousMarkers = Infection.Syphilis.Encode(e.InfectiousMarkers, Syphilis);
            e.InfectiousMarkers = Infection.Malaria.Encode(e.InfectiousMarkers, Malaria);

            if (old != e.InfectiousMarkers)
            {
                DonationTestLogBLL.Insert(db, e, PropertyName.For<Donation>(r => r.Markers), note);
            }

            //Have to save before update TestResult Status
            db.SubmitChanges();

            UpdateTestResultStatus(e);
            db.SubmitChanges();

            return DonationErrEnum.Non;
        }

        public static void Update(string DIN, string bloodGroup, string note)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            Donation e = DonationBLL.Get(db, DIN);

            if (e == null)
                throw new Exception(DonationErrEnum.NonExist.Message);

            if (!CanUpdateTestResult(e))
                throw new Exception(DonationErrEnum.TRLocked.Message);

            if (bloodGroup.Trim() != e.BloodGroup)
            {
                e.BloodGroup = bloodGroup;
                DonationTestLogBLL.Insert(db, e, PropertyName.For<Donation>(r => r.BloodGroup), note);
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

        public static List<Donation> GetUnLock(int campaignID)
        {
            return CampaignBLL.Get(campaignID).CollectedDonations
                .ToList()
                .Where(r => !r.IsTRLocked).ToList();
        }

        public static IEnumerable<Donation> Get(int campaignID)
        {
            return CampaignBLL.Get(campaignID).Donations;
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
                return l.Where(r => r.TestResultStatus == Donation.TestResultStatusX.Negative).ToList();
            }

            if (rptType == ReportType.FourPosInCam)
            {
                return l.Where(r =>
                    r.TestResultStatus == Donation.TestResultStatusX.Positive
                    && r.Markers.HIV == TR.neg.Name).ToList();
            }

            if (rptType == ReportType.HIVInCam)
            {
                return l.Where(r => r.Markers.HIV == TR.pos.Name).ToList();
            }

            return new List<Donation>();
        }
    }
}