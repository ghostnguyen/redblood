using System;
using System.Data;
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
/// Summary description for FurnitureBLL
/// </summary>
public class FurnitureBLL
{
    CompanyBLL companyBLL = new CompanyBLL();
    ItemCatBLL itemCatBLL = new ItemCatBLL();
    public FurnitureBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Update(Guid? furnitureID, string name, string code, string serialNumber, string dimension, string color, string material,
        string unit1Name, string unit2Name, string unit3Name, int? unit12Factor, int? unit23Factor,
        Guid? cat1ID, Guid? cat2ID, Guid? cat3ID, Guid? cat4ID, Guid? cat5ID)
    {
        if (furnitureID == null || furnitureID.Value == Guid.Empty) return "";

        RedBloodDataContext db = new RedBloodDataContext();

        var items = from i in db.Furnitures
                    where i.ID == furnitureID
                    select i;

        if (items.Count() != 1) return "";

        Furniture item = items.First();

        item.Name = name;
        item.Code = code;
        item.SerialNumber = serialNumber;
        item.Dimension = dimension;
        item.Color = color;
        item.Material = material;
        item.Unit1Name = unit1Name;
        item.Unit2Name = unit2Name;
        item.Unit3Name = unit3Name;

        item.Unit12Factor = unit12Factor;
        item.Unit23Factor = unit23Factor;

        try
        {
            db.SubmitChanges();

            itemCatBLL.Update(item.ID, cat1ID, cat2ID, cat3ID, cat4ID, cat5ID, "Furniture");

        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return "";
    }

    public Furniture Insert(Guid? cat1ID, Guid? cat2ID, Guid? cat3ID, Guid? cat4ID, Guid? cat5ID)
    {
        Company com = companyBLL.Select_First();
        if (com == null) return null;

        RedBloodDataContext db = new RedBloodDataContext();

        Furniture i = new Furniture();
        i.Name = "_Tên sản phẩm";
        i.Code = "MaSP";
        i.CompanyID = com.ID;

        db.Furnitures.InsertOnSubmit(i);
        db.SubmitChanges();

        itemCatBLL.InsertAndUpdate(i.ID, cat1ID, cat2ID, cat3ID, cat4ID, cat5ID, "Furniture");

        return i;
    }

    public Furniture GetByID(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        
        var items = from i in db.Furnitures
                    where i.ID == ID
                    select i;

        if (items.Count() != 1) return null;
        else return items.First();
    }
}
