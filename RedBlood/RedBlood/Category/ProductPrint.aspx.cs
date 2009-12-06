using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Category_ProductPrint : System.Web.UI.Page
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
        Product p = db.Products.Where(r => r.Code == code).FirstOrDefault();

        if (p == null) return;

        PrintSettingBLL.Reload();

        for (int i = 0; i < count; i++)
        {

            UserControl_ProductLabel uc = new UserControl_ProductLabel();
            uc = (UserControl_ProductLabel)LoadControl("~/UserControl/ProductLabel.ascx");
            uc.Fill_Letter(p.Code, p.Description);

            divCon.Controls.Add(uc);

            HtmlGenericControl gen = new HtmlGenericControl();
            gen.TagName = "div";
            gen.Attributes.Add("style", "page-break-after:always;");
            divCon.Controls.Add(gen);

        }



    }


}
