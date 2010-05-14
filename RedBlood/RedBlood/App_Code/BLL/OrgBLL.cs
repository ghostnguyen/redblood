using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PointDefBLL
/// </summary>
public class OrgBLL
{
    public OrgBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Org[] Search(string searchStr)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        searchStr = searchStr.Trim();

        if (string.IsNullOrEmpty(searchStr))
        {
            return (from c in db.Orgs
                    select c).ToArray();
        }
        else
        {
            int ID = searchStr.ToInt();

            return (from c in db.Orgs
                    where c.Name.Contains(searchStr) || c.NameNoDiacritics.Contains(searchStr) || c.ID == ID
                    select c).ToArray();     
        }
    }

    public List<Org> SearchByGeo(string searchStr)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        searchStr = searchStr.Trim();

        if (string.IsNullOrEmpty(searchStr))
        {
            return (from c in db.Orgs
                    select c).ToList();
        }
        else
        {
            Geo g = GeoBLL.GetByFullname(searchStr);
            if (g == null) return new List<Org>();

            if (g.Level == 1)
            {
                return db.Orgs.Where(r => r.GeoID1 == g.ID).ToList();
            }
            if (g.Level == 2)
            {
                return db.Orgs.Where(r => r.GeoID2 == g.ID && r.GeoID1 == g.ParentGeo.ID).ToList();
            }
            if (g.Level == 3)
            {
                return db.Orgs.Where(r => r.GeoID3 == g.ID && r.GeoID2 == g.ParentGeo.ID && r.GeoID1 == g.ParentGeo.ParentGeo.ID).ToList();
            }
            return new List<Org>();
        }
    }

    public Org GetByID(int ID, out RedBloodDataContext db)
    {
        db = new RedBloodDataContext();

        if (ID == 0) return null;

        return (from c in db.Orgs
                where c.ID == ID
                select c).First();
    }

    public Org GetByID(int ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return GetByID(ID,out db);
    }

    public bool IsExistName(string name, int ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        int count = (from r in db.Orgs
                     where r.ID != ID && r.Name.Trim() == name.Trim()
                     select r).Count();
        
        if (count == 0) return false;
        else return true;
    }

    public static Org GetByName(string name)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        
        Org e = (from c in db.Orgs
                where c.Name == name.Trim()
                select c).FirstOrDefault();

        if (e == null)
            throw new Exception("Sai tên đơn vị.");

        return e;
    }

    public string Delete(int ID)
    {
        RedBloodDataContext db;
        Org e = GetByID(ID, out db);

        if (e != null)
        {
            try
            {
                db.Orgs.DeleteOnSubmit(e);
                db.SubmitChanges();
                return "Xóa thành công.";
            }
            catch (Exception)
            {
                throw new Exception("Xóa bị lỗi.");
            }
        }
        return "Không tìm thấy.";
    }
}
