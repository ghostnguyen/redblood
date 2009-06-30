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

    string style_non = "";
    string style_select = "border:solid 1px red";

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

        DataListFull.DataSource = null;
        DataListFull.DataBind();

        DataListRBC.DataSource = null;
        DataListRBC.DataBind();

        DataListWBC.DataSource = null;
        DataListWBC.DataBind();

        DataListFFPlasma_Poor.DataSource = null;
        DataListFFPlasma_Poor.DataBind();

        DataListFFPlasma.DataSource = null;
        DataListFFPlasma.DataBind();

        DataListPlatelet.DataSource = null;
        DataListPlatelet.DataBind();

        DataListFactorVIII.DataSource = null;
        DataListFactorVIII.DataBind();

        DataListFFPlasma_Poor2.DataSource = null;
        DataListFFPlasma_Poor2.DataBind();

        divExtract.Visible = false;
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
        if (p.CanExtractTo.Count > 0)
        {
            divExtract.Visible = true;
        }

        List<Pack> l = p.RelatedPack
            .Where(r => r.ComponentID == TestDef.Component.Full)
            .FirstOrDefault()
            .RelatedPack;

        DataListFull.DataSource = l.Where(r => r.ComponentID == TestDef.Component.Full);
        DataListFull.DataBind();

        DataListRBC.DataSource = l.Where(r => r.ComponentID == TestDef.Component.RBC);
        DataListRBC.DataBind();

        DataListFFPlasma.DataSource = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma);
        DataListFFPlasma.DataBind();

        DataListFFPlasma_Poor.DataSource = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma_Poor
            && r.SourcePacks.Select(s => s.ComponentID).Contains(TestDef.Component.Full));
        DataListFFPlasma_Poor.DataBind();

        DataListWBC.DataSource = l.Where(r => r.ComponentID == TestDef.Component.WBC);
        DataListWBC.DataBind();

        DataListPlatelet.DataSource = l.Where(r => r.ComponentID == TestDef.Component.Platelet);
        DataListPlatelet.DataBind();

        DataListFactorVIII.DataSource = l.Where(r => r.ComponentID == TestDef.Component.FactorVIII);
        DataListFactorVIII.DataBind();

        DataListFFPlasma_Poor2.DataSource = l.Where(r => r.ComponentID == TestDef.Component.FFPlasma_Poor
            && r.SourcePacks.Select(s => s.ComponentID).Contains(TestDef.Component.FFPlasma));
        DataListFFPlasma_Poor2.DataBind();

        Highlight_Div(p);
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

    void Highlight_Div(Pack p)
    {
        if (p.ComponentID == TestDef.Component.Full)
        {
            divFull.Attributes.CssStyle.Add("border", "solid 1px red");
        }
        else
            divFull.Attributes.CssStyle.Add("border", "");
    }
}
