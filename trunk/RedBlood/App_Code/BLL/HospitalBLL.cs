using System;
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
/// Summary description for CompanyBLL
/// </summary>
public class HospitalBLL
{
    public HospitalBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Hospital Get(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from s in db.Hospitals
                        where s.ID == ID
                        select s).FirstOrDefault();
    }

    public static Hospital GetFirst()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from s in db.Hospitals
                select s).FirstOrDefault();
    }
}
