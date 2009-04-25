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
/// Summary description for CustomerContactBLL
/// </summary>
public class CustomerContactPersonBLL
{
    public CustomerContactPersonBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Insert(Guid customerLocationID)
    { 
        RedBloodDataContext db = new RedBloodDataContext();
        
        CustomerContactPerson c = new CustomerContactPerson();
        c.FullName = "Họ và tên";
        c.CustomerLocationID = customerLocationID;

        db.CustomerContactPersons.InsertOnSubmit(c);

        db.SubmitChanges();
    }
}
