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

/// <summary>
/// Summary description for CatBLL
/// </summary>
public class CatBLL
{
    public CatBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Cat[] GetByLevelAndParentID(Guid? parentID, int level)
    {
        if (parentID == Guid.Empty) parentID = null;
        
        RedBloodDataContext db = new RedBloodDataContext();

        var cats = from i in db.Cats
                   where object.Equals(i.ParentID, parentID) && i.Level == level
                   select i;

        return cats.ToArray();
    }

    public Cat GetByID(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from i in db.Cats
                where i.ID == ID
                select i).First();
    }

    public string Insert(string name, int level, Guid? parentID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Cat cat = new Cat();
        cat.Name = name.Trim();
        cat.Level = level;
        cat.ParentID = parentID;

        db.Cats.InsertOnSubmit(cat);

        try
        {
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return "";
    }

    public void Update(Guid ID, string name)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Cat c = (from i in db.Cats
                 where i.ID == ID
                 select i).First();

        if (c == null) return;

        c.Name = name;
        db.SubmitChanges();
    }

    public string Delete(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Cat c = (from i in db.Cats
                 where i.ID == ID
                 select i).First();

        if (c == null) return "";

        Delete_Route(c, db);

        //db.Cats.DeleteOnSubmit(c);

        try
        {
            db.SubmitChanges();
            return "";

        }
        catch (Exception)
        {
            return "Tồn tại sản phẩm trong danh mục này.";
        }
        
    }

    public void Delete_Route(Cat c, RedBloodDataContext db)
    {
        foreach (Cat cat in c.Cats)
        {
            Delete_Route(cat,db);
        }
        db.Cats.DeleteOnSubmit(c);
    }
}
