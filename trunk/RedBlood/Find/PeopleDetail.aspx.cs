using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Find_PeopleDetail : System.Web.UI.Page
{
    public Guid ID
    {
        get
        {
            if (ViewState["peopleID"] == null) return Guid.Empty;
            return (Guid)ViewState["peopleID"];
        }
        set
        {
            ViewState["peopleID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.Params["key"].ToGuid();

        DetailsView1.DataBind();
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        People p = PeopleBLL.GetByID(ID);

        if (p == null)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {
            e.Result = p;
        }
    }
}
