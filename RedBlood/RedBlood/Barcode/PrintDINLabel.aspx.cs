using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class Barcode_PrintDINLabel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        int numOfDIN, numOfCopy;

        try
        {
            numOfDIN = Request["numOfDIN"].ToString().ToInt();
            numOfCopy = Request["numOfCopy"].ToString().ToInt();
        }
        catch (Exception)
        {
            return;
        }


        PrintSettingBLL.Reload();
        List<Donation> l = DonationBLL.New(numOfDIN);

        foreach (Donation item in l)
        {
            for (int i = 0; i < numOfCopy; i++)
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

}
