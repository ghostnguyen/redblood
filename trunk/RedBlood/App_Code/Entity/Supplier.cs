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
public partial class Supplier
{
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            if (string.IsNullOrEmpty(this.TaxNo) ||
                string.IsNullOrEmpty(this.TaxNo.Trim()))
                throw new Exception("Nhập MST nhà cung cấp");

            if (string.IsNullOrEmpty(this.Name) ||
                string.IsNullOrEmpty(this.Name.Trim()))
                throw new Exception("Nhập tên nhà cung cấp");

            if (this.TaxNo != "MST")
            {
                RedBloodDataContext db = new RedBloodDataContext();

                int count = (from supp in db.Suppliers
                             where object.Equals(supp.TaxNo, this.TaxNo.Trim()) && supp.ID != this.ID
                             select supp).Count();

                if (count > 0)
                {
                    throw new Exception("Trùng MST với nhà cung cấp khác");
                }
            }
        }
    }
}
