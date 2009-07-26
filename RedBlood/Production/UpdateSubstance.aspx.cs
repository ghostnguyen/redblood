using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_UpdateSubstance : System.Web.UI.Page
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
        //if (!IsPostBack)
        //{
        //}

        //string code = Master.TextBoxCode.Text.Trim();
        //Master.TextBoxCode.Text = "";

        //if (code.Length == 0) return;

        //if (BarcodeBLL.IsValidPackCode(code))
        //{
        //    Autonum = BarcodeBLL.ParsePackAutoNum(code);
        //    LoadAutonum();
        //}
        //else
        //{

        //}
    }

    void LoadAutonum()
    {
        GridView1.DataBind();
    }


    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        //Pack p = PackBLL.Get(Autonum, new List<Pack.StatusX> { Pack.StatusX.Collected, Pack.StatusX.Produced });

        //if (p == null)
        //{
        //    e.Result = null;
        //    e.Cancel = true;
        //}
        //else e.Result = p;
    }
}
