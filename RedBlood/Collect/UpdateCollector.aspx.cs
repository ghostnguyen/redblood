using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_UpdateCollector : System.Web.UI.Page
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
        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (BarcodeBLL.IsValidPackCode(code))
        {
            if (!AutonumListIn.Contains(BarcodeBLL.ParsePackAutoNum(code)))
                AutonumListIn.Add(BarcodeBLL.ParsePackAutoNum(code));
            GridView1.DataBind();
        }
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = Get(db);
    }

    private List<Pack> Get(RedBloodDataContext db)
    {
        return PackBLL.Get(db, AutonumListIn, new List<Pack.StatusX> { Pack.StatusX.Collected, Pack.StatusX.Produced })
            .Where(r => r.ComponentID == TestDef.Component.Full).ToList();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string str = txtCollector.Text.Trim();

        if (string.IsNullOrEmpty(str))
            return;

        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack> l = Get(db);

        foreach (Pack item in l)
        {
            item.Collector = str;
        }
        db.SubmitChanges();

        GridView1.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        AutonumListIn.Clear();
        GridView1.DataBind();
    }
}
