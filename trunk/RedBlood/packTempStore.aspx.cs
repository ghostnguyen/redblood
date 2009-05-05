using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackTempStore : System.Web.UI.Page
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

        DeletePack1.CampaignID = CodabarBLL.ParseCampaignID(code);
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
            //e.Result = PackBLL.GetByCampaingID4Manually(CampaignDetail1.CampaignID);
            e.Result = PackBLL.Get(CampaignDetail1.CampaignID, PackBLL.StatusListEnteringTestResult());
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //PackBLL.Update4Manually((int)e.Keys[0],
        //    null, e.NewValues["Volume"].ToIntNullable(), e.NewValues["BloodType2.ABO.ID"].ToIntNullable(), e.NewValues["BloodType2.RH.ID"].ToIntNullable(),
        //    null, null, null, null, null,
        //    Page.User.Identity.Name, "");

        RedBloodDataContext db;

        Pack p = PackBLL.GetByAutonum((int)e.Keys[0], out db, PackBLL.StatusListEnteringTestResult(), true);

        if (p != null)
        {
            PackBLL.Update(p, e.NewValues["ComponentID"].ToIntNullable(), e.NewValues["Volume"].ToIntNullable());
            BloodTypeBLL.Update(db, p, 2, 
                e.NewValues["BloodType2.ABO.ID"].ToIntNullable(), e.NewValues["BloodType2.RH.ID"].ToIntNullable(), 
                Page.User.Identity.Name, "");
            //TestResultBLL.Update(p, 2, hivID, hcvID, HBsAgID, syphilisID, malariaID, db, actor, note);

            PackBLL.VerifyCommitTestResult(db, p, "");

            db.SubmitChanges();
        }


        e.Cancel = true;
        GridView1.EditIndex = -1;
    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }


}
