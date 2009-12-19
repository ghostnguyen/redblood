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

        for (int i = 0; i < count / 2 + 1; i++)
        {

            Category_ProductLabelUserControl uc = new Category_ProductLabelUserControl();
            uc = (Category_ProductLabelUserControl)LoadControl("~/Category/ProductLabelUserControl.ascx");
            uc.Fill_Letter(p.Code, p.Description);
            uc.ResizeLabel1();

            divCon.Controls.Add(uc);

            Category_ProductLabelUserControl uc2 = new Category_ProductLabelUserControl();
            uc2 = (Category_ProductLabelUserControl)LoadControl("~/Category/ProductLabelUserControl.ascx");
            uc2.Fill_Letter(p.Code, p.Description);
            uc2.ResizeLabel2();

            divCon.Controls.Add(uc2);

            HtmlGenericControl gen = new HtmlGenericControl();
            gen.TagName = "div";
            gen.Attributes.Add("style", "page-break-after:always;");
            divCon.Controls.Add(gen);

        }



    }


}
