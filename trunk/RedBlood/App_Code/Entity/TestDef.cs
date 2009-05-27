using System;
using System.Data;
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
    public enum HIV : int
    {
        Neg = 19,        
        Pos = 20,
        NA = 41
    }

    public enum HBsAg : int
    {
        Neg = 13,
        Pos = 14,
        NA = 42
    }

    public enum HCV : int
    {
        Neg = 16,
        Pos = 17,
        NA = 44
    }

    public enum Syphilis : int
    {
        Neg = 10,
        Pos = 11,
        NA = 43
    }

    public enum Malaria : int
    {
        Neg = 22,
        Pos = 23,
        NA = 45
    }

    public enum Component : int
    {
        Full = 25,
        PlateletKit = 29, // Kit Tiểu cầu

        RBC = 26, //red blood cell
        Plasma = 27, // Huyết tương
        Platelet = 30 // Tiểu cầu
        

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
