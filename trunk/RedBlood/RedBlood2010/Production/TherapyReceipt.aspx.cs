using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_TherapyReceipt : System.Web.UI.Page
{
    string strID = "strID";
    public Guid ReceiptID
    {
        get
        {
            if (ViewState[strID] == null)
                return Guid.Empty;
            return (Guid)ViewState[strID];
        }
        set
        {
            ViewState[strID] = value;
            DisplayToGUI();
        }
    }

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

    public ReceiptBLL receiptBLL
    {
        get
        {
            return new ReceiptBLL()
            {
                ProductCodeInList = ProductCodeInList,
                ProductCodeOutList = ProductCodeOutList,
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
                ProductCodeInList = receiptBLL.AddProductCodeIn(BarcodeBLL.ParseProductCode(code));
                DataListProductIn.DataBind();
            }
        }
        else if (rdbProductCodeOut.Checked)
        {
            if (BarcodeBLL.IsValidProductCode(code))
            {
                ProductCodeOutList = receiptBLL.AddProductCodeOut(BarcodeBLL.ParseProductCode(code));
                DataListProductOut.DataBind();
            }
        }

    }

    protected void LinqDataSourceFind_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = ReceiptBLL.Find(txtNameFind.Text.Trim());
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (btn != null)
        {
            ReceiptID = btn.CommandArgument.ToGuid();
        }
    }

    public void DisplayToGUI()
    {
        Receipt e = ReceiptBLL.Get(ReceiptID);

        if (e == null)
        {
            e = new Receipt();
            ProductCodeInList.Clear();
            ProductCodeOutList.Clear();
            rdbProductCodeIn.Checked = true;
        }
        else
        {
            ProductCodeInList = e.ReceiptProducts.Where(r => r.Type == ReceiptProduct.TypeX.In).Select(r => r.ProductCode).ToList();
            ProductCodeOutList = e.ReceiptProducts.Where(r => r.Type == ReceiptProduct.TypeX.Out).Select(r => r.ProductCode).ToList();
        }

        txtName.Text = e.Name;
        txtNote.Text = e.Note;

        DataListProductIn.DataBind();
        DataListProductOut.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ReceiptID = receiptBLL.InsertOrUpdate(ReceiptID, LoadFromGUI);

        GridView1.DataBind();
        this.Alert("Lưu thành công.");
    }

    private Receipt LoadFromGUI(Receipt p)
    {
        p.Name = txtName.Text.Trim();
        p.Note = txtNote.Text;

        return p;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ReceiptID = Guid.Empty;
        txtName.Focus();
        rdbProductCodeIn.Checked = true;
        rdbProductCodeOut.Checked = false;

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ReceiptID != Guid.Empty)
        {
            ReceiptBLL.Delete(ReceiptID);

            ReceiptID = Guid.Empty;
            GridView1.DataBind();
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

    protected void btnProductCodeIn_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        
        if (btn != null)
        {
            ProductCodeInList.Remove(btn.CommandArgument);
            DataListProductIn.DataBind();
        }
    }

    protected void btnProductCodeOut_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        
        if (btn != null)
        {
            ProductCodeOutList.Remove(btn.CommandArgument);
            DataListProductOut.DataBind();
        }
    }
}
