using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_Extract : System.Web.UI.Page
{
    public int Autonum
    {
        get
        {
            if (ViewState["Autonum"] == null)
            {
                return 0;
            }
            return (int)ViewState["Autonum"];
        }
        set
        {
            ViewState["Autonum"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //btnProduct.Enabled = false;
        }

        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(code))
        {
            Autonum = CodabarBLL.ParsePackAutoNum(code);
            LoadAutonum();
        }
        else if (CodabarBLL.IsValidTestResultCode(code))
        {

        }
        else if (CodabarBLL.IsValidCampaignCode(code))
        {

        }
        else
        {

        }
    }

    void Clear()
    {
        CheckBoxListExtractTo.Items.Clear();

        DetailsViewFull.DataSource = null;
        DetailsViewFull.DataBind();

        DetailsViewRBC.DataSource = null;
        DetailsViewRBC.DataBind();

        DetailsViewFFPlasma.DataSource = null;
        DetailsViewFFPlasma.DataBind();

        DetailsViewFFPlasma_Poor.DataSource = null;
        DetailsViewFFPlasma_Poor.DataBind();

        DetailsViewWBC.DataSource = null;
        DetailsViewWBC.DataBind();

        DetailsViewPlatelet.DataSource = null;
        DetailsViewPlatelet.DataBind();

        DetailsViewFactorVIII.DataSource = null;
        DetailsViewFactorVIII.DataBind();

    }

    void LoadAutonum()
    {
        Pack p = PackBLL.Get4Extract(Autonum, Page.User.Identity.Name);

        if (p == null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Exception", "alert ('CheckAutonum');", true);
            return;
        }

        Clear();

        CheckBoxListExtractTo.DataSource = TestDefBLL.Get(p.CanExtractTo);
        CheckBoxListExtractTo.DataBind();

        List<Pack> l = p.RelatedPack;

        DetailsViewFull.DataSource = l.Where(r => r.ComponentID == TestDef.Component.Full);
        DetailsViewFull.DataBind();

        DetailsViewRBC.DataSource = l.Where(r => r.ComponentID == TestDef.Component.RBC);
        DetailsViewRBC.DataBind();

        DetailsViewFFPlasma.DataSource = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma);
        DetailsViewFFPlasma.DataBind();

        DetailsViewFFPlasma_Poor.DataSource = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma_Poor);
        DetailsViewFFPlasma_Poor.DataBind();

        DetailsViewWBC.DataSource = l.Where(r => r.ComponentID == TestDef.Component.WBC);
        DetailsViewWBC.DataBind();

        DetailsViewPlatelet.DataSource = l.Where(r => r.ComponentID == TestDef.Component.Platelet);
        DetailsViewPlatelet.DataBind();

        DetailsViewFactorVIII.DataSource = l.Where(r => r.ComponentID == TestDef.Component.FactorVIII);
        DetailsViewFactorVIII.DataBind();

        //if (p.Err == PackErrList.Extracted)
        //{
        //    TempAutonum = autonum;

        //    if (IsEditMode)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadConfirm", "doLoadPackCombined();", true);
        //    }
        //    else
        //    {
        //        if (p.ComponentID == (int)TestDef.Component.FFPlasma
        //              || p.ComponentID == (int)TestDef.Component.RBC)
        //        {
        //            PackExtract pe = p.PackExtractsByExtract.FirstOrDefault();
        //            if (pe == null)
        //                p = null;
        //            else p = pe.SourcePack;
        //        }

        //        if (p != null)
        //        {
        //            AutonumListIn.Clear();
        //            AutonumListOut.Clear();

        //            AutonumListIn.Add(p.Autonum);
        //            AutonumListOut = p.PackExtractsBySource
        //            .Where(r => r.ExtractPack.ComponentID == (int)TestDef.Component.RBC
        //                || r.ExtractPack.ComponentID == (int)TestDef.Component.FFPlasma)
        //            .Select(r => r.ExtractPack.Autonum)
        //            .ToList();

        //            LoadPack();
        //        }
        //    }
        //    return;
        //}

        //if (p.Err == PackErrList.Valid4Extract)
        //{
        //    AutonumListIn.Clear();
        //    AutonumListOut.Clear();
        //    IsEditMode = true;

        //    AutonumListIn.Add(autonum);

        //    LoadPack();
        //    return;
        //}

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông báo", "alert ('" + p.Err.Message + "');", true);
    }

    protected void btnProduct_Click(object sender, EventArgs e)
    {
        //if (AutonumListIn.Count == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Thiếu thông tin túi máu.');", true);
        //    return;
        //}

        //PackErr err = PackBLL.Extract(AutonumListIn[0], Page.User.Identity.Name);

        //if (err == PackErrList.Non)
        //{
        //    IsEditMode = false;
        //    CheckAutonum(AutonumListIn[0]);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Sản xuất thành công.');", true);
        //}
        //else
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('" + err.Message + "');", true);

    }
    protected void btnExtract_Click(object sender, EventArgs e)
    {
        List<int> l = new List<int>();

        foreach (ListItem item in CheckBoxListExtractTo.Items)
        {
            if (item.Selected)
                l.Add(item.Value.ToInt());
        }

        PackErr err = PackBLL.Extract(Autonum, l, Page.User.Identity.Name);

        if (err == PackErrList.Non)
        {
            LoadAutonum();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Sản xuất thành công.');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('" + err.Message + "');", true);

    }
}
