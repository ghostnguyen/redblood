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

public partial class UserControl_SupplierGeneralInfo : System.Web.UI.UserControl
{
    public event DetailsViewUpdatedEventHandler GeneralInfoChanged;
    SupplierBLL bll = new SupplierBLL();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetSupplierID(string ID)
    {
        txtSupplierID.Text = ID;
        
        DetailsViewSupplier.DataBind();
        GridViewAccount.DataBind();
        GridViewLocation.DataBind();
        GridViewContact.DataBind();
    }
    protected void DetailsViewSupplier_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
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

    public void RefeshAccount()
    {
        GridViewAccount.DataBind();
        Select_DefaultAccount();
    }

    public void RefeshLocation()
    {
        GridViewLocation.DataBind();
        GridViewContact.DataBind();
    }

    private void Select_DefaultAccount()
    {
        if (string.IsNullOrEmpty(txtSupplierID.Text) || new Guid(txtSupplierID.Text) == Guid.Empty)
            return;

        Guid? defaultAccountID = bll.Get_DefaultAccountID(new Guid(txtSupplierID.Text));

        if (defaultAccountID == null) GridViewAccount.SelectedIndex = -1;
        else
        {
            for (int i = 0; i < GridViewAccount.DataKeys.Count; i++)
            {
                if ((Guid)GridViewAccount.DataKeys[i].Value == defaultAccountID.Value)
                {
                    GridViewAccount.SelectedIndex = i;
                }
            }
        }
    }
}
