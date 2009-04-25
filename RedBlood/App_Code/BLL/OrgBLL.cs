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

    public Org GetByName(string name)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        name = name.Trim();
        if (string.IsNullOrEmpty(name)) return null;

        return (from c in db.Orgs
                where c.Name == name
                select c).First();
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
