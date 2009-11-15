using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_DonationCard : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Fill_Letter(Donation e)
    {
        lblName.Text = e.People.Name;
        lblName.Style.Apply(PrintSettingBLL.Card.Name);

        lblAutoNum.Text = e.People.Autonum.ToString();
        lblAutoNum.Style.Apply(PrintSettingBLL.Card.Autonum);

        lblDonation1.Style.Apply(PrintSettingBLL.Card.lbl1);

        lblDonationDate1.Text = e.CollectedDate.ToStringVN();
        lblDonationDate1.Style.Apply(PrintSettingBLL.Card.Date1);
    }
}
