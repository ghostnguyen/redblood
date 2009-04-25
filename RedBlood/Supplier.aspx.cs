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

public partial class SupplierPage : System.Web.UI.Page
{
    CompanyBLL companyBLL = new CompanyBLL();
    SupplierBLL bll = new SupplierBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Company com = companyBLL.Select_First();
        if (com != null) txtCompanyID.Text = com.ID.ToString();

        ucGeneralInfo.GeneralInfoChanged += new DetailsViewUpdatedEventHandler(ucGeneralInfo_GeneralInfoChanged);
        ucEditAccount.AccountChanged += new EventHandler(ucEditAccount_AccountChanged);
        ucEditLocation.LocationChanged += new EventHandler(ucEditLocation_LocationChanged);
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
        GridViewSupplierList.DataBind();
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        GridViewSupplierList.DataBind();
        UpdateSupplierID();
    }

    private void RefreshAllUserControl(string supplierID)
    {
        ucGeneralInfo.SetSupplierID(supplierID);
        ucEditAccount.SetSupplierID(supplierID);
        ucEditLocation.SetSupplierID(supplierID);
    }

    protected void MenuTab_MenuItemClick(object sender, MenuEventArgs e)
    {
        divGeneralInfo.Visible = MenuTab.Items[0].Selected;
        divEditAccount.Visible = MenuTab.Items[1].Selected;
        divEditLocation.Visible = MenuTab.Items[2].Selected;
    }
    protected void GridViewSupplierList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string ID = e.CommandArgument.ToString();

            RefreshAllUserControl(ID);
        }
    }
    private void UpdateSupplierID()
    {
        string ID = "";

        if (GridViewSupplierList.SelectedValue != null)
            ID = GridViewSupplierList.SelectedValue.ToString();

        RefreshAllUserControl(ID);
    }
    
    protected void LinkButtonDelete_Click(object sender, EventArgs e)
    {
        if (GridViewSupplierList.SelectedValue != null)
        {
            int i = bll.Delete(new Guid(GridViewSupplierList.SelectedValue.ToString()));

            //Can not delete
            if (i != 0)
            {
                ScriptManager.RegisterStartupScript(LinkButtonDelete, this.GetType(), "openpopup", "alert('Không thể xóa. Còn các thông tin khác liên quan.');", true);
            }
            else
            {
                GridViewSupplierList.DataBind();
                UpdateSupplierID();
            }
        }
    }

    protected void LinkButtonNew_Click(object sender, EventArgs e)
    {
        bll.Insert();
        GridViewSupplierList.DataBind();
        UpdateSupplierID();
    }
}
