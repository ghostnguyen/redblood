using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Linq.SqlClient;

/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService
{

    public AutoComplete()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetListGeo(string prefixText, int count)
    {
        string search = "%" + prefixText.Replace(" ", "%") + "%";
        RedBloodDataContext db = new RedBloodDataContext();

        var r = (from rs in db.Geos
                 // where search.Contains(rs.Fullname)
                 //where rs.Fullname.Contains(search1)
                 where SqlMethods.Like(rs.Fullname, search) || SqlMethods.Like(rs.FullnameNoDiacritics, search)
                 select rs.Fullname).Take(count);

        return r.ToArray();
    }

    [WebMethod]
    public string[] GetListOrg(string prefixText, int count)
    {
        string search = "%" + prefixText.Replace(" ", "%") + "%";
        RedBloodDataContext db = new RedBloodDataContext();

        var r = (from rs in db.Orgs
                 // where search.Contains(rs.Fullname)
                 //where rs.Fullname.Contains(search1)
                 where SqlMethods.Like(rs.Name, search) || SqlMethods.Like(rs.NameNoDiacritics, search)
                 select rs.Name).Take(count);

        return r.ToArray();
    }

}

