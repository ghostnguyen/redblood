using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Order_Order : System.Web.UI.Page
{
    public Guid PeopleID
    {
        get
        {
            if (ViewState["PeopleID"] == null)
                return Guid.Empty;
            return (Guid)ViewState["PeopleID"];
        }
        set
        {
            //Clear();

            ViewState["PeopleID"] = value;
            if (value == null)
            { }
            else
            {
                //LoadPeople();
            }
            //if (PeopleChanged != null)
            //    PeopleChanged(value, null);
        }
    }

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
            People r = PeopleBLL.GetByCode(code);
            if (r != null)
            {
                //PeopleID = r.ID;
            }
        }
        else if (Master.TextBoxCode.Text.Length >= 9)
        {
            //People r = PeopleBLL.GetByCMND(Master.TextBoxCode.Text);
            //if (r != null)
            //{
            //    PeopleID = r.ID;
            //}
            //else
            //{
            //    New(Code);
            //}
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
