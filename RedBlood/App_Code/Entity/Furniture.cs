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
/// Summary description for Geo
/// </summary>
public partial class Furniture
{
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            if (string.IsNullOrEmpty(this.Code) ||
                string.IsNullOrEmpty(this.Code.Trim()))
                throw new Exception("Nhập mã sãn phẩm.");

            if (this.Code != "MaSP")
            {
                RedBloodDataContext db = new RedBloodDataContext();

                int count = (from i in db.Furnitures
                             where object.Equals(i.Code, this.Code.Trim()) && i.ID != this.ID
                             select i).Count();

                if (count > 0)
                {
                    throw new Exception("Trùng MaSP.");
                }
            }
        }
    }
}
