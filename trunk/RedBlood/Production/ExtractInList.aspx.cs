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
        if (!IsPostBack)
        {
            CheckBoxListExtractTo.DataSource =
                TestDefBLL.Get(
                new List<int> { TestDef.Component.RBC
                    , TestDef.Component.WBC
                    , TestDef.Component.Platelet
                    , TestDef.Component.FFPlasma
                    , TestDef.Component.FFPlasma_Poor
                    });
            CheckBoxListExtractTo.DataBind();
        }

        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(code))
        {
            if (!AutonumListIn.Contains(CodabarBLL.ParsePackAutoNum(code)))
            {
                Pack p = PackBLL.Get4Extract(CodabarBLL.ParsePackAutoNum(code));

                if (p != null 
                    && p.ComponentID == TestDef.Component.Full 
                    && (p.Err == PackErrList.Valid4Extract || p.Err == PackErrList.Extracted))
                {
                    AutonumListIn.Add(CodabarBLL.ParsePackAutoNum(code));
                    LoadAutonum();
                }
            }
        }
    }

    void LoadAutonum()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack> l = PackBLL.Get(AutonumListIn).Where(r => r.ComponentID == TestDef.Component.Full).ToList();


    }

    protected void btnExtract_Click(object sender, EventArgs e)
    {
        //List<int> l = new List<int>();

        //foreach (ListItem item in CheckBoxListExtractTo.Items)
        //{
        //    if (item.Selected)
        //        l.Add(item.Value.ToInt());
        //}

        //if (l.Contains(TestDef.Component.FFPlasma)
        //    && l.Contains(TestDef.Component.FFPlasma_Poor))
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Chỉ được chọn 1 trong 2 loại huyết tương.');", true);
        //    return;
        //}

        //PackErr err = PackBLL.Extract(Autonum, l, Page.User.Identity.Name);

        //if (err == PackErrList.Non)
        //{
        //    LoadAutonum();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Sản xuất thành công.');", true);
        //}
        //else
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('" + err.Message + "');", true);

    }
}
