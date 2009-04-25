using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class CompanyPage : System.Web.UI.Page
{
    CompanyBLL bll = new CompanyBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        ucGeneralInfo.GeneralInfoChanged += new DetailsViewUpdatedEventHandler(ucGeneralInfo_GeneralInfoChanged);
        ucEditAccount.AccountChanged += new EventHandler(ucEditAccount_AccountChanged);
        ucEditLocation.LocationChanged += new EventHandler(ucEditLocation_LocationChanged);

        if (!Page.IsPostBack)
        {
            Company c = bll.Select_First();
            string ID = "";
            if (c != null) ID = c.ID.ToString();

            RefreshAllUserControl(ID);
        }
    }

    void ucEditLocation_LocationChanged(object sender, EventArgs e)
    {
        ucGeneralInfo.RefeshLocation();
    }

    void ucEditAccount_AccountChanged(object sender, EventArgs e)
    {
        ucGeneralInfo.RefeshAccount();
    }

    void ucGeneralInfo_GeneralInfoChanged(object sender, DetailsViewUpdatedEventArgs e)
    {
        DetailsViewTitle.DataBind();
    }

    private void RefreshAllUserControl(string companyID)
    {
        ucGeneralInfo.SetCompanyID(companyID);
        ucEditAccount.SetCompanyID(companyID);
        ucEditLocation.SetCompanyID(companyID);
    }

    protected void MenuTab_MenuItemClick(object sender, MenuEventArgs e)
    {
        divGeneralInfo.Visible = MenuTab.Items[0].Selected;
        divEditAccount.Visible = MenuTab.Items[1].Selected;
        divEditLocation.Visible = MenuTab.Items[2].Selected;
    }
    protected void LinqDataSourceCompany_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = bll.Select_First();
    }
}
