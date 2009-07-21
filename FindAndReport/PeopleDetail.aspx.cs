using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindAndReport_PeopleDetail : System.Web.UI.Page
{
    public Guid PeopleID
    {
        get
        {
            if (ViewState["PeopleID"] == null) return Guid.Empty;
            return (Guid)ViewState["PeopleID"];
        }
        set
        {
            ViewState["PeopleID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        PeopleID = Request.Params["key"].ToGuid();

        if (PeopleID != Guid.Empty)
        {
            People1.PeopleID = PeopleID;
            PeopleHistory1.PeopleID = PeopleID;
        }

    }
}
