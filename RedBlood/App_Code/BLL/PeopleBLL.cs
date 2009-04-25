using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PeopleBLL
/// </summary>
public class PeopleBLL
{
    CodabarBLL codabarBLL = new CodabarBLL();
    public PeopleBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public People GetByCMND(string CMND)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var e = from c in db.Peoples
                where c.CMND == CMND.Trim()
                select c;


        if (e.Count() != 1) return null;
        else return e.First();

    }

    public People GetByID(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var e = from c in db.Peoples
                where c.ID == ID
                select c;


        if (e.Count() != 1) return null;
        else return e.First();
    }

    public People GetByCode(string code)
    {
        Guid ID = codabarBLL.ParsePeopleCode(code);
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
