using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_CollectPack : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ucPeople.Code = Request.Params["key"].ToString();
        }
        else
        {
            ucEnterPack.PlateletApheresisConfirmed += new EventHandler(ucEnterPack_PlateletApheresisConfirmed);
            ucPeople.PeopleChanged += new EventHandler(ucPeople_PeopleChanged);

            string code = Master.TextBoxCode.Text.Trim();
            Master.TextBoxCode.Text = "";

            if (code.Length == 0) return;

            if (BarcodeBLL.IsValidPackCode(code))
            {
                PackCodeEnter(code);
            }
            else if (BarcodeBLL.IsValidTestResultCode(code))
            {
                TestResultEnter(code);
            }
            else if (BarcodeBLL.IsValidCampaignCode(code))
            {
                CampaignEnter(code);
            }
            else
            {
                ucPeople.Code = code;
            }
        }
    }

    void ucEnterPack_PlateletApheresisConfirmed(object sender, EventArgs e)
    {
        PeopleHistory1.LoadPeople();
    }

    void ucPeople_PeopleChanged(object sender, EventArgs e)
    {
        ucEnterPack.PeopleID = (Guid)sender;
        PeopleHistory1.PeopleID = (Guid)sender;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ucPeople.New("");
        ucEnterPack.PeopleID = Guid.Empty;
    }

    private void PackCodeEnter(string code)
    {
        Pack p = PackBLL.GetByCode(code);
        if (p == null) return;

        if (p.PeopleID == null && p.CampaignID == null)
        {
            if (ucPeople.PeopleID == Guid.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi", "alert ('Chưa nhập thông tin người cho máu.');", true);
                return;
            }
            if (CamDetailLeft.CampaignID == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi", "alert ('Chưa nhập thông tin đợt thu máu.');", true);
                return;
            }

            ucEnterPack.Assign(p.Autonum, CamDetailLeft.CampaignID);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi",
                    "alert ('Túi máu: " + p.Status.ToString() + "');", true);
            return;
        }
    }

    private void TestResultEnter(string code)
    {

    }

    private void CampaignEnter(string code)
    {
        CamDetailLeft.CampaignID = BarcodeBLL.ParseCampaignID(code);
    }

}
