using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_DeletePack : System.Web.UI.UserControl
{
    public event EventHandler PackDeleted;

    public int CampaignID
    {
        get
        {
            if (ViewState["CampaignID"] == null)
                return 0;
            return (int)ViewState["CampaignID"];
        }
        set
        {
            ViewState["CampaignID"] = value;
            if (value == 0)
            { }
            else
            {
                GridViewDeletePack.DataBind();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string err = "";
        if (string.IsNullOrEmpty(txtPackAutonum.Text.Trim())
            || txtPackAutonum.Text.Trim().ToInt() == 0)
        {
            err += "Nhập túi máu. ";
        }

        if (string.IsNullOrEmpty(txtDeleteNote.Text.Trim()))
        {
            err += "Nhập lý do hủy. ";
        }

        PackErr p_err = PackBLL.DeletePack(
            CampaignID,
            txtPackAutonum.Text.Trim().ToInt(),
            txtDeleteNote.Text.Trim()
            );


        if (p_err != null)
        {
            err += p_err.Message;
        }

        if (string.IsNullOrEmpty(err))
        {
            divErr.Attributes["class"] = "hidden";

            GridViewDeletePack.DataBind();
            if (PackDeleted != null) PackDeleted(null, null);
        }
        else
        {
            divErr.InnerText = err;
            divErr.Attributes["class"] = "err";
            return;
        }
    }

    protected void LinqDataSourceDeletePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignID == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {
            //e.Result = PackBLL.GetByCampaingID4Manually(CampaignID, new Pack.StatusX[] { Pack.StatusX.Delete });
            e.Result = PackBLL.GetByCampaign(CampaignID, new List<Pack.StatusX> { Pack.StatusX.Delete });
        }
    }
}
