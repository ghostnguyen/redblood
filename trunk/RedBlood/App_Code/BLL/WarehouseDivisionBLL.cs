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
public class WarehouseDivisionBLL
{

    public WarehouseDivisionBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Insert(Guid warehouseID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        WarehouseDivision whd = new WarehouseDivision();
        whd.Code = "MaViTri";
        whd.WarehouseID = warehouseID;

        db.WarehouseDivisions.InsertOnSubmit(whd);
        db.SubmitChanges();
    }
}
