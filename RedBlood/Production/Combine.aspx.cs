using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //PackInAutonumList.Add(301);
            //PackInAutonumList.Add(302);
        }

        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(code))
        {
            int autonum = CodabarBLL.ParsePackAutoNum(code);

            if (PackBLL.Get4Production(autonum) != null)
            {
                if (!PackInAutonumList.Contains(autonum))
                    PackInAutonumList.Add(autonum);
                GridViewPackIn.DataBind();
            }

            if (PackBLL.Get(autonum,new Pack.StatusX[]{Pack.StatusX.Init}) != null)
            {
                PackOutAutonum = autonum;
                GridViewPackOut.DataBind();
            }
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
        List<Pack> l = PackBLL.Get4Production(PackInAutonumList);
        if (l.Count == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else e.Result = l;
    }
    protected void LinqDataSourcePackOut_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        Pack p = PackBLL.Get(PackOutAutonum, new Pack.StatusX[] { Pack.StatusX.Init });
        if()
        e.Cancel = true;
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {

    }
}
