using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_PackOrderCountInternally : System.Web.UI.UserControl
{
    public DateTime? From
    {
        get
        {
            return (DateTime?)ViewState["From"];
        }
        set
        {
            ViewState["From"] = value;
        }
    }

    public DateTime? To
    {
        get
        {
            return (DateTime?)ViewState["To"];
        }
        set
        {
            ViewState["To"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.LoadComplete += new EventHandler(Page_LoadComplete);

    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        //Count();
    }

    public void Count()
    {
        List<int> orderIDL = OrderBLL.Get(From, To, Order.TypeX.ToPeople).Select(r => r.ID).ToList();

        RedBloodDataContext db = new RedBloodDataContext();
        var v = from o in db.Orders
                from po in db.PackOrders
                from p in db.Packs
                where orderIDL.Contains(o.ID)
                && o.ID == po.OrderID && po.PackID == p.ID
                select new { o.FullDepartment, p.Autonum, Component = p.Component.Name, ml = p.Volume } 
        ;
                

        GridView1.DataSource = v;
        GridView1.DataBind();
    }
}
