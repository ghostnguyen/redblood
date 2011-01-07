using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using RedBlood;
using RedBlood.BLL;
public partial class Category_BloodGroupPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = "";
        string addText = "";
        int count = 0;

        try
        {
            code = Request["code"];
        }
        catch (Exception)
        {

        }

        try
        {
            count = Request["count"].ToInt();
        }
        catch (Exception)
        {

        }

        try
        {
            addText = Request["addText"];
        }
        catch (Exception)
        {

        }

        PrintSettingBLL.Reload();
        RedBloodDataContext db = new RedBloodDataContext();
        string desc = BloodGroupBLL.GetDescription(code);

        for (int i = 0; i < count; i++)
        {
            Panel p = new Panel();
            p.Style.Add("position", "relative");
            p.Style.Add("page-break-after", "always");
            p.Style.Apply(PrintSettingBLL.BloodGroupLabel.PaperSize);
            p.Style.Add("border", "1px solid white");
            divCon.Controls.Add(p);

            AddDINLabelControl(code, desc + addText, PrintSettingBLL.BloodGroupLabel.Label1, p);
            AddDINLabelControl(code, desc + addText, PrintSettingBLL.BloodGroupLabel.Label2, p);
            AddDINLabelControl(code, desc + addText, PrintSettingBLL.BloodGroupLabel.Label3, p);
        }
    }

    void AddDINLabelControl(string code, string desc, PrintSetting ps, Panel panel)
    {
        BloodGroupLabelUserControl uc = new BloodGroupLabelUserControl();
        uc = (BloodGroupLabelUserControl)LoadControl("~/Category/BloodGroupLabelUserControl.ascx");
        uc.Fill_Letter(code, desc);
        uc.ResizeLabel(ps);

        panel.Controls.Add(uc);
    }


}
