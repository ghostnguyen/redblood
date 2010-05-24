using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedBlood;
/// <summary>
/// Summary description for PeopleBLL
/// </summary>
public class PeopleBLL
{
    BarcodeBLL codabarBLL = new BarcodeBLL();
    public PeopleBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static People GetByCMND(string CMND)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return db.Peoples.Where(r => r.CMND == CMND.Trim()).FirstOrDefault();
    }

    public static People GetByID(int autonum)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var e = from c in db.Peoples
                where c.Autonum == autonum
                select c;

        if (e.Count() != 1) return null;
        else return e.First();
    }
    
    public static People GetByID(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var e = from c in db.Peoples
                where c.ID == ID
                select c;


        if (e.Count() != 1) return null;
        else return e.First();
    }

    public static People GetByCode(string code)
    {
        int ID = BarcodeBLL.ParsePeopleCode(code);
        return GetByID(ID);
    }

    public bool IsCMNDDuplicated(string CMND, Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        int count = (from e in db.Peoples
                     where object.Equals(e.CMND, CMND.Trim()) && e.ID != ID
                     select e).Count();

        if (count > 0)
        {
            return true;
        }

        return false;
    }
}
