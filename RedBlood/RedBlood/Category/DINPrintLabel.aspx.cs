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

        int numOfDIN = 0, numOfCopy = 0;

        try
        {
            numOfDIN = Request["numOfDIN"].ToString().ToInt();
        }
        catch (Exception)
        {
        }

        try
        {
            numOfCopy = Request["numOfCopy"].ToString().ToInt();
        }
        catch (Exception)
        {
        }


        PrintSettingBLL.Reload();
        List<Donation> l = DonationBLL.New(numOfDIN);

        foreach (Donation item in l)
        {
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label1);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label2);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label3);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label4);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label5);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label6);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label7);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label8);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label9);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label10);



            HtmlGenericControl gen = new HtmlGenericControl();
            gen.TagName = "div";
            gen.Attributes.Add("style", "page-break-after:always;");
            divCon.Controls.Add(gen);
        }
    }

    void AddDINLabelControl(Donation item, PrintSetting ps)
    {
        DINLabelUserControl uc = new DINLabelUserControl();
        uc = (DINLabelUserControl)LoadControl("~/Category/DINLabelUserControl.ascx");
        uc.Fill_Letter(item.DIN);
        uc.ResizeLabel(ps);

        divCon.Controls.Add(uc);
    }

}
