using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;

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

    void LoadFilter(List<People> rs)
    {
        //Geo1
        //BulletedListGeo1.Items.Clear();
        //BulletedListGeo1.DataSource = rs.Where(e => e.ResidentGeoID1 != null).GroupBy(e => e.ResidentGeo1.Name).Select(g => new { ID = g.Key, Name = g.Key + " (" + g.Count().ToString() + ")" });
        //BulletedListGeo1.DataTextField = "Name";
        //BulletedListGeo1.DataValueField = "ID";
        //BulletedListGeo1.DataBind();
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        DateTime? from = txtFrom.Text.ToDatetimeFromVNFormat();
        DateTime? to = txtTo.Text.ToDatetimeFromVNFormat();

        if (from == null || to == null)
        {
            e.Result = null;
            e.Cancel = true;
            return;
        }

        List<Guid> geo1List = new List<Guid>();
        foreach (ListItem item in CheckBoxListGeo1.Items)
        {
            if (item.Selected) geo1List.Add(item.Value.ToGuid());
        }

        List<int> sourceList = new List<int>();
        foreach (ListItem item in CheckBoxListSource.Items)
        {
            if (item.Selected) sourceList.Add(item.Value.ToInt());
        }

        RedBloodDataContext db = new RedBloodDataContext();
        
        DataLoadOptions dlo = new DataLoadOptions();
        dlo.LoadWith<Campaign>(r => r.Donations);
        dlo.LoadWith<Donation>(r => r.Pack);

        db.LoadOptions = dlo;

        List<Campaign> l = 
            db.Campaigns.Where(r => from.Value.Date <= r.Date.Value.Date
            && to.Value.Date >= r.Date.Value.Date
            && geo1List.Contains(r.CoopOrg.GeoID1.Value)
            && sourceList.Contains(r.SourceID.Value)
            ).ToList();

        if (l.Count == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
            e.Result = l;
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        //GridView1.DataBind();

        DateTime? from = txtFrom.Text.ToDatetimeFromVNFormat();
        DateTime? to = txtTo.Text.ToDatetimeFromVNFormat();

        List<Guid> geo1List = new List<Guid>();
        foreach (ListItem item in CheckBoxListGeo1.Items)
        {
            if (item.Selected) geo1List.Add(item.Value.ToGuid());
        }

        var v = geo1List.Select(r => new { From = from, To = to, ProvinceIDList = ToList(r) });

        DataList1.DataSource = v;

        DataList1.DataBind();
    }

    List<Guid> ToList(Guid g)
    {
        List<Guid> l = new List<Guid>();
        l.Add(g);
        return l;
    }
}
