using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Production_Extract : System.Web.UI.Page
{
    public List<string> ProductCodeInList
    {
        get
        {
            if (ViewState["ProductCodeInList"] == null)
            {
                ViewState["ProductCodeInList"] = new List<string>();
            }
            return (List<string>)ViewState["ProductCodeInList"];
        }
        set
        {
            ViewState["ProductCodeInList"] = value;
        }
    }

    public List<string> ProductCodeOutList
    {
        get
        {
            if (ViewState["ProductCodeOutList"] == null)
            {
                ViewState["ProductCodeOutList"] = new List<string>();
            }
            return (List<string>)ViewState["ProductCodeOutList"];
        }
        set
        {
            ViewState["ProductCodeOutList"] = value;
        }
    }

    public List<string> DINInList
    {
        get
        {
            if (ViewState["DINInList"] == null)
            {
                ViewState["DINInList"] = new List<string>();
            }
            return (List<string>)ViewState["DINInList"];
        }
        set
        {
            ViewState["DINInList"] = value;
        }
    }

    public ProductionBLL productionBLL
    {
        get
        {
            return new ProductionBLL()
            {
                ProductCodeInList = ProductCodeInList,
                ProductCodeOutList = ProductCodeOutList,
                DINInList = DINInList
            };
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;


        if (rdbProductCodeIn.Checked)
        {
            if (BarcodeBLL.IsValidProductCode(code))
            {
                ProductCodeInList = productionBLL.AddProductCodeIn(BarcodeBLL.ParseProductCode(code));
                DataListProductIn.DataBind();
            }
        }
        else if (rdbProductCodeOut.Checked)
        {
            if (BarcodeBLL.IsValidProductCode(code))
            {
                ProductCodeOutList = productionBLL.AddProductCodeOut(BarcodeBLL.ParseProductCode(code));
                DataListProductOut.DataBind();
            }
        }
        else if (rdbDINIn.Checked)
        {
            if (BarcodeBLL.IsValidDINCode(code))
            {
                DINInList = productionBLL.AddDIN(BarcodeBLL.ParseDIN(code));
                DataListDINIn.DataBind();
            }
        }
    }

    protected void LinqDataSourceProductIn_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = db.Products.Where(r => ProductCodeInList.Contains(r.Code));
    }

    protected void LinqDataSourceProductOut_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = db.Products.Where(r => ProductCodeOutList.Contains(r.Code));
    }

    protected void LinqDataSourceDINIn_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = db.Donations.Where(r => DINInList.Contains(r.DIN));
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ProductCodeInList.Clear();
        DataListProductIn.DataBind();

        ProductCodeOutList.Clear();
        DataListProductOut.DataBind();

        DINInList.Clear();
        DataListDINIn.DataBind();

        rdbProductCodeOut.Checked = false;
        rdbDINIn.Checked = false;
        rdbProductCodeIn.Checked = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        productionBLL.Extract();
        this.Alert("Sản xuất thành công.");
    }
    protected void btnDINRemove_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;

        if (btn != null)
        {
            DINInList.Remove(btn.CommandArgument);
            DataListDINIn.DataBind();
        }
    }
}
