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
                    LabelTitle1.Text = "BIÊN BẢN BÀN GIAO";

                    LoadOrder();

                    RedBloodDataContext db = new RedBloodDataContext();


                    var v1 = db.PackOrders.Where(r => r.OrderID.Value == order.ID
                        && !r.ReturnID.HasValue).ToList();

                    var v = v1.GroupBy(r => r.Pack.ProductCode)
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
                                    //DINList = string.Join(",", r2.Select(r3 => r3.Pack.DIN).ToArray()),
                                    DINList = r2.Select(r3 => new { DIN = r3.Pack.DIN }).OrderBy(r4 => r4.DIN),
                                }).OrderBy(r2 => r2.Volume)
                            }).OrderBy(r1 => r1.BloodGroupDesc),
                        });

                    GridViewSum.DataSource = v;
                    GridViewSum.DataBind();

                    LableCount.Text = v1.Count().ToStringRemoveZero();

                    //var v2 = db.PackOrders.Where(r => r.OrderID.Value == order.ID
                    //    && !r.ReturnID.HasValue).ToList().OrderBy(r => r.Pack.DIN);
                    //GridViewPack.DataSource = v2;
                    //GridViewPack.DataBind();
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
            lblOrgFooter.Text = txtOrg.Text;
            lblActor.Text = order.Actor;
            txtNote.Text = order.Note;

            if (order.Date != null)
                txtDate.Text = order.Date.ToStringVN_Hour();

            txtTransfusionNote.Text = order.TransfusionNote;
        }
    }
}
