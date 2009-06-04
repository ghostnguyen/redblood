using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Production_Combine : System.Web.UI.Page
{
    public int PackOutAutonum
    {
        get
        {
            if (ViewState["PackOutAutonum"] == null)
            {
                return 0;
            }
            return (int)ViewState["PackOutAutonum"];
        }
        set
        {
            ViewState["PackOutAutonum"] = value;
        }
    }

    public List<int> PackInAutonumList
    {
        get
        {
            if (ViewState["PackInAutonumList"] == null)
            {
                ViewState["PackInAutonumList"] = new List<int>();
            }
            return (List<int>)ViewState["PackInAutonumList"];
        }
        set
        {
            ViewState["PackInAutonumList"] = value;
        }
    }

    public int CheckPackAutonum
    {
        get
        {
            if (ViewState["CheckPackAutonum"] == null)
            {
                return 0;
            }
            return (int)ViewState["CheckPackAutonum"];
        }
        set
        {
            ViewState["CheckPackAutonum"] = value;
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
            LoadPack(CodabarBLL.ParsePackAutoNum(code));
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

    protected void LinqDataSourcePackIn_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        //List<Pack> l = PackBLL.Get4Production_Combine(PackInAutonumList);
        List<Pack> l = PackBLL.Get(PackInAutonumList);
        if (l.Count == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else e.Result = l;
    }
    protected void LinqDataSourcePackOut_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        //Pack p = PackBLL.GetInitPack4Combine(PackOutAutonum);
        Pack p = PackBLL.Get(PackOutAutonum);
        if (p != null)
        {
            e.Result = p;
        }
        else
        {
            e.Result = null;
            e.Cancel = true;
        }
    }

    protected void GridViewPackIn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PackInAutonumList.Remove((int)e.Keys[0]);
        GridViewPackIn.DataBind();
        e.Cancel = true;

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (PackInAutonumList.Count == 0
            || PackOutAutonum == 0)
        {
            ScriptManager.RegisterStartupScript(btnOk, btnOk.GetType(), "Thông tin", "alert ('Không thể tạo tiểu cầu. Thiếu thông tin túi máu.');", true);
            return;
        }

        PackErr err = PackBLL.Combine2Platelet(PackInAutonumList, PackOutAutonum, Page.User.Identity.Name, txtNote.Text);
        if (err == PackErrList.Non)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Sản xuất thành công.');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('" + err.Message + "');", true);
    }

    private void LoadPack(int autonum)
    {
        if (PackBLL.Get4Production_Combine(autonum) != null)
        {
            ResetIfInLoadMode();

            if (!PackInAutonumList.Contains(autonum))
                PackInAutonumList.Add(autonum);
            GridViewPackIn.DataBind();
        }
        else if (PackBLL.GetInitPack4Combine(autonum) != null)
        {
            ResetIfInLoadMode();

            PackOutAutonum = autonum;
            GridViewPackOut.DataBind();
        }
        else if (PackBLL.IsCombined2Platelet(autonum) != null)
        {
            CheckPackAutonum = autonum;

            if (PackInAutonumList.Count == 0 || PackBLL.IsCombined2Platelet(PackOutAutonum) != null)
            {
                btnLoad_Click(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadConfirm", "doLoadPackCombined();", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông báo", "alert ('Dữ liệu không hợp lệ.');", true);
        }
    }


    protected void btnLoad_Click(object sender, EventArgs e)
    {
        Pack p = PackBLL.IsCombined2Platelet(CheckPackAutonum);
        if (p == null) return;

        if (p.ComponentID == (int)TestDef.Component.Platelet)
        {
            PackOutAutonum = p.Autonum;
            txtNote.Text = p.Note;
            PackInAutonumList = p.PackExtractsByExtract.Select(r => r.SourcePack.Autonum).ToList<int>();

        }
        else
        {
            PackExtract pe = p.PackExtractsBySource.Where(r => r.ExtractPack.ComponentID == (int)TestDef.Component.Platelet).FirstOrDefault();
            if (pe != null)
            {
                PackOutAutonum = pe.ExtractPack.Autonum;
                txtNote.Text = pe.ExtractPack.Note;
                PackInAutonumList = pe.ExtractPack.PackExtractsByExtract.Select(r => r.SourcePack.Autonum).ToList<int>();
            }
        }

        GridViewPackIn.DataBind();
        GridViewPackOut.DataBind();

        btnOk.Enabled = false;

        foreach (DataControlField item in GridViewPackIn.Columns)
        {
            if (item is CommandField)
            {
                (item as CommandField).ShowDeleteButton = false;
            }
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {

    }

    void ResetIfInLoadMode()
    {
        if (PackBLL.IsCombined2Platelet(PackOutAutonum) != null)
        {
            PackInAutonumList.Clear();
            PackOutAutonum = 0;

            GridViewPackIn.DataBind();
            GridViewPackOut.DataBind();

            btnOk.Enabled = true;

            foreach (DataControlField item in GridViewPackIn.Columns)
            {
                if (item is CommandField)
                {
                    (item as CommandField).ShowDeleteButton = true;
                }
            }
        }
    }
}
