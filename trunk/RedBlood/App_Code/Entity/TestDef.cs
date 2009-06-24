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
        public static int Neg = 19;
        public static int Pos = 20;
        public static int NA = 41;
    }

    public static class HBsAg
    {
        public static int Neg = 13;
        public static int Pos = 14;
        public static int NA = 42;
    }

    public static class HCV
    {
        public static int Neg = 16;
        public static int Pos = 17;
        public static int NA = 44;
    }

    public static class Syphilis
    {
        public static int Neg = 10;
        public static int Pos = 11;
        public static int NA = 43;
    }

    public static class Malaria
    {
        public static int Neg = 22;
        public static int Pos = 23;
        public static int NA = 45;
    }

    public static class Component
    {
        public static int Full = 25;
        public static int RBC = 26; //red blood cell
        public static int FFPlasma = 27; // Huyết tương
        public static int FactorVIII = 28;
        public static int PlateletApheresis = 29;
        public static int Platelet = 30; // Tiểu cầu
        public static int FFPlasma_Poor = 46;
        public static int WBC = 47;// white blood cell
    }

    public static class Source
    {
        public static int Donation = 37;
        public static int RedCross = 38;
        public static int Other = 40;
    }

    public static class Substance
    {
        public static int Non = 49;
        public static int Yes = 50;
    }

    //public static class HIV
    //{
    //    public static TestDef Neg;
    //    public static TestDef Pos;
    //    public static TestDef NA;
    //}

    //public static class HBsAg
    //{
    //    public static TestDef Neg;
    //    public static TestDef Pos;
    //    public static TestDef NA;
    //}

    //public static class HCV
    //{
    //    public static TestDef Neg;
    //    public static TestDef Pos;
    //    public static TestDef NA;
    //}

    //public static class Syphilis
    //{
    //    public static TestDef Neg;
    //    public static TestDef Pos;
    //    public static TestDef NA;
    //}

    //public static class Malaria
    //{
    //    public static TestDef Neg;
    //    public static TestDef Pos;
    //    public static TestDef NA;
    //}

    //public static class Component
    //{
    //    public static TestDef Full;
    //    public static TestDef RBC; //red blood cell
    //    public static TestDef FFPlasma; // Huyết tương
    //    public static TestDef FactorVIII;
    //    public static TestDef PlateletApheresis;
    //    public static TestDef Platelet; // Tiểu cầu
    //    public static TestDef FFPlasma_Poor;
    //    public static TestDef WBC;// white blood cell
    //}

    //public static class Source
    //{
    //    public static TestDef Donation;
    //    public static TestDef RedCross;
    //    public static TestDef Other;
    //}


    //public static class Substance
    //{
    //    public static TestDef Non;
    //    public static TestDef Yes;
    //}

    //public static List<TestDef> all;

    //static TestDef() 
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    all = db.TestDefs.ToList();

    //    HIV.Neg = TestDefBLL.GetConst(19);
    //    HIV.Pos = TestDefBLL.GetConst(20);
    //    HIV.NA = TestDefBLL.GetConst(41);

    //    HBsAg.Neg = all.Where(r => r.ID == 13).FirstOrDefault();
    //    HBsAg.Pos = all.Where(r => r.ID == 14).FirstOrDefault();
    //    HBsAg.NA = all.Where(r => r.ID == 42).FirstOrDefault();

    //    HCV.Neg = all.Where(r => r.ID == 16).FirstOrDefault();
    //    HCV.Pos = all.Where(r => r.ID == 17).FirstOrDefault();
    //    HCV.NA = all.Where(r => r.ID == 44).FirstOrDefault();

    //    Syphilis.Neg = all.Where(r => r.ID == 10).FirstOrDefault();
    //    Syphilis.Pos = all.Where(r => r.ID == 11).FirstOrDefault();
    //    Syphilis.NA = all.Where(r => r.ID == 43).FirstOrDefault();

    //    Malaria.Neg = all.Where(r => r.ID == 22).FirstOrDefault();
    //    Malaria.Pos = all.Where(r => r.ID == 23).FirstOrDefault();
    //    Malaria.NA = all.Where(r => r.ID == 45).FirstOrDefault();

    //    Component.Full = all.Where(r => r.ID == 25).FirstOrDefault();
    //    Component.RBC = all.Where(r => r.ID == 26).FirstOrDefault();//red blood cell
    //    Component.FFPlasma = all.Where(r => r.ID == 27).FirstOrDefault();// Huyết tương
    //    Component.FactorVIII = all.Where(r => r.ID == 28).FirstOrDefault();
    //    Component.PlateletApheresis = all.Where(r => r.ID == 29).FirstOrDefault();
    //    Component.Platelet = all.Where(r => r.ID == 30).FirstOrDefault();// Tiểu cầu
    //    Component.FFPlasma_Poor = all.Where(r => r.ID == 46).FirstOrDefault();
    //    Component.WBC = all.Where(r => r.ID == 47).FirstOrDefault(); // white blood cell

    //    Source.Donation = all.Where(r => r.ID == 37).FirstOrDefault();
    //    Source.RedCross = all.Where(r => r.ID == 38).FirstOrDefault();
    //    Source.Other = all.Where(r => r.ID == 40).FirstOrDefault();

    //    Substance.Non = all.Where(r => r.ID == 49).FirstOrDefault();
    //    Substance.Yes = all.Where(r => r.ID == 50).FirstOrDefault();
    //}

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


