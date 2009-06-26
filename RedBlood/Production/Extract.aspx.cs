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

        if (p == null
            ||
            (p.Err != PackErrList.Valid4Extract
            && p.Err != PackErrList.Extracted))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Exception", "alert ('CheckAutonum: " + p.Err.Message + "');", true);
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
    }

    protected void btnExtract_Click(object sender, EventArgs e)
    {
        List<int> l = new List<int>();

        foreach (ListItem item in CheckBoxListExtractTo.Items)
        {
            if (item.Selected)
                l.Add(item.Value.ToInt());
        }

        if (l.Contains(TestDef.Component.FFPlasma)
            && l.Contains(TestDef.Component.FFPlasma_Poor))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Chỉ được chọn 1 trong 2 loại huyết tương.');", true);
            return;
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
