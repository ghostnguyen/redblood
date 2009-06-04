using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_Extract : System.Web.UI.Page
{
    public List<int> AutonumListOut
    {
        get
        {
            if (ViewState["AutonumListOut"] == null)
            {
                ViewState["AutonumListOut"] = new List<int>();
            }
            return (List<int>)ViewState["AutonumListOut"];
        }
        set
        {
            ViewState["AutonumListOut"] = value;
        }
    }

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

    public int TempAutonum
    {
        get
        {
            if (ViewState["TempAutonum"] == null)
            {
                return 0;
            }
            return (int)ViewState["TempAutonum"];
        }
        set
        {
            ViewState["TempAutonum"] = value;
        }
    }

    public bool IsEditMode
    {
        get
        {
            if (ViewState["IsEditMode"] == null)
            {
                return true;
            }
            return (bool)ViewState["IsEditMode"];
        }
        set
        {
            ViewState["IsEditMode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnProduct.Enabled = false;
        }

        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(code))
        {
            CheckAutonum(CodabarBLL.ParsePackAutoNum(code));
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
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        Pack p = PackBLL.Get(AutonumListIn).FirstOrDefault();
        if (p == null)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else e.Result = p;
    }

    protected void LinqDataSourceExtract_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        List<Pack> l = PackBLL.Get(AutonumListOut);
        if (l.Count == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else e.Result = l;
    }

    void CheckAutonum(int autonum)
    {
        Pack p;
        p = PackBLL.Get4Production_Extract(autonum);
        if (p != null)
        {
            AutonumListIn.Clear();
            AutonumListOut.Clear();
            IsEditMode = true;

            AutonumListIn.Add(autonum);

            LoadPack();
            return;
        }

        p = PackBLL.IsExtracted(autonum);
        if (p != null)
        {
            TempAutonum = autonum;

            if (IsEditMode)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadConfirm", "doLoadPackCombined();", true);
            }
            else
            {
                if (p.ComponentID == (int)TestDef.Component.Platelet
                      || p.ComponentID == (int)TestDef.Component.RBC)
                {
                    PackExtract pe = p.PackExtractsByExtract.FirstOrDefault();
                    if (pe == null)
                        p = null;
                    else p = pe.SourcePack;
                }

                if (p != null)
                {
                    AutonumListIn.Clear();
                    AutonumListOut.Clear();

                    AutonumListIn.Add(p.Autonum);
                    AutonumListOut = p.PackExtractsBySource
                    .Where(r => r.ExtractPack.ComponentID == (int)TestDef.Component.RBC
                        || r.ExtractPack.ComponentID == (int)TestDef.Component.Plasma)
                    .Select(r => r.ExtractPack.Autonum)
                    .ToList();

                    LoadPack();

                }
            }
            return;
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông báo", "alert ('Dữ liệu không hợp lệ.');", true);
    }

    void LoadPack()
    {
        DetailsViewPack.DataBind();
        GridViewExtract.DataBind();

        //Load GUI
        btnProduct.Enabled = IsEditMode;
    }

    protected void btnProduct_Click(object sender, EventArgs e)
    {
        if (AutonumListIn.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Thiếu thông tin túi máu.');", true);
            return;
        }

        PackErr err = PackBLL.Extract(AutonumListIn[0], Page.User.Identity.Name);

        if (err == PackErrList.Non)
        {
            IsEditMode = false;
            CheckAutonum(AutonumListIn[0]);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Sản xuất thành công.');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('" + err.Message + "');", true);

    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        Pack p = PackBLL.IsExtracted(TempAutonum);
        if (p == null) return;

        IsEditMode = false;
        CheckAutonum(TempAutonum);
    }
}
