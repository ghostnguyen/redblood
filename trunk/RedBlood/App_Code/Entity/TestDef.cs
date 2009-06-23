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
    public static TestDef HIV_Neg;
    public static TestDef HIV_Pos;
    public static TestDef HIV_NA;

    public static TestDef HBsAg_Neg;
    public static TestDef HBsAg_Pos;
    public static TestDef HBsAg_NA;

    public static TestDef HCV_Neg;
    public static TestDef HCV_Pos;
    public static TestDef HCV_NA;

    public static TestDef Syphilis_Neg;
    public static TestDef Syphilis_Pos;
    public static TestDef Syphilis_NA;

    public static TestDef Malaria_Neg;
    public static TestDef Malaria_Pos;
    public static TestDef Malaria_NA;



    static class Component
    {
        public static TestDef Full;
        //= 25,
        //RBC = 26, //red blood cell
        //FFPlasma = 27, // Huyết tương
        //FactorVIII = 28,
        //PlateletApheresis = 29,
        //Platelet = 30, // Tiểu cầu
        //FFPlasma_Poor = 46,
        //WBC = 47 // white blood cell
    }

    public enum Source : int
    {
        Donation = 37,
        RedCross = 38,
        Other = 40
    }

    public enum Substance : int
    {
        Non = 49,
        Yes = 50
    }

    static TestDef()
    {
        RedBloodDataContext db = new RedBloodDataContext();
        List<TestDef> l = db.TestDefs.ToList();

        HIV_Neg = l.Where(r => r.ID == 19).FirstOrDefault();
        HIV_Pos = l.Where(r => r.ID == 20).FirstOrDefault();
        HIV_NA = l.Where(r => r.ID == 41).FirstOrDefault();

        HBsAg_Neg = l.Where(r => r.ID == 13).FirstOrDefault();
        HBsAg_Pos = l.Where(r => r.ID == 14).FirstOrDefault();
        HBsAg_NA = l.Where(r => r.ID == 42).FirstOrDefault();

        HCV_Neg = l.Where(r => r.ID == 16).FirstOrDefault();
        HCV_Pos = l.Where(r => r.ID == 17).FirstOrDefault();
        HCV_NA = l.Where(r => r.ID == 44).FirstOrDefault();

        Syphilis_Neg = l.Where(r => r.ID == 10).FirstOrDefault();
        Syphilis_Pos = l.Where(r => r.ID == 11).FirstOrDefault();
        Syphilis_NA = l.Where(r => r.ID == 43).FirstOrDefault();

        Malaria_Neg = l.Where(r => r.ID == 22).FirstOrDefault();
        Malaria_Pos = l.Where(r => r.ID == 23).FirstOrDefault();
        Malaria_NA = l.Where(r => r.ID == 45).FirstOrDefault();

        
        
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


