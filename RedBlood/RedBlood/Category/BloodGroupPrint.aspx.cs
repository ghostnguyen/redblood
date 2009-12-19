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

        for (int i = 0; i < count / 3 + 1; i++)
        {
            BloodGroupLabelUserControl uc = new BloodGroupLabelUserControl();
            uc = (BloodGroupLabelUserControl)LoadControl("~/Category/BloodGroupLabelUserControl.ascx");
            uc.Fill_Letter(code, desc);
            uc.ResizeLabel1();

            divCon.Controls.Add(uc);

            BloodGroupLabelUserControl uc2 = new BloodGroupLabelUserControl();
            uc2 = (BloodGroupLabelUserControl)LoadControl("~/Category/BloodGroupLabelUserControl.ascx");
            uc2.Fill_Letter(code, desc);
            uc2.ResizeLabel2();

            divCon.Controls.Add(uc2);

            BloodGroupLabelUserControl uc3 = new BloodGroupLabelUserControl();
            uc3 = (BloodGroupLabelUserControl)LoadControl("~/Category/BloodGroupLabelUserControl.ascx");
            uc3.Fill_Letter(code, desc);
            uc3.ResizeLabel3();

            divCon.Controls.Add(uc3);

            HtmlGenericControl gen = new HtmlGenericControl();
            gen.TagName = "div";
            gen.Attributes.Add("style", "page-break-after:always;");
            divCon.Controls.Add(gen);
        }
    }


}
