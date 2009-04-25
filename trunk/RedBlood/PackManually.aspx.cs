using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackManually : System.Web.UI.Page
{
    CampaignBLL campaignBLL = new CampaignBLL();
    PackBLL packBLL = new PackBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

        if (Master.TextBoxCode.Text.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(Master.TextBoxCode.Text))
        {
            //PackCodeEnter(Master.TextBoxCode.Text);
        }
        else if (CodabarBLL.IsValidCampaignCode(Master.TextBoxCode.Text))
        {
            CampaignEnter(Master.TextBoxCode.Text);
        }
        else
        {
            //ucPeople.Code = Master.TextBoxCode.Text;
        }

        Master.TextBoxCode.Text = "";
    }

    private void CampaignEnter(string code)
    {
        CampaignDetail1.CampaignID = CodabarBLL.ParseCampaignID(code);
        GridView1.DataBind();
        GridViewDeletePack.DataBind();
    }

    protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignDetail1.CampaignID == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {
            //e.Result = campaignBLL.GetByID(CampaignDetail1.CampaignID).Packs;
            e.Result = PackBLL.GetByCampaingID4Manually(CampaignDetail1.CampaignID);
        }
    }

    protected void LinqDataSourceDeletePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignDetail1.CampaignID == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {
            //e.Result = campaignBLL.GetByID(CampaignDetail1.CampaignID).Packs;
            e.Result = PackBLL.GetByCampaingID4Manually(
                CampaignDetail1.CampaignID, new Pack.StatusX[] { Pack.StatusX.Delete });
        }
    }



    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        PackBLL.Update4Manually((int)e.Keys[0],
            e.NewValues["ComponentID"].ToIntNullable(), e.NewValues["Volume"].ToIntNullable(), e.NewValues["BT.ABOID"].ToIntNullable(), e.NewValues["BT.RHID"].ToIntNullable(),
            e.NewValues["TR.HIVID"].ToIntNullable(), e.NewValues["TR.HCVID"].ToIntNullable(), e.NewValues["TR.HBsAgID"].ToIntNullable(), e.NewValues["TR.SyphilisID"].ToIntNullable(), e.NewValues["TR.MalariaID"].ToIntNullable(),
            Page.User.Identity.Name, "");

        e.Cancel = true;
        GridView1.EditIndex = -1;

    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
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
            CampaignDetail1.CampaignID,
            txtPackAutonum.Text.Trim().ToInt(),
            txtDeleteNote.Text.Trim(),
            Page.User.Identity.Name);


        if (p_err != null)
        {
            err += p_err.Message;
        }

        if (string.IsNullOrEmpty(err))
        {
            divErr.Attributes["class"] = "hidden";

            GridView1.DataBind();
            GridViewDeletePack.DataBind();
        }
        else
        {
            divErr.InnerText = err;
            divErr.Attributes["class"] = "err";
            return;
        }
    }
}
