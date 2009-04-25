using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Enter : System.Web.UI.Page
{
    CodabarBLL codabarBLL = new CodabarBLL();
    PackBLL packBLL = new PackBLL();
    protected void Page_Load(object sender, EventArgs e)
    {

        ucPeople.PeopleChanged += new EventHandler(ucPeople_PeopleChanged);

        Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

        if (Master.TextBoxCode.Text.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(Master.TextBoxCode.Text))
        {
            PackCodeEnter(Master.TextBoxCode.Text);
        }
        else if (codabarBLL.IsValidTestResultCode(Master.TextBoxCode.Text))
        {
            TestResultEnter(Master.TextBoxCode.Text);
        }
        else if (CodabarBLL.IsValidCampaignCode(Master.TextBoxCode.Text))
        {
            CampaignEnter(Master.TextBoxCode.Text);
        }
        else
        {
            ucPeople.Code = Master.TextBoxCode.Text;
        }

        Master.TextBoxCode.Text = "";
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
            if (p.Status == Pack.StatusX.Delete)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi", 
                    "alert ('Túi máu đã hủy.');", true);
                return; 
            }
            ucPeople.PeopleID = p.PeopleID.Value;
        }
    }

    private void TestResultEnter(string code)
    {

    }

    private void CampaignEnter(string code)
    {
        CamDetailLeft.CampaignID = CodabarBLL.ParseCampaignID(code); 
    }

}
