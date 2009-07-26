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
            //ucPeople.Code = Request.Params["key"].ToString();
        }
        else
        {
            //ucEnterPack.PlateletApheresisConfirmed += new EventHandler(ucEnterPack_PlateletApheresisConfirmed);
            //ucPeople.PeopleChanged += new EventHandler(ucPeople_PeopleChanged);

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

    //void ucEnterPack_PlateletApheresisConfirmed(object sender, EventArgs e)
    //{
    //    PeopleHistory1.LoadPeople();
    //}

    void ucPeople_PeopleChanged(object sender, EventArgs e)
    {
        //ucEnterPack.PeopleID = (Guid)sender;
        //PeopleHistory1.PeopleID = (Guid)sender;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        //ucPeople.New("");
        //ucEnterPack.PeopleID = Guid.Empty;
    }

    private void DINEnter(string code)
    {
        string tempDIN = BarcodeBLL.ParseDIN(code);

        RedBloodDataContext db = new RedBloodDataContext();
        Donation d = DonationBLL.Get(tempDIN);

        if (d == null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi", "alert ('" + DonationErrEnum.NonExist.Message + "');", true);
            return;
        }

        if (d.PeopleID != null)
        {
            DIN = tempDIN;
            //ucPeople.PeopleID = d.PeopleID;
            //CamDetailLeft.CampaignID = d.CampaignID;
            //imgPack.ImageUrl = BarcodeBLL.Url4Product(d.OrgPack.ProductCode);
            //ucPDL.Load();
            return;
        }

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

        DIN = tempDIN;

        DonationErr err = DonationBLL.Assign(DIN, ucPeople.PeopleID, CamDetailLeft.CampaignID);

        if (err != DonationErrEnum.Non)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi",
                        "alert ('Túi máu: " + err.Message + "');", true);
        }
        else
        {
            //ucPDL.Load();
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
