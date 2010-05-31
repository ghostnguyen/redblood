using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class DonationCardUserControl : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Fill_Letter(Donation e)
    {
        lblName.Text = e.People.Name;
        lblName.Style.Apply(PrintSettingBLL.Card.Name);

        lblDOB.Text = e.People.DOBToString;
        lblDOB.Style.Apply(PrintSettingBLL.Card.DOB);

        imgAutonum.ImageUrl = BarcodeBLL.Url4People(e.People.Autonum);
        imgAutonum.Style.Apply(PrintSettingBLL.Card.Autonum);

        lblBloodGroup.Text = e.BloodGroupDesc;
        lblBloodGroup.Style.Apply(PrintSettingBLL.Card.BloodGroup);

        lblAddress.Text = e.People.FullResidentalAddress;
        lblAddress.Style.Apply(PrintSettingBLL.Card.Address);

        lblDonation1.Style.Apply(PrintSettingBLL.Card.lbl1);

        lblDonationDate1.Text = e.CollectedDate.ToStringVN();
        lblDonationDate1.Style.Apply(PrintSettingBLL.Card.Date1);

        divLabel.Style.Apply(PrintSettingBLL.Card.CardSize);
    }
}
