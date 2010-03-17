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
            Panel p = new Panel();
            p.Style.Add("position", "relative");
            p.Style.Add("page-break-after", "always");

            p.Height = 300;
            p.Width = 200;
            p.Style.Add("border", "1px solid white");
            divCon.Controls.Add(p);

            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label1, p);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label2, p);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label3, p);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label4, p);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label5, p);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label6, p);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label7, p);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label8, p);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label9, p);
            AddDINLabelControl(item, PrintSettingBLL.DINLabel.Label10, p);
        }
    }


    void AddDINLabelControl(Donation item, PrintSetting ps, Panel panel)
    {
        DINLabelUserControl uc = new DINLabelUserControl();
        uc = (DINLabelUserControl)LoadControl("~/Category/DINLabelUserControl.ascx");
        uc.Fill_Letter(item.DIN);
        uc.ResizeLabel(ps);

        panel.Controls.Add(uc);
        //divCon.Controls.Add(uc);
    }
}
