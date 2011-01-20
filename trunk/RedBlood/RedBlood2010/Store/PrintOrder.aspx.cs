using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Store_PrintOrder : System.Web.UI.Page
{
    public Order order { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strOrderID = Request["OrderID"];

            if (!string.IsNullOrEmpty(strOrderID))
            {
                order = OrderBLL.Get(strOrderID.ToInt());

                if (order.Type == Order.TypeX.ForOrg)
                {
                    LabelTitle1.Text = "Biên bản giao nhận";

                    LoadOrder();

                    RedBloodDataContext db = new RedBloodDataContext();

                    var v = db.PackOrders.Where(r => r.OrderID.Value == order.ID
                        && !r.ReturnID.HasValue).ToList()
                        .GroupBy(r => r.Pack.ProductCode)
                        .Select(r => new
                        {
                            ProductCode = r.Key,
                            Sum = r.Count(),
                            BloodGroupSumary = r.GroupBy(r1 => r1.Pack.Donation.BloodGroup).Select(r1 => new
                            {
                                BloodGroupDesc = BloodGroupBLL.GetDescription(r1.Key),
                                Total = r1.Count(),
                                VolumeSumary = r1.GroupBy(r2 => r2.Pack.Volume).Select(r2 => new
                                {
                                    Volume = r2.Key.HasValue ? r2.Key.Value.ToString() : "_",
                                    Total = r2.Count(),
                                    DINList = string.Join(",", r2.Select(r3 => r3.Pack.DIN).ToArray()),
                                }).OrderBy(r2 => r2.Volume)
                            }).OrderBy(r1 => r1.BloodGroupDesc),
                        });

                    GridViewSum.DataSource = v;
                    GridViewSum.DataBind();



                    var v2 = db.PackOrders.Where(r => r.OrderID.Value == order.ID
                        && !r.ReturnID.HasValue).ToList().OrderBy(r => r.Pack.DIN);


                    GridViewPack.DataSource = v2;
                    GridViewPack.DataBind();
                }
            }
        }
    }

    public void LoadOrder()
    {
        if (order != null)
        {
            imgOrder.ImageUrl = BarcodeBLL.Url4Order(order.ID);
            txtOrg.Text = order.Org != null ? order.Org.Name : "";

            txtNote.Text = order.Note;

            if (order.Date != null)
                txtDate.Text = order.Date.ToStringVN_Hour();

            txtTransfusionNote.Text = order.TransfusionNote;
        }
    }
}
