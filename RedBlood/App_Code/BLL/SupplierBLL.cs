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
/// Summary description for SupplierBLL
/// </summary>
public class SupplierBLL
{
    CompanyBLL companyBLL = new CompanyBLL();
    public SupplierBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Supplier Select_byID(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var suppliers = from s in db.Suppliers
                        where s.ID == ID
                        select s;


        if (suppliers.Count() != 1) return null;
        else return suppliers.First();
    }

    public Guid? Get_DefaultAccountID(Guid ID)
    {
        Supplier s = Select_byID(ID);

        if (s == null) return null;
        else return s.DefaultBankAccountID;
    }

    public void Set_DefaultAccountID(Guid ID, Guid accountID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var suppliers = from s in db.Suppliers
                        where s.ID == ID
                        select s;


        if (suppliers.Count() != 1) return;

        Supplier supp = suppliers.First();
        supp.DefaultBankAccountID = accountID;

        db.SubmitChanges();
    }

    public void Insert()
    {
        Company com = companyBLL.Select_First();
        if (com == null) return;

        RedBloodDataContext db = new RedBloodDataContext();

        Supplier s = new Supplier();
        s.Name = "_Tên nhà cung cấp";
        s.TaxNo = "MST";
        s.CompanyID = com.ID;

        db.Suppliers.InsertOnSubmit(s);
        db.SubmitChanges();
    }

    public int Delete(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var suppliers = from s in db.Suppliers
                        where s.ID == ID
                        select s;


        if (suppliers.Count() != 1) return 1;

        db.Suppliers.DeleteOnSubmit(suppliers.First());

        try
        {
            db.SubmitChanges();
        }
        catch (Exception)
        {
            return 1;
        }
        return 0;
    }
}
