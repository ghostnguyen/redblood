﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UserControl_SupplierEditAccount : System.Web.UI.UserControl
{
    public event EventHandler AccountChanged;

    SupplierBankAccountBLL bll = new SupplierBankAccountBLL();
    SupplierBLL supplierBLL = new SupplierBLL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetSupplierID(string ID)
    {
        txtSupplierID.Text = ID;
        GridViewAccount.DataBind();
    }

    protected void GridViewBank_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "AddAccount":

                if (string.IsNullOrEmpty(txtSupplierID.Text) || new Guid(txtSupplierID.Text) == Guid.Empty)
                {
                    return;
                }

                //Guid bankID = (Guid)GridViewBank.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                Guid bankID = new Guid(e.CommandArgument.ToString());

                bll.Insert(new Guid(txtSupplierID.Text), bankID);

                GridViewAccount.DataBind();
                break;
            case "Select":
                divBankBrandList.Visible = true;

                break;
        }
    }

    protected void GridViewBankBrand_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "AddAccount":

                if (string.IsNullOrEmpty(txtSupplierID.Text) || new Guid(txtSupplierID.Text) == Guid.Empty)
                {
                    return;
                }

                Guid bankID = new Guid(e.CommandArgument.ToString());

                bll.Insert(new Guid(txtSupplierID.Text), bankID);

                GridViewAccount.DataBind();

                break;
        }
    }

    protected void DetailsViewBankBrand_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        if (GridViewBank.SelectedValue == null)
        {
            e.Cancel = true;
            return;
        }

        e.Values["ParentID"] = new Guid(GridViewBank.SelectedValue.ToString());
        e.Values["Level"] = 2;
    }
    protected void DetailsViewBankBrand_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {


        if (e.Exception != null)
        {
            lblMess.Text = e.Exception.Message;
            e.KeepInInsertMode = true;
            e.ExceptionHandled = true;
        }
        else
        {
            lblMess.Text = "";
            GridViewBankBrand.DataBind();
        }
    }

    protected void GridViewAccount_DataBinding(object sender, EventArgs e)
    {
        if (AccountChanged != null)
        {
            AccountChanged(sender, e);
        }
    }
    protected void GridViewAccount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Select":

                if (string.IsNullOrEmpty(txtSupplierID.Text) || new Guid(txtSupplierID.Text) == Guid.Empty)
                {
                    return;
                }

                Guid accID = new Guid(e.CommandArgument.ToString());

                supplierBLL.Set_DefaultAccountID(new Guid(txtSupplierID.Text), accID);

                GridViewAccount.DataBind();

                break;
        }
    }
    protected void GridViewAccount_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            ActionStatus.Text = e.Exception.Message;
            e.KeepInEditMode = true;
            e.ExceptionHandled = true;
        }
        else
        {
            ActionStatus.Text = "";
        }
    }

}
