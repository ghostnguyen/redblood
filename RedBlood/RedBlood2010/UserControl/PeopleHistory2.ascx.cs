﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class UserControl_PeopleHistory2 : System.Web.UI.UserControl
{
    public Guid PeopleID
    {
        get
        {
            if (ViewState["PeopleID"] == null)
                return Guid.Empty;
            return (Guid)ViewState["PeopleID"];
        }
        set
        {
            ViewState["PeopleID"] = value;
            GridView1.DataBind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {


    }

    public void LoadPeople()
    {
        GridView1.DataBind();
    }

    public void Clear()
    {
        GridView1.DataBind();
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = GetByPeopleID4PackHistory(PeopleID);
    }
    protected void LinqDataSource1_Selected(object sender, LinqDataSourceStatusEventArgs e)
    {
        //LabelTotal.Text = "TC: " + (e.Result as List<Pack>).Count.ToString();
    }
    public object GetByPeopleID4PackHistory(Guid peopleID)
    {
        RedBloodDataContext db = new RedBloodDataContext();



        var v = db.Donations.Where(r => r.PeopleID == peopleID)
                    .OrderByDescending(r => r.CollectedDate)
                    .ToList().Select(c => new
                    {
                        c.DIN,
                        c.Status,
                        c.CollectedDate,
                        Note = (c.TestResultStatus != Donation.TestResultStatusX.Negative ? c.Markers.Description : "") + " | " + c.Note,
                        c.BloodGroupDesc,
                        ProductDesc = ProductBLL.GetDesc(c.Pack.ProductCode),
                        c.Pack.Volume
                    });

        //foreach (var e in v)
        //{
        //if (e.Status == Donation.StatusX.DataErr)
        //{
        //    e.Note = PackErrEnum.DataErr.Message;
        //}
        //else if (e.Status == Donation.StatusX.Delete)
        //{
        //    if (e.PackStatusHistories.Count == 0)
        //        e.Note = "Unknown";
        //    else
        //        e.Note = e.Status.ToString() + ": " + e.PackStatusHistories.Where(h => h.ToStatus == Pack.StatusX.Delete).First().Note;
        //}
        //else if (e.Status == Donation.StatusX.Assigned)
        //{
        //    e.Note = e.Status.ToString() + ": " + e.TestResultStatus.ToString();
        //}
        //else if (e.Status == Pack.StatusX.Expired)
        //{
        //    e.Note = e.Note = e.Status.ToString() + ": " + e.PackStatusHistories.Where(h => h.ToStatus == Pack.StatusX.Expired).First().Note;
        //}
        //else
        //{
        //    e.Note = e.Status.ToString();
        //}
        //}

        return v;

    }

}
