using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Order_Order : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Order1.OrderID = Request.Params["key"].ToInt(); ;
        }
    }

    protected void btnNew4People_Click(object sender, EventArgs e)
    {
        Order1.New(Order.TypeX.ToPeople);
    }

    protected void btnNew4Org_Click(object sender, EventArgs e)
    {
        Order1.New(Order.TypeX.ToOrg);
    }
}
