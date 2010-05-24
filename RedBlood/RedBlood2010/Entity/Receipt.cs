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
namespace RedBlood
{
    /// <summary>
    /// Summary description for Receipt
    /// </summary>
    public partial class Receipt
    {
        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            if (action == System.Data.Linq.ChangeAction.Insert
                || action == System.Data.Linq.ChangeAction.Update)
            {
                OnNameChanging(Name);
            }
        }

        partial void OnNameChanging(string value)
        {
            if (string.IsNullOrEmpty(value.Trim()))
                throw new Exception("Nhập tên.");

            if (ReceiptBLL.IsExistName(value.Trim(), ID))
                throw new Exception("Trùng tên");
        }
    }
}