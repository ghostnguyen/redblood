using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class Codabar_GenerateDIN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //List<Donation> l = DonationBLL.New(5);

        //foreach (Donation r in l)
        //{
        //    r.Note = BarcodeBLL.CalculateISO7064Mod37_2(r.DIN);
        //}

        //DataList1.DataSource = l;
        //DataList1.DataBind();

        PrintSettingBLL.Reload();
        List<Donation> l = DonationBLL.New(5);

        foreach (Donation item in l)
        {
            UserControl_DINLabel uc = new UserControl_DINLabel();
            uc = (UserControl_DINLabel)LoadControl("~/UserControl/DINLabel.ascx");
            uc.Fill_Letter(item.DIN);

            divCon.Controls.Add(uc);

            HtmlGenericControl gen = new HtmlGenericControl();
            gen.TagName = "div";
            gen.Attributes.Add("style", "page-break-after:always;");
            divCon.Controls.Add(gen);
        }
    }

}
