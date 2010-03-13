﻿using System;
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
    public static void Insert(string geo1Name, string geo2Name, string geo3Name)
    {
        //Geo1
        if (string.IsNullOrEmpty(geo1Name)
            || string.IsNullOrEmpty(geo1Name.Trim())) return;

        Geo geo1 = Get(geo1Name, 1, null);
        Guid? geo1ID = geo1 != null ? geo1.ID : Insert(geo1Name, 1, null);

        //Geo2
        if (string.IsNullOrEmpty(geo2Name)
            || string.IsNullOrEmpty(geo2Name.Trim())) return;

        Geo geo2 = Get(geo2Name, 2, geo1ID);
        Guid? geo2ID = geo2 != null ? geo2.ID : Insert(geo2Name, 2, geo1ID);

        //Geo3
        if (string.IsNullOrEmpty(geo3Name)
            || string.IsNullOrEmpty(geo3Name.Trim())) return;

        Geo geo3 = Get(geo3Name, 3, geo2ID);
        Guid? geo3ID = geo3 != null ? geo3.ID : Insert(geo3Name, 3, geo2ID);
    }

    public static Guid Insert(string name, int level, Guid? parentID)
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
            throw ex;
        }

        return geo.ID;
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

    static public Geo Get(string name, int level, Guid? parentID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        if (
            (level == 1 && !parentID.HasValue)
            ||
            ((level == 2 || level == 3) && parentID.HasValue)
            )
        {
            Geo e = db.Geos.Where(r => r.Level == level
                && r.Name.Trim().ToLower() == name.Trim().ToLower()
                && (
                    (r.ParentID == null && parentID == null)
                    || (parentID != null && r.ParentID == parentID)
                )
                ).FirstOrDefault();

            return e != null ? e : null;
        }

        throw new Exception("Invalid params.");
    }

    static public Geo GetByFullname(string fullname)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from e in db.Geos
                where e.Fullname.ToLower() == fullname.Trim().ToLower()
                select e).FirstOrDefault();
    }


    public static List<Geo> Get(List<Guid> IDList, int level)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return db.Geos.Where(r => IDList.Contains(r.ID) && r.Level == level).ToList();
    }
}
