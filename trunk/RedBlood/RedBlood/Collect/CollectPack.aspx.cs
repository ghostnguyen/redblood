using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_CollectPack : System.Web.UI.Page
{
    public List<string> DINList
    {
        get
        {
            if (ViewState["DINList"] == null)
            {
                ViewState["DINList"] = new List<string>();
            }
            return (List<string>)ViewState["DINList"];
        }
        set
        {
            ViewState["DINList"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (BarcodeBLL.IsValidDINCode(code))
        {
            if (!DINList.Contains(BarcodeBLL.ParseDIN(code)))
                DINList.Add(BarcodeBLL.ParseDIN(code));
            GridView1.DataBind();
        }
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = Get(db);
    }

    private List<Donation> Get(RedBloodDataContext db)
    {
        return db.Donations.Where(r => DINList.Contains(r.DIN)).ToList();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string str = txtCollector.Text.Trim();

        if (string.IsNullOrEmpty(str))
            return;

        RedBloodDataContext db = new RedBloodDataContext();

        List<Donation> l = Get(db);

        foreach (Donation item in l)
        {
            item.Collector = str;
        }
        db.SubmitChanges();

        GridView1.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        DINList.Clear();
        GridView1.DataBind();
    }
}
