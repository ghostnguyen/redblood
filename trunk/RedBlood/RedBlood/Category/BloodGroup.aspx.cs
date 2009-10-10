﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category_BloodGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GridView1.DataSource = BloodGroup.BloodGroupList;
            GridView1.DataBind();
        }
        
        
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {

        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        try
        {
            int count = (GridView1.SelectedRow.FindControl("txtCount") as TextBox).Text.ToInt();
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi",
                        "window.open('" + RedBloodSystem.RootUrl + "/Category/BloodGroupPrint.aspx" + "?count=" + count.ToString() + "&code=" + GridView1.SelectedValue.ToString() + "');", true);
        }
        catch (Exception)
        {

        }


    }
}