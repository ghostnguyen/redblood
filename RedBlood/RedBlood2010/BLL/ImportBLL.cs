using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace RedBlood.BLL
{
    /// <summary>
    /// Summary description for ImportBLL
    /// </summary>
    public class ImportBLL
    {
        public ImportBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void Importing()
        {
            //Validate database before insert

            List<string> importedGeo = new List<string>();

            RedBloodDataContext importDB;
            try
            {
                importDB = new RedBloodDataContext(ConfigurationManager.ConnectionStrings["ImportingRedBlood_DBConnectionString"].ConnectionString);

                //try to load whatever data to test connection
                importDB.Sexes.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string strImportWithInDays = ConfigurationManager.AppSettings["ImportWithInDays"];
            int importWithInDays = 1;

            if (!string.IsNullOrEmpty(strImportWithInDays))
            {
                importWithInDays = strImportWithInDays.ToInt();
            }

            if (importWithInDays < 1) importWithInDays = 1;

            List<Donation> importDINList = importDB.Donations
                .Where(r => r.CollectedDate.HasValue
                    && (DateTime.Now.Date - r.CollectedDate.Value.Date).Days <= importWithInDays - 1
                    && r.PeopleID.HasValue
                    && r.CampaignID.HasValue
                    )
                .ToList();

            RedBloodDataContext db = new RedBloodDataContext();

            foreach (Donation item in importDINList)
            {
                //Validate DIN
                Donation innerDIN = DonationBLL.Get(item.DIN);

                if (innerDIN == null
                    || innerDIN.CollectedDate.HasValue
                    || innerDIN.PeopleID.HasValue
                    )
                    continue;

                //Campaign
                Campaign innerCam = CampaignBLL.Get(item.CampaignID.Value);
                if (innerCam == null)
                    continue;

                //People
                if (item.People == null)
                    continue;

                Guid? peopleID = ImportPeople(db, item.People);

                if (!peopleID.HasValue || peopleID.Value == Guid.Empty)
                    continue;

                //Import DIN
                DonationBLL.Assign(innerDIN.DIN, peopleID.Value, innerCam.ID, item.CollectedDate, item.Actor);

                //PackBLL.CreateOriginal(innerDIN.DIN, item.Pack.ProductCode, item.Volume.Value);

                DonationBLL.Update(innerDIN.DIN, item.BloodGroup, "ImportingFromMDF");
                DonationBLL.UpdateCollector(innerDIN.DIN, item.Collector);
            }
        }

        private static Guid? ImportPeople(RedBloodDataContext db, People outerP)
        {
            //Importing people
            if (outerP == null || db == null) return null;

            People innerP = PeopleBLL.GetByID(outerP.ID);

            if (innerP == null)
            {
                People newP = new People();

                newP.Name = outerP.Name;
                newP.DOB = outerP.DOB;
                newP.DOBYear = outerP.DOBYear;
                newP.CMND = outerP.CMND;
                newP.SexID = outerP.SexID;

                GeoBLL.Insert(
                    outerP.ResidentGeo1 != null ? outerP.ResidentGeo1.Name : ""
                    , outerP.ResidentGeo2 != null ? outerP.ResidentGeo2.Name : ""
                    , outerP.ResidentGeo3 != null ? outerP.ResidentGeo3.Name : "");

                newP.ResidentAddress = outerP.ResidentAddress;
                newP.SetResidentGeo3(outerP.FullResidentalGeo);

                if (outerP.EnableMailingAddress.HasValue
                    && outerP.EnableMailingAddress.Value)
                {
                    GeoBLL.Insert(
                    outerP.MailingGeo1 != null ? outerP.MailingGeo1.Name : ""
                    , outerP.MailingGeo2 != null ? outerP.MailingGeo2.Name : ""
                    , outerP.MailingGeo3 != null ? outerP.MailingGeo3.Name : "");

                    newP.MailingAddress = outerP.MailingAddress;
                    newP.SetMailingGeo3(outerP.FullMaillingGeo);

                    newP.EnableMailingAddress = outerP.EnableMailingAddress;
                }

                newP.Job = outerP.Job;
                newP.Email = outerP.Email;
                newP.Phone = outerP.Phone;
                newP.Note = outerP.Note;

                db.Peoples.InsertOnSubmit(newP);
                db.SubmitChanges();

                return newP.ID;
            }
            else
            {
                return innerP.ID;
            }
        }
    }

}