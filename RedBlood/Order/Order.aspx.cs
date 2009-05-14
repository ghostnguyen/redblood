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
        Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

        if (Master.TextBoxCode.Text.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(Master.TextBoxCode.Text))
        {
            //PackCodeEnter(Master.TextBoxCode.Text);
        }
        else if (CodabarBLL.IsValidOrderCode(Master.TextBoxCode.Text))
        {
            Order1.OrderID = CodabarBLL.ParseOrderID(Master.TextBoxCode.Text);
        }
        else
        {
            //ucPeople.Code = Master.TextBoxCode.Text;
        }

        Master.TextBoxCode.Text = "";

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Order1.New(Order.Typex.ToOrg);
    }
}
