using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_CollectPack : System.Web.UI.Page
{
    public string DIN
    {
        get
        {
            if (ViewState["DIN"] == null)
            {
                ViewState["DIN"] = "";
            }
            return (string)ViewState["DIN"];
        }
        set
        {
            ViewState["DIN"] = value;
            LoadDIN();
        }
    }

    public List<string> DINList
    {
        get
        {
            if (ViewState["DINList"] == null)
            {
                ViewState["DINList"] = new List<string>();
            }
            return (List<string>)ViewState["DINList"];
        }
        set
        {
            ViewState["DINList"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (BarcodeBLL.IsValidDINCode(code))
        {
            DIN = BarcodeBLL.ParseDIN(code);

            //if (!DINList.Contains(BarcodeBLL.ParseDIN(code)))
            //    DINList.Add(BarcodeBLL.ParseDIN(code));
            //GridView1.DataBind();
            //GridView1.EditIndex = 0;
        }
        else if (BarcodeBLL.IsValidProductCode(code))
        {

        }
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = Get(db);
    }

    private List<Donation> Get(RedBloodDataContext db)
    {
        return db.Donations.Where(r => DINList.Contains(r.DIN)).ToList();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string str = txtCollector.Text.Trim();

        if (string.IsNullOrEmpty(str))
            return;

        RedBloodDataContext db = new RedBloodDataContext();

        List<Donation> l = Get(db);

        foreach (Donation item in l)
        {
            item.Collector = str;
        }
        db.SubmitChanges();

        GridView1.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        DINList.Clear();
        GridView1.DataBind();
    }

    public void LoadDIN()
    {
        Donation e = DonationBLL.Get(DIN);


        if (e == null)
        {
            Clear();
        }
        else
        {
            lblName.Text = e.People.Name;

            imgDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN, "00");
            lblDate.Text = e.CollectedDate.ToStringVN_Hour();

            if (e.Pack != null)
                imgProduct.ImageUrl = BarcodeBLL.Url4Product(e.Pack.Product.Code);
            else
                imgProduct.ImageUrl = "none";

            txtVolume.Text = e.Volume.ToString();
            txtCollector.Text = e.Collector;
            txtNote.Text = e.Note;
        }

    }

    private void Clear()
    {
        lblName.Text = "";
        imgDIN.ImageUrl = "none";
        lblDate.Text = "";
        imgProduct.ImageUrl = "none";
        txtVolume.Text = "";
        txtCollector.Text = "";
        txtNote.Text = "";
    }
    protected void txtSave_Click(object sender, EventArgs e)
    {

    }
}
