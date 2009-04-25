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
public partial class CompanyBankAccount
{
   partial void OnValidate(System.Data.Linq.ChangeAction action)
   {
      if (action == System.Data.Linq.ChangeAction.Insert
          || action == System.Data.Linq.ChangeAction.Update)
      {
          if (string.IsNullOrEmpty(this.No) || string.IsNullOrEmpty(this.No.Trim()))
            throw new Exception("Nhập số tài khoản");
      }
   }
}
