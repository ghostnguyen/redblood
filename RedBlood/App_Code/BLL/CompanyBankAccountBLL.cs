using System;
using System.Data;
using System.Data.Linq;
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
/// Summary description for GeoBLL
/// </summary>
public class CompanyBankAccountBLL
{
    public CompanyBankAccountBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Insert(Guid companyID, Guid bankID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        CompanyBankAccount acc = new CompanyBankAccount();
        acc.CompanyID = companyID;
        acc.BankID = bankID;
        acc.Name = "Tên tài khoản";
        acc.No = "Nhập số tài khoản";

        db.CompanyBankAccounts.InsertOnSubmit(acc);
        db.SubmitChanges();
        return "";
    }
}
