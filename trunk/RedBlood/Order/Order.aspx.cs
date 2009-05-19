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
        string code = Master.TextBoxCode.Text.Trim();
        //Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

        if (code.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(code))
        {
            //PackCodeEnter(Master.TextBoxCode.Text);
        }
        else if (CodabarBLL.IsValidOrderCode(code))
        {
            Order1.OrderID = CodabarBLL.ParseOrderID(code);
        }
        else if (CodabarBLL.IsValidPeopleCode(code))
        {
            Order1.Code = code;
        }
        else if (Master.TextBoxCode.Text.Length >= 9)
        {
            Order1.Code = code;
        }
        else
        { }

        Master.TextBoxCode.Text = "";

    }

    protected void btnNew4People_Click(object sender, EventArgs e)
    {
        Order1.New(Order.Typex.ToPeople);
    }

    protected void btnNew4Org_Click(object sender, EventArgs e)
    {
        Order1.New(Order.Typex.ToOrg);
    }
}
