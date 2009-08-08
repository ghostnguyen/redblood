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
            UpdateProductCode(BarcodeBLL.ParseDIN
            
        }
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
            Donation temp = DonationBLL.UpdateDefault(DIN, txtCollector.Text.Trim());
            if (temp != null) e = temp;

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

    void UpdateProductCode(string productCode)
    {
 
    }

    protected void txtSave_Click(object sender, EventArgs e)
    {

    }
}
