using System;
using System.Data;
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
/// Summary description for Warehouse
/// </summary>
public partial class WarehouseDivision
{
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            if (string.IsNullOrEmpty(this.Code) ||
                string.IsNullOrEmpty(this.Code.Trim()))
                throw new Exception("Nhập mã vị trí.");

            if (this.Code != "MaViTri")
            {
                RedBloodDataContext db = new RedBloodDataContext();

                int count = (from whd in db.WarehouseDivisions
                             where object.Equals(whd.Code, this.Code.Trim()) && whd.WarehouseID == this.WarehouseID && whd.ID != this.ID
                             select whd).Count();

                if (count > 0)
                {
                    throw new Exception("Trùng mã vị trí.");
                }
            }
        }
    }
}
