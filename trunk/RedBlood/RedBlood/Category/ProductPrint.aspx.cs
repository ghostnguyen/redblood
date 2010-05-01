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
        Product item = db.Products.Where(r => r.Code == code).FirstOrDefault();

        if (item == null) return;

        PrintSettingBLL.Reload();

        for (int i = 0; i < count / 2 + 1; i++)
        {
            Panel p = new Panel();
            p.Style.Add("position", "relative");
            p.Style.Add("page-break-after", "always");
            p.Style.Apply(PrintSettingBLL.ProductLabel.PaperSize);
            p.Style.Add("border", "1px solid white");
            divCon.Controls.Add(p);

            AddControl(item, PrintSettingBLL.ProductLabel.Label1, p);
            AddControl(item, PrintSettingBLL.ProductLabel.Label2, p);
        }
    }

    void AddControl(Product item, PrintSetting ps, Panel panel)
    {
        Category_ProductLabelUserControl uc = new Category_ProductLabelUserControl();
        uc = (Category_ProductLabelUserControl)LoadControl("~/Category/ProductLabelUserControl.ascx");
        uc.Fill_Letter(item.Code, item.LabelDesc);
        uc.ResizeLabel(ps);

        panel.Controls.Add(uc);
    }


}
