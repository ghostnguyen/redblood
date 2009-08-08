using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductBLL
/// </summary>
public class ProductBLL
{
    public ProductBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Product Get(string code)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return db.Products.Where(r => r.Code == code).FirstOrDefault();
    }
}
