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

    public static class ABO    
    {
        public static int AB = 2;
        public static int A = 3;
        public static int B = 4;
        public static int O = 5;
    }

    public static class RH
    {
        public static int Pos = 7;
        public static int Neg = 8;
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


