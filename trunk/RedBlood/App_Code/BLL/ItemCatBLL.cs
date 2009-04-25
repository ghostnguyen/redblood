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
/// Summary description for CompanyBLL
/// </summary>
public class ItemCatBLL
{
    public ItemCatBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public ItemCat Insert(Guid itemID,string tableName)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var ics = from ic in db.ItemCats
                  where ic.ItemID == itemID && ic.TableName == tableName
                  select ic;

        if (ics.Count() == 0)
        {
            ItemCat ic = new ItemCat();
            ic.ItemID = itemID;
            ic.TableName = tableName;

            db.ItemCats.InsertOnSubmit(ic);
            db.SubmitChanges();
            return ic;
        }
        else if (ics.Count() == 1)
        {
            return ics.First();
        }
        else
        {
            return null;
        }
    }

    public void Update(Guid itemID, Guid? cat1ID, Guid? cat2ID, Guid? cat3ID, Guid? cat4ID, Guid? cat5ID, string tableName)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var ics = from ic in db.ItemCats
                  where ic.ItemID == itemID && ic.TableName == tableName
                  select ic;
        
        if (ics.Count() != 1) return;

        ItemCat icat = ics.First();

        icat.CatID1 = cat1ID;
        icat.CatID2 = cat2ID;
        icat.CatID3 = cat3ID;
        icat.CatID4 = cat4ID;
        icat.CatID5 = cat5ID;

        db.SubmitChanges();
    }

    public void InsertAndUpdate(Guid itemID, Guid? cat1ID, Guid? cat2ID, Guid? cat3ID, Guid? cat4ID, Guid? cat5ID, string tableName)
    {
        Insert(itemID, tableName);
        Update(itemID, cat1ID, cat2ID, cat3ID, cat4ID, cat5ID, tableName);
    }
}
