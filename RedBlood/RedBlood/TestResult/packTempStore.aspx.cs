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
        DeletePack1.PackDeleted += new EventHandler(DeletePack1_PackDeleted);

        Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

        if (Master.TextBoxCode.Text.Length == 0) return;

if (BarcodeBLL.IsValidCampaignCode(Master.TextBoxCode.Text))
        {
            CampaignEnter(Master.TextBoxCode.Text);
        }
        else
        {
            //ucPeople.Code = Master.TextBoxCode.Text;
        }

        Master.TextBoxCode.Text = "";
    }

    void DeletePack1_PackDeleted(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }

    private void CampaignEnter(string code)
    {
        CampaignDetail1.CampaignID = BarcodeBLL.ParseCampaignID(code);
        GridView1.DataBind();

        DeletePack1.CampaignID = BarcodeBLL.ParseCampaignID(code);
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
            //e.Result = PackBLL.GetByCampaign(CampaignDetail1.CampaignID);
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = PackBLL.Get(db, (int)e.Keys[0]);

        if (p != null)
        {
            //PackBLL.Update(db, p, e.NewValues["ComponentID"].ToIntNullable(), e.NewValues["Volume"].ToIntNullable(), e.NewValues["SubstanceID"].ToIntNullable());
            //BloodTypeBLL.Update(db, p, 2, 
            //    e.NewValues["BloodType2.ABO.ID"].ToIntNullable(), e.NewValues["BloodType2.RH.ID"].ToIntNullable(), 
            //    Page.User.Identity.Name, "");

            PackBLL.Update(db, p,
               e.NewValues["ComponentID"].ToInt(),
               e.NewValues["Volume"].ToIntNullable(),
               e.NewValues["SubstanceID"].ToInt());

            PackBLL.Update(db, p, 2,
                e.NewValues["ABO.ID"].ToInt(),
                e.NewValues["RH.ID"].ToInt(),
                "");

            db.SubmitChanges();
        }

        e.Cancel = true;
        GridView1.EditIndex = -1;
    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }


}
