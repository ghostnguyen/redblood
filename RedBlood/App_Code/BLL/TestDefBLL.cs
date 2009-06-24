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
/// Summary description for TestDefBLL
/// </summary>
public class TestDefBLL
{
    public TestDefBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public TestDef[] GetByLevelAndParentID(int? parentID, int level)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var r = from i in db.TestDefs
                   where object.Equals(i.ParentID, parentID) && i.Level == level
                   select i;

        return r.ToArray();
    }

    public TestDef GetByID(int ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from i in db.TestDefs
                where i.ID == ID
                select i).FirstOrDefault();
    }

    public static TestDef Get(RedBloodDataContext db, int ID)
    {
        return (from i in db.TestDefs
                where i.ID == ID
                select i).FirstOrDefault();
    }

    //public static TestDef GetConst(int ID)
    //{
    //    return TestDef.all.Where(r => r.ID == ID).FirstOrDefault(); 
    //}

    public string Insert(string name, int level, int? parentID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        TestDef e = new TestDef();
        e.Name = name.Trim();
        e.Level = level;
        e.ParentID = parentID;

        db.TestDefs.InsertOnSubmit(e);

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

    public void Update(int ID, string name)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        TestDef c = (from i in db.TestDefs
                 where i.ID == ID
                 select i).First();

        if (c == null) return;

        c.Name = name;
        db.SubmitChanges();
    }

    public string Delete(int ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        TestDef c = (from i in db.TestDefs
                 where i.ID == ID
                 select i).First();

        if (c == null) return "";

        Delete_Route(c, db);

        try
        {
            db.SubmitChanges();
            return "";
        }
        catch (Exception)
        {
            return "Dữ liệu đã được sử dụng. Không thể xóa.";
        }
        
    }

    public void Delete_Route(TestDef c, RedBloodDataContext db)
    {
        foreach (TestDef e in c.Children)
        {
            Delete_Route(e, db);
        }
        db.TestDefs.DeleteOnSubmit(c);
    }

    
}
