using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Category_BloodGroupPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = "";
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

        RedBloodDataContext db = new RedBloodDataContext();

        string desc = BloodGroupBLL.GetDescription(code);

        PrintSettingBLL.Reload();

        for (int i = 0; i < count; i++)
        {
            UserControl_BloodGroupLabel uc = new UserControl_BloodGroupLabel();
            uc = (UserControl_BloodGroupLabel)LoadControl("~/UserControl/BloodGroupLabel.ascx");
            uc.Fill_Letter(code,desc);

            divCon.Controls.Add(uc);

            HtmlGenericControl gen = new HtmlGenericControl();
            gen.TagName = "div";
            gen.Attributes.Add("style", "page-break-after:always;");
            divCon.Controls.Add(gen);
        }
    }


}
