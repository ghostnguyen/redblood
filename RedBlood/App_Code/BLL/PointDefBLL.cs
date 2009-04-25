using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PointDefBLL
/// </summary>
public class PointDefBLL
{
    public PointDefBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    CompanyBLL companyBLL = new CompanyBLL();
    public void UpdateStatus(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var r = from def in db.PointDefs
                where def.ID == ID
                select def;

        if (r.Count() == 0) return;

        r.First().Status = r.First().Status == 1 ? 2 : 1;

        db.SubmitChanges();
    }

    public Guid Insert(string name)
    {
        Company com = companyBLL.Select_First();
        if (com == null) return Guid.Empty;

        RedBloodDataContext db = new RedBloodDataContext();

        PointDef e = new PointDef();
        e.Name = name;        
        e.CompanyID = com.ID;
        e.Status = 1;

        db.PointDefs.InsertOnSubmit(e);
        db.SubmitChanges();
        return e.ID;
    }
}
