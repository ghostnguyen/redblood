using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for ExcelBLL
/// </summary>
public class ExcelBLL
{
    public ExcelBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    static int Imported = 1;
    static string note = "Excel Import";
    public static void Import(string actor)
    {
        RedBloodDataContext db1 = new RedBloodDataContext();

        //List<Excel> l = db1.Excels.Where(r => r.Imported == null || r.Imported != Imported).OrderBy(r => r.ID).ToList();

        List<Excel> l = (from r in db1.Excels
                         where (r.Imported == null || r.Imported != Imported)
                         orderby r.ID
                         select r).ToList();

        foreach (Excel item in l)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            //Pack p = db.Packs.Where(r => r.Status == Pack.StatusX.Init).FirstOrDefault();
            
            Pack p = db.Packs.Where(r => r.Autonum == item.ID).FirstOrDefault();

            if (p == null || p.Status != Pack.StatusX.Init)
            {
                return;
            }

            //db.SubmitChanges();

            if (string.IsNullOrEmpty(item.HoVaTen.Trim())
                || string.IsNullOrEmpty(item.MSTM.Trim())
                || string.IsNullOrEmpty(item.MSNH.Trim()))
            {
                continue;
            }

            string ABO = item.ABO.Trim();
            string RH = item.RH.Trim();

            int ABOID = 0;
            int RHID = 0;

            if (ABO.ToLower() == "AB".ToLower()) ABOID = TestDef.ABO.AB;
            if (ABO.ToLower() == "A".ToLower()) ABOID = TestDef.ABO.A;
            if (ABO.ToLower() == "B".ToLower()) ABOID = TestDef.ABO.B;
            if (ABO.ToLower() == "O".ToLower()) ABOID = TestDef.ABO.O;

            if (RH.ToLower() == "Rh(+)".ToLower()) RHID = TestDef.RH.Pos;
            if (RH.ToLower() == "Rh(-)".ToLower()) RHID = TestDef.RH.Neg;


            string HIV = item.HIV.Trim();
            string HBsAg = item.HBsAg.Trim();
            string HCV = item.HCV.Trim();
            string Syphilis = item.Syphilis.Trim();
            string Malaria = item.Malaria.Trim();

            int HIVID = 0;
            int HBsAgID = 0;
            int HCVID = 0;
            int SyphilisID = 0;
            int MalariaID = 0;

            if (HIV.ToLower() == ("Âm tính").ToLower()) HIVID = TestDef.HIV.Neg;
            if (HIV.ToLower() == "Kiểm tra lần 2".ToLower()) HIVID = TestDef.HIV.Pos;
            if (HIV.ToLower() == "Chưa xác định".ToLower()) HIVID = TestDef.HIV.NA;

            if (HCV.ToLower() == "Âm tính".ToLower()) HCVID = TestDef.HCV.Neg;
            if (HCV.ToLower() == "Dương tính".ToLower()) HCVID = TestDef.HCV.Pos;
            if (HCV.ToLower() == "Chưa xác định".ToLower()) HCVID = TestDef.HCV.NA;

            if (HBsAg.ToLower() == "Âm tính".ToLower()) HBsAgID = TestDef.HBsAg.Neg;
            if (HBsAg.ToLower() == "Dương tính".ToLower()) HBsAgID = TestDef.HBsAg.Pos;
            if (HBsAg.ToLower() == "Chưa xác định".ToLower()) HBsAgID = TestDef.HBsAg.NA;

            if (Syphilis.ToLower() == "Âm tính".ToLower()) SyphilisID = TestDef.Syphilis.Neg;
            if (Syphilis.ToLower() == "Dương tính".ToLower()) SyphilisID = TestDef.Syphilis.Pos;
            if (Syphilis.ToLower() == "Chưa xác định".ToLower()) SyphilisID = TestDef.Syphilis.NA;

            if (Malaria.ToLower() == "Âm tính".ToLower()) MalariaID = TestDef.Malaria.Neg;
            if (Malaria.ToLower() == "Dương tính".ToLower()) MalariaID = TestDef.Malaria.Pos;
            if (Malaria.ToLower() == "Chưa xác định".ToLower()) MalariaID = TestDef.Malaria.NA;

            if (HIVID == 0 || HCVID == 0 || HBsAgID == 0 || SyphilisID == 0 || MalariaID == 0
                || RHID == 0 || RHID == 0)
            {
                continue;
            }
            Geo geo1 = null;
            Geo geo2 = null;
            Geo geo3 = null;

            geo1 = GeoBLL.GetByName(item.Province, 1);

            if (geo1 == null)
            {
                continue;
            }

            geo2 = geo1.Geos.Where(r => r.Name.ToLower() == item.District.ToLower()).FirstOrDefault();
            if (geo2 != null)
                geo3 = geo2.Geos.Where(r => r.Name.ToLower() == item.Ward.ToLower()).FirstOrDefault();

            //Geo geo2 = GeoBLL.GetByName(item.District, 2);
            //Geo geo3 = GeoBLL.GetByName(item.Ward, 3);

           

            Campaign cam = CampaignBLL.GetByID(item.CampaignID.ToInt());
            if (cam == null || cam.Date == null)
            {
                continue;
            }

            People people = new People();
            
            try
            {
                people.DOB = new DateTime(item.DOB.ToInt(), 1, 1);
            }
            catch (Exception)
            {
                people.DOB = new DateTime(1888, 1, 1);
            }
            

            people.Name = item.HoVaTen;
            people.ResidentGeoID1 = geo1.ID;
            if (geo2 != null) people.ResidentGeoID2 = geo2.ID;
            if (geo3 != null) people.ResidentGeoID3 = geo3.ID;
            people.ResidentAddress = item.Address;

            db.Peoples.InsertOnSubmit(people);
            db.SubmitChanges();

            

            p.MSTM = item.MSTM;
            p.MSNH = item.MSNH;

            p.PeopleID = people.ID;
            p.CollectedDate = cam.Date;
            p.CampaignID = cam.ID;

            PackStatusHistory his = PackBLL.ChangeStatus(p, Pack.StatusX.Collected, actor, note);
            db.PackStatusHistories.InsertOnSubmit(his);

            p.DeliverStatus = Pack.DeliverStatusX.Yes;

            PackBLL.Update(db, p
                , TestDefBLL.Get(db, TestDef.Component.Full)
                , 250
                , TestDefBLL.Get(db, TestDef.Substance.Non));
            BloodTypeBLL.Update(db, p, 2, TestDefBLL.Get(db, ABOID), TestDefBLL.Get(db, RHID), actor, note);
            TestResultBLL.Update(db, p, 2
                , TestDefBLL.Get(db, HIVID)
                , TestDefBLL.Get(db, HCVID)
                , TestDefBLL.Get(db, HBsAgID)
                , TestDefBLL.Get(db, SyphilisID)
                , TestDefBLL.Get(db, MalariaID)
                , actor, note);

            p.HospitalID = new Guid("0D39EC10-B425-41ED-9210-28FF740AD80D");
            p.Actor = actor;
            p.Note = note;

            try
            {
                db.SubmitChanges(ConflictMode.FailOnFirstConflict);

                PackBLL.UpdateTestResultStatus4Full(p.Autonum);

                item.Imported = 1;
            }
            catch (Exception ex)
            {
                throw;
            }

            db1.SubmitChanges();
        }

        
    }

}
