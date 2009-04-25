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
using System.Text;

public partial class CustomerPage : System.Web.UI.Page
{
    CompanyBLL companyBLL = new CompanyBLL();
    CustomerBLL bll = new CustomerBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Company com = companyBLL.Select_First();
        if (com != null) txtCompanyID.Text = com.ID.ToString();

        ucGeneralInfo.GeneralInfoChanged += new DetailsViewUpdatedEventHandler(ucGeneralInfo_GeneralInfoChanged);


        String script;
        ClientScriptManager CSManager = Page.ClientScript;
        if (!CSManager.IsOnSubmitStatementRegistered(this.GetType(), "SaveScrollPosition"))
        {
            script = "var HiddenField = document.getElementById('" +
            hfScrollPosition.ClientID + "');\n\r";
            script += "var ScrollElement = document.getElementById('" +
            pGridView.ClientID + "');\n\r";
            script += "HiddenField.value = ScrollElement.scrollTop;\n\r";

            CSManager.RegisterOnSubmitStatement(this.GetType(),
            "SaveScrollPosition", script);
        }
        //if(!CSManager.IsStartupScriptRegistered(this.GetType(), "RetrieveScrollPosition"))
        if (true)
        {
            script = "var HiddenField = document.getElementById('" +
            hfScrollPosition.ClientID + "');\n\r";
            script += "var ScrollElement = document.getElementById('" +
            pGridView.ClientID + "');\n\r";

            script += "if(HiddenField.value != '')\n\r";
            script += "{\n\r";
            script += "ScrollElement.scrollTop = HiddenField.value;\n\r";
            script += "}\n\r";


            


            ScriptManager.RegisterStartupScript(GridViewCustomerList, this.GetType(),
            "RetrieveScrollPosition", script, true);

            //CSManager.RegisterStartupScript(this.GetType(),
            //"RetrieveScrollPosition", script, true);
        }

    }



    void ucGeneralInfo_GeneralInfoChanged(object sender, DetailsViewUpdatedEventArgs e)
    {
        DetailsViewTitle.DataBind();
        GridViewCustomerList.DataBind();
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        GridViewCustomerList.DataBind();
        UpdateCustomerID();
    }

    private void RefreshAllUserControl(string customerID)
    {
        ucGeneralInfo.SetCustomerID(customerID);

    }

    protected void MenuTab_MenuItemClick(object sender, MenuEventArgs e)
    {
        divGeneralInfo.Visible = MenuTab.Items[0].Selected;

    }
    protected void GridViewCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string ID = e.CommandArgument.ToString();

            RefreshAllUserControl(ID);


        }
    }
    private void UpdateCustomerID()
    {
        string ID = "";

        if (GridViewCustomerList.SelectedValue != null)
            ID = GridViewCustomerList.SelectedValue.ToString();

        RefreshAllUserControl(ID);
    }

    private void SelectCustomer(Guid ID)
    {

        foreach (GridViewRow row in GridViewCustomerList.Rows)
        {
            Guid value = (Guid)GridViewCustomerList.DataKeys[row.RowIndex].Value;
            if (value == ID)
            {
                GridViewCustomerList.SelectedIndex = row.RowIndex;

                string script = "";

                script += "var row14 = document.getElementById('" +
                    row.ClientID + "');\n\r";

                script += "row14.scrollIntoView(true);\n\r";


                ScriptManager.RegisterStartupScript(GridViewCustomerList, this.GetType(),
                "ScrollPositionToNew", script, true);

            }



        }

        RefreshAllUserControl(ID.ToString());
    }

    protected void LinkButtonDelete_Click(object sender, EventArgs e)
    {
        if (GridViewCustomerList.SelectedValue != null)
        {
            int i = bll.Delete(new Guid(GridViewCustomerList.SelectedValue.ToString()));

            //Can not delete
            if (i != 0)
            {
                ScriptManager.RegisterStartupScript(LinkButtonDelete, this.GetType(), "openpopup", "alert('Không thể xóa. Còn các thông tin khác liên quan.');", true);
            }
            else
            {
                GridViewCustomerList.DataBind();
                UpdateCustomerID();
            }
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text)) return;

        Guid ID = bll.Insert(txtName.Text);

        GridViewCustomerList.DataBind();
        SelectCustomer(ID);
    }
}
