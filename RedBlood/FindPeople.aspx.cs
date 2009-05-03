using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq.SqlClient;

public partial class FindPeople : System.Web.UI.Page
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
        if (Master.TextBoxCode.Text.Trim().Length == 0
            || Keyword == Master.TextBoxCode.Text.Trim()) return;

        Keyword = Master.TextBoxCode.Text.Trim();

        string pattern = @"\d+";
        Regex regx = new Regex(pattern);

        if (CodabarBLL.IsValidPeopleCode(Keyword))
        {
            //People r = PeopleBLL.GetByCode(code);

            //if (r != null)
            //{
            //}
        }
        else if (regx.IsMatch(Keyword) && Keyword.Length >= 9)
        {
            //People r = bll.GetByCMND(Code);
            //if (r != null)
            //{
            //    PeopleID = r.ID;
            //}
            //else
            //{
            //    New(Code);
            //}
        }
        else if (Keyword.Length > 1)
        {
            GridView1.DataBind();
        }

        //Master.TextBoxCode.Text = "";
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (string.IsNullOrEmpty(Keyword) || Keyword.Length < 2)
        {
            e.Cancel = true;
            return;
        }

        string search = "%" + Keyword.Replace(" ", "%") + "%";

        if (Keyword.ToLower().Trim() == "all") search = "%";
        RedBloodDataContext db = new RedBloodDataContext();

        var r = (from rs in db.Peoples
                 where SqlMethods.Like(rs.Name, search) || SqlMethods.Like(rs.NameNoDiacritics, search)
                 select rs);

        LoadFilter(r.ToList());


        e.Result = r.ToList().Where(g => (g.Sex.Name == SexName || string.IsNullOrEmpty(SexName)));
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
        BulletedListDOB.DataSource = rs.Where(e => e.DOB != null)
            .GroupBy(e => (e.DOB.Value.Year - (e.DOB.Value.Year % 10)))
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

    void FilterChange()
    {
        BulletedListFilter.Items.Clear();

        GridView1.DataBind();
    }
}
