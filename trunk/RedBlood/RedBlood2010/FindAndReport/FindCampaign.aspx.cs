using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using RedBlood;
using RedBlood.BLL;
public partial class FindAndReport_FindCampaign : System.Web.UI.Page
{
    public DateTime From
    {
        get
        {
            if (Session["From"] == null)
            {
                return new DateTime(1900, 1, 1);
            }
            return (DateTime)Session["From"];
        }
        set
        {
            Session["From"] = value;
        }
    }

    public DateTime To
    {
        get
        {
            if (Session["To"] == null)
            {
                return DateTime.Now.AddYears(1).Date;
            }
            return (DateTime)Session["To"];
        }
        set
        {
            Session["To"] = value;
        }
    }

    public List<Guid> Geo1List
    {
        get
        {
            if (Session["Geo1List"] == null)
            {
                Session["Geo1List"] = new List<Guid>();
            }
            return (List<Guid>)Session["Geo1List"];
        }
        set
        {
            Session["Geo1List"] = value;
        }
    }

    public List<int> SourceList
    {
        get
        {
            if (Session["SourceList"] == null)
            {
                Session["SourceList"] = new List<int>();
            }
            return (List<int>)Session["SourceList"];
        }
        set
        {
            Session["SourceList"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            List<Guid> geo1List = new List<Guid>();
            geo1List.Add(Geo.BinhDuong);
            geo1List.Add(Geo.BRVT);
            geo1List.Add(Geo.DongNai);
            geo1List.Add(Geo.TayNinh);
            geo1List.Add(Geo.HCMC);
            geo1List.Add(Geo.BinhPhuoc);
            CheckBoxListGeo1.DataSource = db.Geos.Where(r => geo1List.Contains(r.ID));
            CheckBoxListGeo1.DataBind();
            foreach (ListItem item in CheckBoxListGeo1.Items)
            {
                item.Selected = true;
            }

            List<int> sourceList = new List<int>();
            sourceList.Add(TestDef.Source.Donation);
            sourceList.Add(TestDef.Source.RedCross);
            sourceList.Add(TestDef.Source.Other);
            CheckBoxListSource.DataSource = db.TestDefs.Where(r => sourceList.Contains(r.ID));
            CheckBoxListSource.DataBind();
            foreach (ListItem item in CheckBoxListSource.Items)
            {
                item.Selected = true;
            }
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        DateTime? from = txtFrom.Text.ToDatetimeFromVNFormat();
        DateTime? to = txtTo.Text.ToDatetimeFromVNFormat();

        List<Guid> geo1List = new List<Guid>();
        foreach (ListItem item in CheckBoxListGeo1.Items)
        {
            if (item.Selected) geo1List.Add(item.Value.ToGuid());
        }

        var v = geo1List.Select(r => new { From = from, To = to, ProvinceID = r });

        ListView1.DataSource = v;

        ListView1.DataBind();
    }

}
