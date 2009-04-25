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
public class CompanyBLL
{
    public CompanyBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Company Select_byID(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var companies = from s in db.Companies
                        where s.ID == ID
                        select s;


        if (companies.Count() != 1) return null;
        else return companies.First();
    }

    public Company Select_First()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from s in db.Companies
                select s).First();
    }

    public Guid? Get_DefaultAccountID(Guid ID)
    {
        Company s = Select_byID(ID);

        if (s == null) return null;
        else return s.DefaultBankAccountID;
    }

    public void Set_DefaultAccountID(Guid ID, Guid accountID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var companies = from s in db.Companies
                        where s.ID == ID
                        select s;


        if (companies.Count() != 1) return;

        Company com = companies.First();
        com.DefaultBankAccountID = accountID;

        db.SubmitChanges();
    }
}
