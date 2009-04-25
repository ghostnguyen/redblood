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
public partial class Customer
{
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            if (string.IsNullOrEmpty(this.TaxNo) ||
                string.IsNullOrEmpty(this.TaxNo.Trim()))
                throw new Exception("Nhập MST khách hàng");

            if (string.IsNullOrEmpty(this.Name) ||
                string.IsNullOrEmpty(this.Name.Trim()))
                throw new Exception("Nhập tên khách hàng");

            if (this.TaxNo != "MST")
            {
                RedBloodDataContext db = new RedBloodDataContext();

                int count = (from cus in db.Customers
                             where object.Equals(cus.TaxNo, this.TaxNo.Trim()) && cus.ID != this.ID
                             select cus).Count();

                if (count > 0)
                {
                    throw new Exception("Trùng MST với khách hàng khác");
                }
            }
        }
    }
}
