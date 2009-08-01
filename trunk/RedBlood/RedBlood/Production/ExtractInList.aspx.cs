using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_ExtractInList : System.Web.UI.Page
{
    public List<int> AutonumListIn
    {
        get
        {
            if (ViewState["AutonumListIn"] == null)
            {
                ViewState["AutonumListIn"] = new List<int>();
            }
            return (List<int>)ViewState["AutonumListIn"];
        }
        set
        {
            ViewState["AutonumListIn"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    CheckBoxListExtractTo.DataSource = new List<TestDef> { 
        //        TestDefBLL.Get(TestDef.Component.RBC)
        //        ,TestDefBLL.Get(TestDef.Component.WBC)
        //    ,TestDefBLL.Get(TestDef.Component.Platelet)
        //    ,TestDefBLL.Get(TestDef.Component.FFPlasma)
        //    ,TestDefBLL.Get(TestDef.Component.FFPlasma_Poor)};

        //    CheckBoxListExtractTo.DataBind();
        //}

        //string code = Master.TextBoxCode.Text.Trim();
        //Master.TextBoxCode.Text = "";

        //if (code.Length == 0) return;

        //if (BarcodeBLL.IsValidPackCode(code))
        //{
        //    if (!AutonumListIn.Contains(BarcodeBLL.ParsePackAutoNum(code)))
        //    {
        //        Pack p = PackBLL.Get4Extract(BarcodeBLL.ParsePackAutoNum(code));

        //        if (p != null
        //            && p.ComponentID == TestDef.Component.Full
        //            && p.Err == PackErrEnum.Valid4Extract)
        //        {
        //            AutonumListIn.Add(BarcodeBLL.ParsePackAutoNum(code));
        //            LoadAutonum();
        //        }
        //    }
        //}
    }

    void LoadAutonum()
    {
        //RedBloodDataContext db = new RedBloodDataContext();

        //List<Pack> l = PackBLL.Get4Extract(AutonumListIn).Where(r => r.ComponentID == TestDef.Component.Full).ToList();

        //GridViewFull.DataSource = l;
        //GridViewFull.DataBind();
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

        PackErr err = PackBLL.Extract(AutonumListIn, l);

        if (err == PackErrEnum.Non)
        {
            LoadAutonum();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Sản xuất thành công.');", true);
        }
        //else
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('" + err.Message + "');", true);

    }

    protected void CheckBoxListExtractTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<int> l = new List<int>();

        for (int i = 0; i < CheckBoxListExtractTo.Items.Count; i++)
        {
            GridViewFull.Columns[i + 1].Visible = CheckBoxListExtractTo.Items[i].Selected;
        }
    }

    protected void GridViewFull_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            AutonumListIn.Remove(e.CommandArgument.ToInt());
            LoadAutonum();
        }
    }
}
