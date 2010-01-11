using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class Category_DINPrintLabel : System.Web.UI.Page
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
            for (int i = 0; i < (numOfCopy +1 ) / 2 ; i++)
            {
                DINLabelUserControl uc = new DINLabelUserControl();
                uc = (DINLabelUserControl)LoadControl("~/Category/DINLabelUserControl.ascx");
                uc.Fill_Letter(item.DIN);
                uc.ResizeLabel1();

                divCon.Controls.Add(uc);

                DINLabelUserControl uc2 = new DINLabelUserControl();
                uc2 = (DINLabelUserControl)LoadControl("~/Category/DINLabelUserControl.ascx");
                uc2.Fill_Letter(item.DIN);
                uc2.ResizeLabel2();

                divCon.Controls.Add(uc2);

                HtmlGenericControl gen = new HtmlGenericControl();
                gen.TagName = "div";
                gen.Attributes.Add("style", "page-break-after:always;");
                divCon.Controls.Add(gen);
            }
        }
    }

}
