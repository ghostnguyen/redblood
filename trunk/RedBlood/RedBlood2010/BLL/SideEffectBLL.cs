using System;
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
using RedBlood;
/// <summary>
/// Summary description for SideEffectBLL
/// </summary>
public class SideEffectBLL
{
    public SideEffectBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string Insert(string name, int level, Guid? parentID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        SideEffect geo = new SideEffect();
        geo.Name = name.Trim();
        geo.Level = level;
        geo.ParentID = parentID;

        db.SideEffects.InsertOnSubmit(geo);

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

    public static void UpdateFullname()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var r = from e in db.SideEffects
                select e;

        foreach (SideEffect e in r)
        {
            SetFullname(e);
        }

        db.SubmitChanges();
    }

    public static void SetFullname(SideEffect e)
    {
        if (e.Level == 1)
        {
            e.Fullname = e.Name;
        }

        if (e.Level == 2)
        {
            e.Fullname = e.ParentSideEffect.Name + ", " + e.Name;
        }

        if (e.Level == 3)
        {
            e.Fullname = e.ParentSideEffect.ParentSideEffect.Name + ", " + e.ParentSideEffect.Name + ", " + e.Name;
        }

        e.FullnameNoDiacritics = e.Fullname.RemoveDiacritics();
    }

    static public SideEffect GetByFullnameAndLevel(string fullname, int lvl)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var r = from e in db.SideEffects
                where e.Level.Value == lvl && e.Fullname == fullname.Trim()
                select e;
        if (r.Count() == 0)
            return null;
        else
            return r.First();
    }

    static public SideEffect GetByFullname(string fullname)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from e in db.SideEffects
                where e.Fullname.ToLower() == fullname.Trim().ToLower()
                select e).FirstOrDefault();
    }

    static public SideEffect GetByName(string name, int level)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from e in db.SideEffects
                where e.Name.ToLower() == name.Trim().ToLower() && e.Level == level
                select e).FirstOrDefault();
    }
}
