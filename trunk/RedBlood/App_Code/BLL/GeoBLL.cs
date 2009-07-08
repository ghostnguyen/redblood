using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for GeoBLL
/// </summary>
public class GeoBLL
{
    public GeoBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Insert(string name, int level, Guid? parentID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Geo geo = new Geo();
        geo.Name = name.Trim();
        geo.Level = level;
        geo.ParentID = parentID;

        db.Geos.InsertOnSubmit(geo);

        try
        {
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return geo.ID.ToString();
    }

    public void UpdateFullname()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var r = from e in db.Geos
                select e;

        foreach (Geo e in r)
        {
            SetFullname(e);
        }

        db.SubmitChanges();
    }

    public void SetFullname(Geo e)
    {
        if (e.Level == 1)
        {
            e.Fullname = e.Name;
        }

        if (e.Level == 2)
        {
            e.Fullname = e.Name + ", " + e.ParentGeo.Name;
        }

        if (e.Level == 3)
        {
            e.Fullname = e.Name + ", " + e.ParentGeo.Name + ", " + e.ParentGeo.ParentGeo.Name;
        }

        e.FullnameNoDiacritics = e.Fullname.RemoveDiacritics();
    }

    static public Geo GetByFullnameAndLevel(string fullname, int lvl)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var r = from e in db.Geos
                where e.Level.Value == lvl && e.Fullname == fullname.Trim()
                select e;
        if (r.Count() == 0)
            return null;
        else
            return r.First();
    }

    static public Geo GetByFullname(string fullname)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from e in db.Geos
                where e.Fullname.ToLower() == fullname.Trim().ToLower()
                select e).FirstOrDefault();
    }

    static public Geo GetByName(string name, int level)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from e in db.Geos
                where e.Name.ToLower() == name.Trim().ToLower() && e.Level == level
                select e).FirstOrDefault();
    }

    public static List<Geo> Get(List<Guid> IDList, int level)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return db.Geos.Where(r => IDList.Contains(r.ID) && r.Level == level).ToList();
    }
}
