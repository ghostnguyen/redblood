using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Store_CountList : System.Web.UI.Page
{
    public string ProductCode
    {
        get
        {
            if (ViewState["ProductCode"] == null)
            {
                ViewState["ProductCode"] = "";
            }
            return (string)ViewState["ProductCode"];
        }
        set
        {
            ViewState["ProductCode"] = value;
        }
    }

    public int ExpiredInDays
    {
        get
        {
            if (ViewState["ExpiredInDays"] == null)
            {
                ViewState["ExpiredInDays"] = "";
            }
            return ViewState["ExpiredInDays"].ToInt();
        }
        set
        {
            ViewState["ExpiredInDays"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string productCode = Request["ProductCode"];

            if (!string.IsNullOrEmpty(productCode))
            {
                Product r = ProductBLL.Get(productCode);

                ProductCode = r.Code;

                imgProduct.ImageUrl = BarcodeBLL.Url4Product(productCode);
                lblProduct.Text = r.Description;
            }

            ExpiredInDays = Request["ExpiredInDays"].ToInt();
            
            ucInDays.Date = DateTime.Now.Date.AddDays(ExpiredInDays);
            txtDays.Text = ExpiredInDays.ToString();

            GridView1.DataBind();
        }
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.Packs.Where(r => r.Status == Pack.StatusX.Product && r.ProductCode == ProductCode)
            .ToList()
            .Select(r => new
            {
                r.DIN,
                r.Donation.TestResultStatus,
                r.Donation.BloodGroupDesc,
                r.Volume,
                ExpirationDate = r.ExpirationDate.ToStringVN_Hour(),
                Expired = r.ExpirationDate.Value.Expired() ? "X" : "",
                ExpiredInDays = r.ExpirationDate.Value.ExpiredInDays(ExpiredInDays) ? "X" : ""
            })
            .OrderBy(r => r.DIN);
    }

    protected void btnOk1_Click(object sender, EventArgs e)
    {
        ExpiredInDays = txtDays.Text.ToInt();
        ucInDays.Date = DateTime.Now.Date.AddDays(ExpiredInDays);
        GridView1.DataBind();
    }

    protected void btnOk2_Click(object sender, EventArgs e)
    {
        ExpiredInDays = (ucInDays.Date.Value - DateTime.Now.Date).Days;
        txtDays.Text = ExpiredInDays.ToString();
        GridView1.DataBind();
    }
}


