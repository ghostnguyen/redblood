using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class UserControl_PeopleDonationLog : System.Web.UI.UserControl
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

    public void ShowLog()
    {
        GridView1.DataBind();
    }

    public void Clear()
    {
        GridView1.DataBind();
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();


        var l = db.Donations.Where(r => r.PeopleID == PeopleID)
        .OrderByDescending(r => r.CollectedDate)
        .ToList()
        .Select(r => new
        {
            r.DIN,
            CollectedDate = r.CollectedDate.ToStringVN(),
            Note = r.TestResultStatus != Donation.TestResultStatusX.Negative ? r.InfectiousDesc : r.Note,
        });
        e.Result = l;

        if (l != null && l.Count() != 0)
            LabelTotal.Text = "Tổng cộng: " + l.Count().ToString();
        else
            LabelTotal.Text = "";

    }
    protected void LinqDataSource1_Selected(object sender, LinqDataSourceStatusEventArgs e)
    {
        //List<Donation> l = (List<Donation>)e.Result;
        //if (l != null && l.Count != 0)
        //    LabelTotal.Text = "Tổng cộng: " + l.Count.ToString();
        //else
        //    LabelTotal.Text = "";
    }
    public object GetByPeopleID4PackHistory(Guid peopleID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var v = from c in db.Donations
                where c.PeopleID == peopleID
                orderby c.CollectedDate descending
                select c;

        //foreach (Pack e in v)
        //{
        //    if (e.Status == Pack.StatusX.DataErr)
        //    {
        //        e.Note = PackErrList.DataErr.Message;
        //    }
        //    else if (e.Status == Pack.StatusX.Delete)
        //    {
        //        if (e.PackStatusHistories.Count == 0)
        //            e.Note = "Unknown";
        //        else
        //            e.Note = e.Status.ToString() + ": " + e.PackStatusHistories.Where(h => h.ToStatus == Pack.StatusX.Delete).First().Note;
        //    }
        //    else if (e.Status == Pack.StatusX.Collected)
        //    {
        //        e.Note = e.Status.ToString() + ": " + e.TestResultStatus.ToString();
        //    }
        //    else if (e.Status == Pack.StatusX.Expire)
        //    {
        //        e.Note = e.Note = e.Status.ToString() + ": " + e.PackStatusHistories.Where(h => h.ToStatus == Pack.StatusX.Expire).First().Note;
        //    }
        //    else
        //    {
        //        e.Note = e.Status.ToString();
        //    }
        //}

        return v;
    }

}
