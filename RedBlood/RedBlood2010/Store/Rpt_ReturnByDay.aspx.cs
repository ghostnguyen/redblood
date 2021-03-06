﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Store_Rpt_ReturnByDay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDateFrom.Text = DateTime.Now.Date.ToStringVN();
            txtHourFrom.Text = "00:01";

            txtDateTo.Text = DateTime.Now.Date.ToStringVN();
            txtHourTo.Text = "23:59";
            LoadData();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        LoadData();
    }

    void LoadData()
    {
        DateTime? dtFrom = txtDateFrom.Text.ToDatetimeFromVNFormat();
        DateTime? dtTo = txtDateTo.Text.ToDatetimeFromVNFormat();

        if (dtFrom.HasValue)
        {
            DateTime hourFrom;
            if (DateTime.TryParse(txtHourFrom.Text, out hourFrom))
            {
                dtFrom = dtFrom.Value.AddHours(hourFrom.Hour).AddMinutes(hourFrom.Minute);
            }
        }

        if (dtTo.HasValue)
        {
            DateTime hourTo;
            if (DateTime.TryParse(txtHourTo.Text, out hourTo))
            {
                dtTo = dtTo.Value.AddHours(hourTo.Hour).AddMinutes(hourTo.Minute);
            }
        }

        RedBloodDataContext db = new RedBloodDataContext();

        //var packs = db.Packs.Where(r => r.Date.Value >= dtFrom && r.Date.Value <= dtTo
        //   && r.Donation.OrgPackID != r.ID).OrderBy(r => r.Date);

        var v = db.Returns.Where(r => r.Date.Value >= dtFrom && r.Date.Value <= dtTo)
            .OrderBy(r => r.Date)
            .ToList()
            .Select(r => new
            {
                r.ID,
                Date = r.Date.ToStringVN_Hour(),
                r.Note,
                r.Actor,
                Packs = r.PackOrders.Select(r1 => r1.Pack)
            });



        GridView1.DataSource = v;
        GridView1.DataBind();

        GridViewSummary.DataSource = v.SelectMany(r => r.Packs).GroupBy(r => r.ProductCode).Select(r => new { ProductCode = r.Key, Count = r.Count() });
        GridViewSummary.DataBind();
    }

}
