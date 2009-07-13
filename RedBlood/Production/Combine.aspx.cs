using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Production_Combine : System.Web.UI.Page
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

        }

        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(code))
        {
            CheckAutonum(CodabarBLL.ParsePackAutoNum(code));
        }
        else
        {

        }
    }

    protected void LinqDataSourcePackIn_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        List<Pack> l = PackBLL.Get(AutonumListIn);
        if (l.Count == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else e.Result = l;
    }
    protected void LinqDataSourcePackOut_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        Pack p = PackBLL.Get(AutonumListOut).FirstOrDefault();
        if (p != null)
        {
            e.Result = p;
            txtNote.Text = p.Note;
        }
        else
        {
            e.Result = null;
            e.Cancel = true;
        }
    }

    protected void GridViewPackIn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AutonumListIn.Remove((int)e.Keys[0]);
        GridViewPackIn.DataBind();
        e.Cancel = true;
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (AutonumListIn.Count == 0
            || AutonumListOut.Count == 0)
        {
            ScriptManager.RegisterStartupScript(btnOk, btnOk.GetType(), "Thông tin", "alert ('Không thể tạo tiểu cầu. Thiếu thông tin túi máu.');", true);
            return;
        }

        PackErr err = PackBLL.Combine2Platelet(AutonumListIn, AutonumListOut[0], Page.User.Identity.Name, txtNote.Text);
        if (err == PackErrList.Non)
        {
            IsEditMode = false;
            LoadPack();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('Sản xuất thành công.');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('" + err.Message + "');", true);
    }

    private void CheckAutonum(int autonum)
    {
        Pack p = PackBLL.Get4Combined2Platelet(autonum, Page.User.Identity.Name);

        if (p == null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Exception", "alert ('CheckAutonum');", true);
        }

        if (p.Err == PackErrList.IsPlatelet
        || p.Err == PackErrList.Combined2Platelet)
        {
            TempAutonum = autonum;

            if (IsEditMode)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadConfirm", "doLoadPackCombined();", true);
            }
            else
            {
                AutonumListIn.Clear();
                AutonumListOut.Clear();

                if (p.ComponentID == TestDef.Component.Full)
                {
                    PackExtract pe = p.PackExtractsBySource.Where(r => r.ExtractPack.ComponentID == TestDef.Component.Platelet).FirstOrDefault();

                    AutonumListIn = pe.ExtractPack.PackExtractsByExtract.Select(r => r.SourcePack.Autonum).ToList<int>();
                    AutonumListOut.Add(pe.ExtractPack.Autonum);
                }
                else if (p.ComponentID == TestDef.Component.Platelet)
                {
                    AutonumListIn = p.PackExtractsByExtract.Select(r => r.SourcePack.Autonum).ToList<int>();
                    AutonumListOut.Add(p.Autonum);
                }

                LoadPack();
            }
            return;
        }

        if (p.Err == PackErrList.Valid4Platelet)
        {
            if (IsEditMode)
            {
                if (!AutonumListIn.Contains(autonum))
                    AutonumListIn.Add(autonum);
            }
            else
            {
                AutonumListIn.Clear();
                AutonumListOut.Clear();
                IsEditMode = true;

                AutonumListIn.Add(autonum);
            }
            LoadPack();
            return;
        }

        if (p.Err == PackErrList.Init4Platelet)
        {
            if (IsEditMode)
            {
                AutonumListOut.Clear();
                AutonumListOut.Add(autonum);
            }
            else
            {
                AutonumListIn.Clear();
                AutonumListOut.Clear();
                IsEditMode = true;

                AutonumListOut.Add(autonum);
            }

            LoadPack();
            return;
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông báo", "alert ('" + p.Err.Message + "');", true);
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        Pack p = PackBLL.Get4Combined2Platelet(TempAutonum, Page.User.Identity.Name);

        if (p == null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Exception", "alert ('CheckAutonum');", true);
        }

        if (p.Err == PackErrList.IsPlatelet
            || p.Err == PackErrList.Combined2Platelet)
        {
            IsEditMode = false;
            CheckAutonum(TempAutonum);
        }
    }

    //bool IsInEditMode()
    //{
    //    return (AutonumListOut.Count == 0
    //        || PackBLL.GetInitPack4Combine(AutonumListOut[0]) != null);
    //}

    void LoadPack()
    {
        GridViewPackIn.DataBind();
        GridViewPackOut.DataBind();

        //Load GUI
        btnOk.Enabled = IsEditMode;
        foreach (DataControlField item in GridViewPackIn.Columns)
        {
            if (item is CommandField)
            {
                (item as CommandField).ShowDeleteButton = IsEditMode;
            }
        }
    }
}
