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
public partial class Cat
{
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if(action == System.Data.Linq.ChangeAction.Insert
            || action== System.Data.Linq.ChangeAction.Update)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            int count = (from geos in db.Cats
                         where object.Equals(geos.ParentID, this.ParentID) && geos.Name == this.Name.Trim()
                         select geos).Count();

            if (count > 0)
            {
                throw new Exception("Trùng tên");
            }
        }
    }
}
