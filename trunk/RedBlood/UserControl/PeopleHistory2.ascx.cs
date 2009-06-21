using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            //Clear();
            //ViewState["PeopleID"] = value;
            //if (value == null)
            //{ }
            //else
            //{
            //    LoadPeople();
            //}
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
        LabelTotal.Text = "TC: " + (e.Result as List<Pack>).Count.ToString();
    }
    public object GetByPeopleID4PackHistory(Guid peopleID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var v = from c in db.Packs
                where c.PeopleID == peopleID
                orderby c.Status descending, c.CollectedDate descending
                select c;

        foreach (Pack e in v)
        {
            if (e.Status == Pack.StatusX.Delete)
            {
                if (e.PackStatusHistories.Count == 0)
                    e.Note = "Unknown";
                else
                    e.Note = e.Status.ToString() + ": " + e.PackStatusHistories.Where(h => h.ToStatus == Pack.StatusX.Delete).First().Note;
            }
            else if (e.Status == Pack.StatusX.Collected )
            {
                e.Note = e.Status.ToString();
            }

            else if (e.Status == Pack.StatusX.Collected || e.Status == Pack.StatusX.Expire)
            {
                e.Note = e.Note = e.Status.ToString() + ": " + e.PackStatusHistories.Where(h => h.ToStatus == Pack.StatusX.Expire).First().Note;
            }

            else if (e.TestResultStatus != Pack.TestResultStatusX.Non)
            {
                //List<TestDef> def = PackBLL.ValidateTestResult(e.TestResults.Where(r => r.Times == 2).First());
                List<TestDef> def = TestResultBLL.GetNonNegative(e.TestResults.Where(r => r.Times == 2).First());

                if (def.Count() == 0)
                {
                    e.Note = "Negative";
                }
                else
                {
                    e.Note = "Positive: ";
                    foreach (TestDef t in def)
                    {
                        e.Note += t.Parent.Name + ", ";
                    }
                }
            }
            else
            {
                e.Note = e.Status.ToString();
            }


        }

        return v;
    }

}
