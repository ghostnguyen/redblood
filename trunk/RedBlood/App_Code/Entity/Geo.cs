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
/// Summary description for Cat
/// </summary>


public partial class Geo
{
    public static Guid BRVT = new Guid("9cfb06ed-137c-493c-9585-8ea6d4fc5b7b");
    public static Guid TayNinh = new Guid("e33105ec-306b-4567-8c40-f81b04e8fa71");

    GeoBLL bll = new GeoBLL();
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if(action == System.Data.Linq.ChangeAction.Insert
            || action== System.Data.Linq.ChangeAction.Update)
        {
            if (string.IsNullOrEmpty(this.Name) ||
             string.IsNullOrEmpty(this.Name.Trim()))
                throw new Exception("Nhập danh mục");

            
            RedBloodDataContext db = new RedBloodDataContext();

            int count = (from cats in db.Geos
                         where cats.ID != this.ID && object.Equals(cats.ParentID, this.ParentID) 
                         && cats.Name.Trim() == this.Name.Trim() 
                         select cats).Count();

            if (count > 0)
            {
                throw new Exception("Trùng tên");
            }

            bll.SetFullname(this);
        }
    }

   
}
