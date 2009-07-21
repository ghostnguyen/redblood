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
/// Summary description for Department
/// </summary>


public partial class Department
{
    
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if(action == System.Data.Linq.ChangeAction.Insert
            || action== System.Data.Linq.ChangeAction.Update)
        {
            if (string.IsNullOrEmpty(this.Name) ||
             string.IsNullOrEmpty(this.Name.Trim()))
                throw new Exception("Nhập tên khoa");

            
            RedBloodDataContext db = new RedBloodDataContext();

            int count = (from cats in db.Departments
                         where cats.ID != this.ID && object.Equals(cats.ParentID, this.ParentID) 
                         && cats.Name.Trim() == this.Name.Trim() 
                         select cats).Count();

            if (count > 0)
            {
                throw new Exception("Trùng tên");
            }

            DepartmentBLL.SetFullname(this);
        }
    }

   
}
