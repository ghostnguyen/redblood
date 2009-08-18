using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestResult_PackManually : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DeletePack1.PackDeleted += new EventHandler(DeletePack1_PackDeleted);

        Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

        if (Master.TextBoxCode.Text.Length == 0) return;

        if (BarcodeBLL.IsValidCampaignCode(Master.TextBoxCode.Text))
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
        CampaignDetail1.CampaignID = BarcodeBLL.ParseCampaignID(code);
        GridView1.DataBind();
        GridViewOtherPack.DataBind();

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
        //RedBloodDataContext db = new RedBloodDataContext();

        //Pack p = PackBLL.Get(db, (int)e.Keys[0]);

        //if (p != null)
        //{
        //    PackBLL.Update(db, p,
        //        e.NewValues["ComponentID"].ToInt(),
        //        e.NewValues["Volume"].ToIntNullable(),
        //        e.NewValues["SubstanceID"].ToInt());

        //    PackBLL.Update(db, p, 2,
        //        e.NewValues["ABO.ID"].ToInt(),
        //        e.NewValues["RH.ID"].ToInt(),
        //        "");

        //    //PackBLL.Update(db, p, 2,
        //    //    e.NewValues["HIV.ID"].ToInt(),
        //    //    e.NewValues["HCV.ID"].ToInt(),
        //    //    e.NewValues["HBsAg.ID"].ToInt(),
        //    //     e.NewValues["Syphilis.ID"].ToInt(),
        //    //    e.NewValues["Malaria.ID"].ToInt(),
        //    //     "");

        //    db.SubmitChanges();

        //    //PackBLL.UpdateTestResultStatus4Full(p.Autonum);
        //}

        //e.Cancel = true;
        //GridView1.EditIndex = -1;
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
            //RedBloodDataContext db = new RedBloodDataContext();

            //List<Pack.TestResultStatusX> trStatusL = new List<Pack.TestResultStatusX>();
            //trStatusL.Add(Pack.TestResultStatusX.NegativeLocked);
            //trStatusL.Add(Pack.TestResultStatusX.PositiveLocked);


            //e.Result = db.Packs.Where(r => r.CampaignID == CampaignDetail1.CampaignID
            //    && trStatusL.Contains(r.TestResultStatus)
            //    );
        }
    }
}
