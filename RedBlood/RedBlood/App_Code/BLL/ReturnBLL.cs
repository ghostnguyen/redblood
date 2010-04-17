using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderBLL
/// </summary>
public class ReturnBLL
{
    public ReturnBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Return Get(int ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(ID, db);
    }

    public static Return Get(int ID, RedBloodDataContext db)
    {
        Return e = db.Returns.Where(r => r.ID == ID).FirstOrDefault();

        if (e == null)
            throw new Exception("Không tìm thấy đợt trả về.");

        return e;
    }

    public static int Add(List<int> packOrderIDList, string note)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<PackOrder> poL = PackOrderBLL.Get4Return(db, packOrderIDList);

        Return re = new Return();
        re.Note = note;
        
        db.Returns.InsertOnSubmit(re);
        db.SubmitChanges();

        foreach (var item in packOrderIDList)
        {
            PackOrderBLL.Return(re.ID, item, note);
        }

        return re.ID;
    }
}
