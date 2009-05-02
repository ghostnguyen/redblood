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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Master.TextBoxCode.Text.Trim().Length == 0) return;

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
        else if (Keyword.Length >= 3)
        {
            GridView1.DataBind();
        }

        Master.TextBoxCode.Text = "";
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        string search = "%" + Keyword.Replace(" ", "%") + "%";
        RedBloodDataContext db = new RedBloodDataContext();

        var r = (from rs in db.Peoples
                 where SqlMethods.Like(rs.Name, search) || SqlMethods.Like(rs.NameNoDiacritics, search)
                 select rs);

        e.Result = r.ToArray();
    }
}
