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
/// Summary description for SupplierContactBLL
/// </summary>
public class CompanyContactPersonBLL
{
    public CompanyContactPersonBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Insert(Guid companyLocationID)
    { 
        RedBloodDataContext db = new RedBloodDataContext();
        
        CompanyContactPerson c = new CompanyContactPerson();
        c.FullName = "Họ và tên";
        c.CompanyLocationID = companyLocationID;

        db.CompanyContactPersons.InsertOnSubmit(c);

        db.SubmitChanges();
    }
}
