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

    protected void Page_Load(object sender, EventArgs e)
    {
        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (BarcodeBLL.IsValidDINCode(code))
        {
            DIN = BarcodeBLL.ParseDIN(code);
        }
        else if (BarcodeBLL.IsValidProductCode(code))
        {
            EnterProductCode(BarcodeBLL.ParseProductCode(code));
        }
    }



    public void LoadDIN()
    {
        Donation e = DonationBLL.Get(DIN);

        Clear();
        if (e != null)
        {
            Donation temp = DonationBLL.UpdateDefault(DIN, txtDefaultCollector.Text.Trim());
            if (temp != null) e = temp;

            lblName.Text = e.People.Name;

            imgDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN, "00");

            lblDINDate.Text = e.CollectedDate.ToStringVN();


            if (e.Pack != null)
            {
                imgProduct.ImageUrl = BarcodeBLL.Url4Product(e.Pack.Product.Code);
                lblProductDesc.Text = e.Pack.Product.Description;

                lblDate.Text = e.Pack.Date.ToStringVN_Hour();
            }

            txtVolume.Text = e.Volume.ToString();
            txtCollector.Text = e.Collector;
            txtNote.Text = e.Note;
        }
    }

    private void Clear()
    {
        lblName.Text = "";
        imgDIN.ImageUrl = "none";
        lblDINDate.Text = "";
        lblDate.Text = "";
        imgProduct.ImageUrl = "none";
        lblProductDesc.Text = "";
        txtVolume.Text = "";
        txtCollector.Text = "";
        txtNote.Text = "";

    }

    void EnterProductCode(string productCode)
    {
        PackErr err = PackBLL.CreateOriginal(DIN, productCode, txtDefaultVolume.Text.ToInt());
        if (err != PackErrEnum.Non)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi",
                    "alert ('" + err.Message + "');", true);
        }
        else
        {
            LoadDIN();
        }
    }

    protected void txtSave_Click(object sender, EventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Donation d = DonationBLL.Get(db, DIN);

        if (d == null)
        {
            //Clear();
        }
        else
        {
            // Check to see too late to update
            // Code check will be here

            d.Collector = txtCollector.Text.Trim();

            if (d.Pack != null)
            {
                d.Volume = txtVolume.Text.ToInt();
                d.Pack.Volume = txtVolume.Text.ToInt();

                d.Note = txtNote.Text.Trim();
            }

            db.SubmitChanges();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi",
                    "alert ('Lưu thành công.');", true);
        }

    }
}
