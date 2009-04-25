using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UserControl_CustomerGeneralInfo : System.Web.UI.UserControl
{
    public event DetailsViewUpdatedEventHandler GeneralInfoChanged;
    CustomerBLL bll = new CustomerBLL();
    PointDefBLL pointDefBLL = new PointDefBLL();

    protected void Page_Load(object sender, EventArgs e)
    {        

    }

    public void SetCustomerID(string ID)
    {
        txtCustomerID.Text = ID;

        DetailsViewCustomer.DataBind();
        GridViewCusPoint.DataBind();
    }
    protected void DetailsViewCustomer_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            ActionStatus.Text = e.Exception.Message;
            e.KeepInEditMode = true;
            e.ExceptionHandled = true;
        }
        else
        {
            ActionStatus.Text = "";
            if (GeneralInfoChanged != null)
                GeneralInfoChanged(sender, e);
        }
    }



    protected void DetailsViewCustomer_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel") ActionStatus.Text = "";

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int? value = txtRedBlood.Text.ToIntNullable();
        if (value.HasValue)
        {
            bll.UpdateRedBlood(txtCustomerID.Text.ToGuid(),value.Value);
            DetailsViewRedBlood.DataBind();
            txtRedBlood.Text = "";
        } 
       
        
    }
    protected void LinqDataSourcePoint_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = bll.GetPointByID(txtCustomerID.Text.ToGuid());
    }
    
    
    protected void GridViewCusPoint_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {        
        Guid pointDefID = (Guid)e.Keys[0];        

        TextBox txt = (TextBox)GridViewCusPoint.Rows[e.RowIndex].FindControl("txtPointAdd");
        int? value = txt.Text.ToIntNullable();

        if (value.HasValue)
        {
            bll.UpdatePoint(txtCustomerID.Text.ToGuid(), pointDefID, value.Value);
            GridViewCusPoint.DataBind();
            txt.Text = "";
        }                 
    }
}
