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
        DeletePack1.PackDeleted += new EventHandler(DeletePack1_PackDeleted);

        Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

        if (Master.TextBoxCode.Text.Length == 0) return;

        if (CodabarBLL.IsValidCampaignCode(Master.TextBoxCode.Text))
        {
            CampaignEnter(Master.TextBoxCode.Text);
        }

        Master.TextBoxCode.Text = "";
    }

    void DeletePack1_PackDeleted(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }

    private void CampaignEnter(string code)
    {
        CampaignDetail1.CampaignID = CodabarBLL.ParseCampaignID(code);
        GridView1.DataBind();
        GridViewOtherPack.DataBind();

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
            e.Result = PackBLL.GetByCampaign(CampaignDetail1.CampaignID);
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = PackBLL.Get((int)e.Keys[0], db);

        if (p != null)
        {
            PackBLL.Update(db, p,
                TestDefBLL.Get(db, e.NewValues["ComponentID"].ToInt()),
                e.NewValues["Volume"].ToIntNullable(),
                TestDefBLL.Get(db, e.NewValues["SubstanceID"].ToInt()));
            BloodTypeBLL.Update(db, p, 2,
                TestDefBLL.Get(db, e.NewValues["BloodType2.ABO.ID"].ToInt()),
                TestDefBLL.Get(db, e.NewValues["BloodType2.RH.ID"].ToInt()),
                Page.User.Identity.Name, "");
            TestResultBLL.Update(db, p, 2,
                TestDefBLL.Get(db, e.NewValues["TestResult2.HIV.ID"].ToInt()),
                TestDefBLL.Get(db, e.NewValues["TestResult2.HCV.ID"].ToInt()),
                TestDefBLL.Get(db, e.NewValues["TestResult2.HBsAg.ID"].ToInt()),
                TestDefBLL.Get(db, e.NewValues["TestResult2.Syphilis.ID"].ToInt()),
                TestDefBLL.Get(db, e.NewValues["TestResult2.Malaria.ID"].ToInt()),
                Page.User.Identity.Name, "");

            db.SubmitChanges();

            PackBLL.UpdateTestResultStatus4Full(p.Autonum);
        }

        e.Cancel = true;
        GridView1.EditIndex = -1;
    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void LinqDataSourcePackOther_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignDetail1.CampaignID == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {
            RedBloodDataContext db = new RedBloodDataContext();

            List<Pack.TestResultStatusX> trStatusL = new List<Pack.TestResultStatusX>();
            trStatusL.Add(Pack.TestResultStatusX.NegativeLocked);
            trStatusL.Add(Pack.TestResultStatusX.PositiveLocked);


            e.Result = db.Packs.Where(r => r.CampaignID == CampaignDetail1.CampaignID
                && trStatusL.Contains(r.TestResultStatus)
                );
        }
    }
}
