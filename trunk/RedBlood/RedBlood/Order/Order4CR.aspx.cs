﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class Order_Order4CR : System.Web.UI.Page
{
    public int OrderID
    {
        get
        {
            if (ViewState["OrderID"] == null)
                return 0;
            return (int)ViewState["OrderID"];
        }
        set
        {
            ViewState["OrderID"] = value;
            LoadOrder();
        }
    }

    public string CurrentDIN
    {
        get
        {
            if (ViewState["CurrentDIN"] == null)
                return "";
            return (string)ViewState["CurrentDIN"];
        }
        set
        {
            ViewState["CurrentDIN"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int orgID = Request.Params["key"].ToInt();

            if (orgID != 0)
            {
                OrderID = orgID;
            }
        }
        else
        {
            string code = Master.TextBoxCode.Text.Trim();
            Master.TextBoxCode.Text = "";

            if (BarcodeBLL.IsValidDINCode(code))
            {
                LoadCurrentDIN(BarcodeBLL.ParseDIN(code));
            }
            else if (BarcodeBLL.IsValidOrderCode(code))
            {
                OrderID = BarcodeBLL.ParseOrderID(code);
            }
            else if (BarcodeBLL.IsValidProductCode(code))
            {
                AddPack(BarcodeBLL.ParseProductCode(code));
            }
            else if (BarcodeBLL.IsValidPeopleCode(code))
            {
                People1.Code = code;
            }
            else if (code.Length >= 9)
            {
                People1.Code = code;
            }
        }
    }

    void AddPack(string productCode)
    {
        PackOrderBLL.Add(OrderID, CurrentDIN, productCode);

        GridViewPack.DataBind();

        CurrentDIN = "";
        imgCurrentDIN.ImageUrl = "none";
    }

    void LoadCurrentDIN(string DIN)
    {
        Donation e = DonationBLL.Get4Order(DIN);

        CurrentDIN = e.DIN;
        imgCurrentDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN);
    }

    protected void btnNew4CR_Click(object sender, EventArgs e)
    {
        OrderID = 0;

        //Clear();
        //btnUpdate.Enabled = true;
    }

    //public void Clear()
    //{
    //    OrderID = 0;

    //    imgOrder.ImageUrl = "none";

    //    txtDate.Text = "";
    //    txtNote.Text = "";
    //    txtDept.Text = "";
    //    txtRoom.Text = "";
    //    txtBed.Text = "";
    //    txtDiagnosis.Text = "";
    //    txtPatientCode.Text = "";
    //    txtTransfusionNote.Text = "";
    //    People1.PeopleID = Guid.Empty;

    //    GridViewPack.DataBind();

    //    CurrentDIN = "";
    //    imgCurrentDIN.ImageUrl = "none";
    //}

    public void LoadOrder()
    {
        Order e = new Order();
        if (OrderID == 0)
        {
            imgOrder.ImageUrl = "none";
            People1.PeopleID = Guid.Empty;
        }
        else
        {
            e = OrderBLL.Get(OrderID);

            if (e.Type == Order.TypeX.ForOrg)
            {
                Response.Redirect(RedBloodSystem.Url4Order4Org + "key=" + e.ID.ToString());
            }
            else if (e.Type == Order.TypeX.ForCR)
            {
                imgOrder.ImageUrl = BarcodeBLL.Url4Order(e.ID);
                People1.PeopleID = e.People != null ? e.PeopleID.GetValueOrDefault() : Guid.Empty;
            }
        }

        txtNote.Text = e.Note;

        if (e.Date != null)
            txtDate.Text = e.Date.ToStringVN_Hour();
        else txtDate.Text = "";

        txtDept.Text = e.FullDepartment;
        txtRoom.Text = e.Room;
        txtBed.Text = e.Bed;
        txtDiagnosis.Text = e.Diagnosis;
        txtPatientCode.Text = e.PatientCode;
        txtTransfusionNote.Text = e.TransfusionNote;

        GridViewPack.DataBind();

        btnUpdate.Enabled = e.Status == Order.StatusX.Init;

        CurrentDIN = "";
        imgCurrentDIN.ImageUrl = "none";
    }

    protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.PackOrders.Where(r => r.OrderID.Value == OrderID
            && r.Status != PackOrder.StatusX.Return);
    }


    protected void GridViewPack_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ReturnBLL.Add(new List<int>() { e.Keys[0].ToInt() }, txtRemoveNoteGlobal.Text.Trim());
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (OrderID == 0)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            Order p = new Order();
            p.Status = Order.StatusX.Init;

            if (LoadFromGUI(p))
            {
                db.Orders.InsertOnSubmit(p);
                db.SubmitChanges();
                OrderID = p.ID;
            }
            else
                return;
        }
        else
        {
            RedBloodDataContext db = new RedBloodDataContext();

            Order p = OrderBLL.Get(OrderID, db);

            if (p == null) return;


            if (LoadFromGUI(p))
            {
                db.SubmitChanges();
                OrderID = p.ID;
            }
            else return;
        }
        this.Alert("Lưu thành công.");

    }


    private bool LoadFromGUI(Order p)
    {
        bool isDone = true;

        if (p.Date == null) p.Date = DateTime.Now;

        p.Type = Order.TypeX.ForCR;
        p.Note = txtNote.Text.Trim();

        p.PeopleID = People1.PeopleID;

        p.SetDepartment(txtDept.Text.Trim());

        p.Room = txtRoom.Text.Trim();
        p.Bed = txtBed.Text.Trim();
        p.Diagnosis = txtDiagnosis.Text.Trim();
        p.PatientCode = txtPatientCode.Text.Trim();
        p.TransfusionNote = txtTransfusionNote.Text.Trim();

        return isDone;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //if (CampaignID == 0)
        //{
        //    return;
        //}
        //else
        //{
        //    try
        //    {
        //        string m = bll.Delete(CampaignID);
        //        Clear();

        //        ScriptManager.RegisterStartupScript(btnDelete, btnDelete.GetType(), "", "alert ('" + m + "');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(btnDelete, btnDelete.GetType(), "", "alert ('" + ex.Message + "');", true);
        //    }
        //}
    }
}
