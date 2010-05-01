using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DINCertUserControl : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Fill_Letter(Donation e)
    {
        if (e.People == null
            || e.Pack == null
            || e.Campaign == null) return;

        if (e.Campaign.HostOrg != null
            && e.Campaign.HostOrg.Geo1 != null)
        {
            lblProvince1.Text = e.Campaign.HostOrg.Geo1.Name;
            lblProvince1.Style.Apply(PrintSettingBLL.DINCert.Province1);
        }

        lblName.Text = e.People.Name;
        lblName.Style.Apply(PrintSettingBLL.DINCert.Name);

        if (e.People.DOB != null)
        {
            lblDOBDate.Text = e.People.DOB.Value.Day.ToString();
            lblDOBDate.Style.Apply(PrintSettingBLL.DINCert.DOBDate);

            lblDOBMonth.Text = e.People.DOB.Value.Month.ToString();
            lblDOBMonth.Style.Apply(PrintSettingBLL.DINCert.DOBMonth);

            lblDOBYear.Text = e.People.DOB.Value.Year.ToString();
            lblDOBYear.Style.Apply(PrintSettingBLL.DINCert.DOBYear);
        }
        else if (e.People.DOBYear != null)
        {
            lblDOBYear.Text = e.People.DOBYear.ToString();
            lblDOBYear.Style.Apply(PrintSettingBLL.DINCert.DOBYear);
        }

        lblAddress.Text = e.People.FullResidentalAddress;
        lblAddress.Style.Apply(PrintSettingBLL.DINCert.Address);

        lblCMND.Text = e.People.CMND;
        lblCMND.Style.Apply(PrintSettingBLL.DINCert.CMND);

        lblOrg.Text = e.Campaign.HostOrg.Name;
        lblOrg.Style.Apply(PrintSettingBLL.DINCert.Org);

        if (e.OrgVolume == "250")
        {
            lblVol250.Text = "x";
            lblVol250.Style.Apply(PrintSettingBLL.DINCert.Vol250);
        }
        else if (e.OrgVolume == "350")
        {
            lblVol350.Text = "x";
            lblVol350.Style.Apply(PrintSettingBLL.DINCert.Vol350);
        }
        else if (e.OrgVolume == "450")
        {
            lblVol450.Text = "x";
            lblVol450.Style.Apply(PrintSettingBLL.DINCert.Vol450);
        }

        lblProvince2.Text = e.Campaign.HostOrg.Geo1.Name;
        lblProvince2.Style.Apply(PrintSettingBLL.DINCert.Province2);

        lblNowDate.Text = DateTime.Now.Day.ToString();
        lblNowDate.Style.Apply(PrintSettingBLL.DINCert.NowDate);

        lblNowMonth.Text = DateTime.Now.Month.ToString();
        lblNowMonth.Style.Apply(PrintSettingBLL.DINCert.NowMonth);

        lblNowYear.Text = DateTime.Now.Year.ToString();
        lblNowYear.Style.Apply(PrintSettingBLL.DINCert.NowYear);

        DivUC.Style.Apply(PrintSettingBLL.DINCert.CardSize);
    }
}
