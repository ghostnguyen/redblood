using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq.SqlClient;
using RedBlood;
using RedBlood.BLL;
public partial class FindAndReport_FindPeople : System.Web.UI.Page
{
    public string Keyword
    {
        get
        {
            if (ViewState["Keyword"] == null) return "";
            return (string)ViewState["Keyword"];
        }
        set
        {
            ViewState["Keyword"] = value;
        }
    }

    public string SexName
    {
        get
        {
            if (ViewState["SexName"] == null) return "";
            return (string)ViewState["SexName"];
        }
        set
        {
            ViewState["SexName"] = value;
        }
    }

    public string DOBYear
    {
        get
        {
            if (ViewState["DOBYear"] == null) return "";
            return (string)ViewState["DOBYear"];
        }
        set
        {
            ViewState["DOBYear"] = value;
        }
    }

    public string Geo1Name
    {
        get
        {
            if (ViewState["Geo1Name"] == null) return "";
            return (string)ViewState["Geo1Name"];
        }
        set
        {
            ViewState["Geo1Name"] = value;
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Keyword = Request.Params["key"];
            GridView1.DataBind();
        }
        else
        {
            Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

            if (Master.TextBoxCode.Text.Length != 0)
            {
                Keyword = Master.TextBoxCode.Text;
                GridView1.DataBind();
                Master.TextBoxCode.Text = "";
            }
        }
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (string.IsNullOrEmpty(Keyword) || Keyword.Length < 2)
        {
            e.Cancel = true;
            return;
        }

        string search = "%" + Keyword + "%";

        if (Keyword.ToLower().Trim() == "all") search = "%";
        RedBloodDataContext db = new RedBloodDataContext();

        var r = (from rs in db.Peoples
                 where SqlMethods.Like(rs.Name, search) || SqlMethods.Like(rs.NameNoDiacritics, search)
                 select rs);

        //LoadFilter(r.ToList());

        List<People> filter = r.ToList().Where(g =>
            (string.IsNullOrEmpty(SexName) || (g.Sex != null && g.Sex.Name == SexName))
            && (string.IsNullOrEmpty(Geo1Name) || g.ResidentGeo1.Name == Geo1Name)
                //&& (string.IsNullOrEmpty(DOBYear) || (g.DOB != null && g.DOB.Value.Decade() == DOBYear.ToInt()
                //    || (g.DOBYear != null && g.DOBYear.ToString() == DOBYear)))
            && (string.IsNullOrEmpty(DOBYear) || g.DOBInDecade == DOBYear.ToInt())
                ).ToList();
        e.Result = filter;
        LoadFilter(filter);
    }


    void LoadFilter(List<People> rs)
    {
        //Sex
        BulletedListSex.Items.Clear();
        BulletedListSex.DataSource = rs.Where(e => e.SexID != null).GroupBy(e => e.Sex.Name).Select(g => new { ID = g.Key, Name = g.Key + " (" + g.Count().ToString() + ")" });
        BulletedListSex.DataTextField = "Name";
        BulletedListSex.DataValueField = "ID";
        BulletedListSex.DataBind();

        //Geo1
        BulletedListGeo1.Items.Clear();
        BulletedListGeo1.DataSource = rs.Where(e => e.ResidentGeoID1 != null).GroupBy(e => e.ResidentGeo1.Name).Select(g => new { ID = g.Key, Name = g.Key + " (" + g.Count().ToString() + ")" });
        BulletedListGeo1.DataTextField = "Name";
        BulletedListGeo1.DataValueField = "ID";
        BulletedListGeo1.DataBind();

        //DOB
        BulletedListDOB.Items.Clear();
        BulletedListDOB.DataSource = rs.Where(e => e.DOBInDecade != 0)
            //.GroupBy(e => (e.DOB.Value.Year - (e.DOB.Value.Year % 10)))
            .ToList()
            //TODO: GroupBy DOBYear Only
            .GroupBy(e => e.DOBInDecade)
            //.Select(g => new { ID = g.Key, Name = g.Key.ToString() + " - " + (g.Key + 9).ToString() + " (" + g.Count().ToString() + ")" })
            .Select(g => new { ID = g.Key, Name = g.Key.ToString() + "s" + " (" + g.Count().ToString() + ")" })
            .OrderBy(j => j.Name);
        BulletedListDOB.DataTextField = "Name";
        BulletedListDOB.DataValueField = "ID";
        BulletedListDOB.DataBind();
    }

    protected void BulletedListSex_Click(object sender, BulletedListEventArgs e)
    {
        SexName = BulletedListSex.Items[e.Index].Value.ToString();
        FilterChange();
    }

    protected void BulletedListDOB_Click(object sender, BulletedListEventArgs e)
    {
        DOBYear = BulletedListDOB.Items[e.Index].Value.ToString();
        FilterChange();
    }

    protected void BulletedListGeo1_Click(object sender, BulletedListEventArgs e)
    {
        Geo1Name = BulletedListGeo1.Items[e.Index].Value.ToString();
        FilterChange();
    }

    protected void BulletedListFilter_Click(object sender, BulletedListEventArgs e)
    {
        if (BulletedListFilter.Items[e.Index].Value == "SexName")
            SexName = "";

        if (BulletedListFilter.Items[e.Index].Value == "DOBYear")
            DOBYear = "";

        if (BulletedListFilter.Items[e.Index].Value == "Geo1Name")
            Geo1Name = "";

        FilterChange();
    }

    void FilterChange()
    {
        BulletedListFilter.Items.Clear();

        if (!string.IsNullOrEmpty(SexName))
        {
            BulletedListFilter.Items.Add(new ListItem("(x) " + SexName, "SexName"));
        }

        if (!string.IsNullOrEmpty(DOBYear))
        {
            BulletedListFilter.Items.Add(new ListItem("(x) " + DOBYear, "DOBYear"));
        }

        if (!string.IsNullOrEmpty(Geo1Name))
        {
            BulletedListFilter.Items.Add(new ListItem("(x) " + Geo1Name, "Geo1Name"));
        }

        GridView1.DataBind();
    }
}
