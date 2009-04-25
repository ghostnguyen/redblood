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
/// Summary description for WarehouseBLL
/// </summary>
public class WarehouseBLL
{
    CompanyBLL companyBLL = new CompanyBLL();
    public WarehouseBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Warehouse Select_byID(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var whs = from s in db.Warehouses
                        where s.ID == ID
                        select s;


        if (whs.Count() != 1) return null;
        else return whs.First();
    }


    public void Insert()
    {
        Company com = companyBLL.Select_First();
        if (com == null) return;

        RedBloodDataContext db = new RedBloodDataContext();

        Warehouse w = new Warehouse();
        w.Name = "_Tên kho hàng";
        w.Code = "MaKhoHang";
        w.CompanyID = com.ID;

        db.Warehouses.InsertOnSubmit(w);
        db.SubmitChanges();
    }

    public int Delete(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var whs = from w in db.Warehouses
                        where w.ID == ID
                        select w;


        if (whs.Count() != 1) return 1;

        db.Warehouses.DeleteOnSubmit(whs.First());

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
