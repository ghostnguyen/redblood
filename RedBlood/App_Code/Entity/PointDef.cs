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
public partial class PointDef
{
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            if (string.IsNullOrEmpty(this.Name) ||
                string.IsNullOrEmpty(this.Name.Trim()))
                throw new Exception("Nhập tên");


            RedBloodDataContext db = new RedBloodDataContext();

            int count = (from r in db.PointDefs
                         where object.Equals(r.Name, this.Name.Trim()) && r.ID != this.ID
                         select r).Count();

            if (count > 0)
            {
                throw new Exception("Trùng tên.");
            }

        }
    }
}
