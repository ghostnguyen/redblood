﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_CampaignListByProvince : System.Web.UI.UserControl
{
    public List<Guid> ProvinceIDList
    {
        get
        {
            if (ViewState["ProvinceIDList"] == null)
                return new List<Guid>();
            else
                return (List<Guid>)ViewState["ProvinceIDList"];
        }
        set
        {
            ViewState["ProvinceIDList"] = value;
        }
    }

    public DateTime? From
    {
        get
        {
            return (DateTime?)ViewState["From"];
        }
        set
        {
            ViewState["From"] = value;
        }
    }

    public DateTime? To
    {
        get
        {
            return (DateTime?)ViewState["To"];
        }
        set
        {
            ViewState["To"] = value;
        }
    }

    //public List<int> SourceList
    //{
    //    get
    //    {
    //        if (Session["SourceList"] == null)
    //        {
    //            Session["SourceList"] = new List<int>();
    //        }
    //        return (List<int>)Session["SourceList"];
    //    }
    //    set
    //    {
    //        Session["SourceList"] = value;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = CampaignBLL.Get(ProvinceIDList, From, To, Campaign.TypeX.Short_run);
    }
}
