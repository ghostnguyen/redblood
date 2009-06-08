﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindAndReport_PackDetail : System.Web.UI.Page
{
    public int Autonum
    {
        get
        {
            if (ViewState["Autonum"] == null) return 0;
            return (int)ViewState["Autonum"];
        }
        set
        {
            ViewState["Autonum"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Autonum = Request.Params["key"].ToInt();

        DetailView1.DataBind();
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        Pack p = PackBLL.Get(Autonum);
        if (p == null)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
            e.Result = p;
    }
}
