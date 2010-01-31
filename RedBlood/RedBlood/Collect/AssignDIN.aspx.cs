using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_AssignDIN : System.Web.UI.Page
{
    public string DIN
    {
        get
        {
            if (ViewState["DIN"] == null)
            {
                ViewState["DIN"] = "";
            }
            return (string)ViewState["DIN"];
        }
        set
        {
            ViewState["DIN"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
        else
        {
            ucPeople.PeopleChanged += new EventHandler(ucPeople_PeopleChanged);

            string code = Master.TextBoxCode.Text.Trim();
            Master.TextBoxCode.Text = "";

            if (code.Length == 0) return;

            if (BarcodeBLL.IsValidDINCode(code))
            {
                DINEnter(code);
            }
            else if (BarcodeBLL.IsValidCampaignCode(code))
            {
                CampaignEnter(code);
            }
            else if (BarcodeBLL.IsValidProductCode(code))
            {

            }
            else
            {
                ucPeople.Code = code;
            }
        }
    }

    void ucPeople_PeopleChanged(object sender, EventArgs e)
    {
        ucPDL.PeopleID = ucPeople.PeopleID;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ucPeople.New("");
    }

    private void DINEnter(string code)
    {
        string tempDIN = BarcodeBLL.ParseDIN(code);

        RedBloodDataContext db = new RedBloodDataContext();
        Donation d = DonationBLL.Get(tempDIN);

        if (d == null)
        {
            this.Alert(DonationErrEnum.NonExist.Message);
            return;
        }

        if (d.PeopleID != null)
        {
            DIN = tempDIN;
            ucPDL.PeopleID = d.PeopleID.Value;
            return;
        }

        if (ucPeople.PeopleID == Guid.Empty)
        {
            this.Alert("Chưa nhập thông tin người cho máu.");
            return;
        }
        if (CamDetailLeft.CampaignID == 0)
        {
            this.Alert("Chưa nhập thông tin đợt thu máu.");
            return;
        }

        DIN = tempDIN;

        DonationErr err = DonationBLL.Assign(DIN, ucPeople.PeopleID, CamDetailLeft.CampaignID);

        if (err != DonationErrEnum.Non)
        {
            this.Alert("Túi máu: " + err.Message);
        }
        else
        {
            ucPDL.ShowLog();
        }

        return;
    }

    private void CampaignEnter(string code)
    {
        CamDetailLeft.CampaignID = BarcodeBLL.ParseCampaignID(code);
    }

    private void ProductCodeEnter(string code)
    {

    }

}
