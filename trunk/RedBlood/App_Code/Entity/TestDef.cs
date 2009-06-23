using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
/// Summary description for TestDef
/// </summary>
public partial class TestDef
{
    public static class HIV
    {
        public static TestDef Neg;
        public static TestDef Pos;
        public static TestDef NA;
    }

    public static class HBsAg
    {
        public static TestDef Neg;
        public static TestDef Pos;
        public static TestDef NA;
    }

    public static class HCV
    {
        public static TestDef Neg;
        public static TestDef Pos;
        public static TestDef NA;
    }

    public static class Syphilis
    {
        public static TestDef Neg;
        public static TestDef Pos;
        public static TestDef NA;
    }

    public static class Malaria
    {
        public static TestDef Neg;
        public static TestDef Pos;
        public static TestDef NA;
    }

    public static class Component
    {
        public static TestDef Full;
        public static TestDef RBC; //red blood cell
        public static TestDef FFPlasma; // Huyết tương
        public static TestDef FactorVIII;
        public static TestDef PlateletApheresis;
        public static TestDef Platelet; // Tiểu cầu
        public static TestDef FFPlasma_Poor;
        public static TestDef WBC;// white blood cell
    }

    public static class Source
    {
        public static TestDef Donation;
        public static TestDef RedCross;
        public static TestDef Other;
    }


    public static class Substance
    {
        public static TestDef Non;
        public static TestDef Yes;
    }

    static TestDef()
    {
        RedBloodDataContext db = new RedBloodDataContext();
        List<TestDef> l = db.TestDefs.ToList();

        HIV.Neg = l.Where(r => r.ID == 19).FirstOrDefault();
        HIV.Pos = l.Where(r => r.ID == 20).FirstOrDefault();
        HIV.NA = l.Where(r => r.ID == 41).FirstOrDefault();

        HBsAg.Neg = l.Where(r => r.ID == 13).FirstOrDefault();
        HBsAg.Pos = l.Where(r => r.ID == 14).FirstOrDefault();
        HBsAg.NA = l.Where(r => r.ID == 42).FirstOrDefault();

        HCV.Neg = l.Where(r => r.ID == 16).FirstOrDefault();
        HCV.Pos = l.Where(r => r.ID == 17).FirstOrDefault();
        HCV.NA = l.Where(r => r.ID == 44).FirstOrDefault();

        Syphilis.Neg = l.Where(r => r.ID == 10).FirstOrDefault();
        Syphilis.Pos = l.Where(r => r.ID == 11).FirstOrDefault();
        Syphilis.NA = l.Where(r => r.ID == 43).FirstOrDefault();

        Malaria.Neg = l.Where(r => r.ID == 22).FirstOrDefault();
        Malaria.Pos = l.Where(r => r.ID == 23).FirstOrDefault();
        Malaria.NA = l.Where(r => r.ID == 45).FirstOrDefault();

        Component.Full = l.Where(r => r.ID == 25).FirstOrDefault();
        Component.RBC = l.Where(r => r.ID == 26).FirstOrDefault();//red blood cell
        Component.FFPlasma = l.Where(r => r.ID == 27).FirstOrDefault();// Huyết tương
        Component.FactorVIII = l.Where(r => r.ID == 28).FirstOrDefault();
        Component.PlateletApheresis = l.Where(r => r.ID == 29).FirstOrDefault();
        Component.Platelet = l.Where(r => r.ID == 30).FirstOrDefault();// Tiểu cầu
        Component.FFPlasma_Poor = l.Where(r => r.ID == 46).FirstOrDefault();
        Component.WBC = l.Where(r => r.ID == 47).FirstOrDefault(); // white blood cell

        Source.Donation = l.Where(r => r.ID == 37).FirstOrDefault();
        Source.RedCross = l.Where(r => r.ID == 38).FirstOrDefault();
        Source.Other = l.Where(r => r.ID == 40).FirstOrDefault();

        Substance.Non = l.Where(r => r.ID == 49).FirstOrDefault();
        Substance.Yes = l.Where(r => r.ID == 50).FirstOrDefault();
    }

    public static bool operator ==(TestDef td1, TestDef td2)
    {

        if ((object)td1 == null && (object)td2 == null) return true;

        if ((object)td1 == null || (object)td2 == null) return false;

        return (td1.ID == td2.ID);
    }

    public static bool operator !=(TestDef td1, TestDef td2)
    {
        return !(td1 == td2);
    }


    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            int count = (from e in db.TestDefs
                         where object.Equals(e.ParentID, this.ParentID) && e.Name == this.Name.Trim()
                         select e).Count();

            if (count > 0)
            {
                throw new Exception("Trùng tên");
            }
        }
    }
}


