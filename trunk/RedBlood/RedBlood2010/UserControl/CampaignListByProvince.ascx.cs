using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class UserControl_CampaignListByProvince : System.Web.UI.UserControl
{
    public Guid ProvinceID
    {
        get
        {
            if (ViewState["ProvinceID"] == null)
                return new Guid();
            else
                return (Guid)ViewState["ProvinceID"];
        }
        set
        {
            ViewState["ProvinceID"] = value;
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
        e.Result = CampaignBLL.Get(new List<Guid>() { ProvinceID }, From, To, Campaign.TypeX.Short_run);
    }
}
