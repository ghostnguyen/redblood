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
public partial class Warehouse
{
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            if (string.IsNullOrEmpty(this.Name) ||
                string.IsNullOrEmpty(this.Name.Trim()))
                throw new Exception("Nhập tên kho hàng.");

            if (string.IsNullOrEmpty(this.Code) ||
                string.IsNullOrEmpty(this.Code.Trim()))
                throw new Exception("Nhập mã kho hàng.");

            if (this.Code != "MaKhoHang")
            {
                RedBloodDataContext db = new RedBloodDataContext();

                int count = (from wh in db.Warehouses
                             where object.Equals(wh.Code, this.Code.Trim()) && wh.ID != this.ID
                             select wh).Count();

                if (count > 0)
                {
                    throw new Exception("Trùng mã kho hàng.");
                }
            }
        }
    }
}
