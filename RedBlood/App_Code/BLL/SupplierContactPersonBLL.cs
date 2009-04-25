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
public class SupplierContactPersonBLL
{
    public SupplierContactPersonBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Insert(Guid supplierLocationID)
    { 
        RedBloodDataContext db = new RedBloodDataContext();
        
        SupplierContactPerson c = new SupplierContactPerson();
        c.FullName = "Họ và tên";
        c.SupplierLocationID = supplierLocationID;

        db.SupplierContactPersons.InsertOnSubmit(c);

        db.SubmitChanges();
    }
}
