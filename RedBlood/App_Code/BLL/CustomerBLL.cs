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
/// Summary description for CustomerBLL
/// </summary>
public class CustomerBLL
{
    CompanyBLL companyBLL = new CompanyBLL();
    public CustomerBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Customer Select_byID(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var cuss = from c in db.Customers
                   where c.ID == ID
                   select c;


        if (cuss.Count() != 1) return null;
        else return cuss.First();
    }

    public Guid? Get_DefaultAccountID(Guid ID)
    {
        Customer s = Select_byID(ID);

        if (s == null) return null;
        else return s.DefaultBankAccountID;
    }

    public void Set_DefaultAccountID(Guid ID, Guid accountID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var cuss = from c in db.Customers
                   where c.ID == ID
                   select c;


        if (cuss.Count() != 1) return;

        Customer cus = cuss.First();
        cus.DefaultBankAccountID = accountID;

        db.SubmitChanges();
    }

    public Guid Insert(string name)
    {
        Company com = companyBLL.Select_First();
        if (com == null) return Guid.Empty;

        RedBloodDataContext db = new RedBloodDataContext();

        Customer c = new Customer();
        c.Name = name;
        c.TaxNo = "MST";
        c.CompanyID = com.ID;

        db.Customers.InsertOnSubmit(c);
        db.SubmitChanges();
        return c.ID;
    }

    public int Delete(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var cuss = from c in db.Customers
                   where c.ID == ID
                   select c;


        if (cuss.Count() != 1) return 1;

        db.Customers.DeleteOnSubmit(cuss.First());

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

    public void UpdateRedBlood(Guid ID, int value)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var cuss = from c in db.Customers
                   where c.ID == ID
                   select c;


        if (cuss.Count() != 1) return;

        Customer cus = cuss.First();

        if (!cus.RedBlood.HasValue) cus.RedBlood = 0;

        cus.RedBlood += value;

        db.SubmitChanges();
    }

    public void UpdatePoint(Guid? customerID, Guid? pointDefID, int value)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var r = from c in db.CustomerPoints
                where (c.CustomerID == customerID && c.PointDefID == pointDefID)
                select c;


        if (r.Count() == 0)
        {
            CustomerPoint cp = new CustomerPoint();
            cp.CustomerID = customerID;
            cp.PointDefID = pointDefID;
            cp.Point = value;
            db.CustomerPoints.InsertOnSubmit(cp);
            db.SubmitChanges();
        }
        else if (r.Count() == 1)
        {
            CustomerPoint cp = r.First();

            if (!cp.Point.HasValue) cp.Point = 0;

            cp.Point += value;

            db.SubmitChanges();
        }
        else return;



    }

    public object GetPointByID(Guid? ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var r = from def in db.PointDefs
                where def.Status == 1
                select new
                {
                    def.ID,
                    def.Name,
                    Point = (from cp in db.CustomerPoints
                             where (cp.CustomerID == ID && cp.PointDefID == def.ID)
                             select cp.Point).FirstOrDefault()

                };

        return r;
    }
}
