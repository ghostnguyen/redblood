﻿using System;
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

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (BarcodeBLL.IsValidDINCode(code))
        {
            EnterDIN(BarcodeBLL.ParseDIN(code));
        }
        else if (BarcodeBLL.IsValidProductCode(code))
        {
            EnterProductCode(BarcodeBLL.ParseProductCode(code));
        }
        else if (BarcodeBLL.IsValidBloodGroupCode(code))
        {
            //TODO: ProductCode must enter before BloodGroup
            EnterBloodGroup(BarcodeBLL.ParseBloodGroupCode(code));
        }
    }

    void EnterDIN(string DINCode)
    {
        DIN = DINCode;

        DonationBLL.UpdateCollector(DIN, txtDefaultCollector.Text.Trim());
        LoadDIN();
    }

    void EnterProductCode(string productCode)
    {
        PackBLL.Add(DIN, productCode, txtDefaultVolume.Text.ToInt(), true);
        LoadDIN();
    }

    void EnterBloodGroup(string bloodGroupCode)
    {
        DonationBLL.Update(DIN, bloodGroupCode, "");
        LoadDIN();
    }

    public void LoadDIN()
    {
        Donation e = DonationBLL.Get(DIN);

        Clear(); 
        if (e != null)
        {
            lblName.Text = e.People.Name;

            imgDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN, "00");

            lblDINDate.Text = e.CollectedDate.ToStringVN();


            if (e.Pack != null)
            {
                imgProduct.ImageUrl = BarcodeBLL.Url4Product(e.Pack.Product.Code);
                lblProductDesc.Text = e.Pack.Product.Description;

                lblDate.Text = e.Pack.Date.ToStringVN_Hour();

                txtVolume.Text = e.Pack.Volume.ToString();

                if (!string.IsNullOrEmpty(e.BloodGroup))
                {
                    ImageBloodGroup.ImageUrl = BarcodeBLL.Url4BloodGroup(e.BloodGroup);
                    lblBloodGroup.Text = BloodGroupBLL.GetDescription(e.BloodGroup);
                }
            }

            txtCollector.Text = e.Collector;

            txtNote.Text = e.Note;

            //btnSave.Enabled = DonationBLL.CanUpdateTestResult(e);
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
        ImageBloodGroup.ImageUrl = "none";
        lblBloodGroup.Text = "";
        txtCollector.Text = "";
        txtNote.Text = "";
        //btnSave.Enabled = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Donation d = DonationBLL.Get(db, DIN);

        if (d == null)
        {
            Clear();
        }
        else
        {
            //TODO: Check to see too late to update
            // Code check will be here

            d.Collector = txtCollector.Text.Trim();

            if (d.Pack != null)
            {
                d.Pack.Volume = txtVolume.Text.ToInt();
                d.Pack.Note = txtNote.Text.Trim();
            }

            d.Note = txtNote.Text.Trim();

            db.SubmitChanges();

            this.Alert("Lưu thành công.");
        }
    }
}
