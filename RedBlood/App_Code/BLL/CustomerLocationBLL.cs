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
/// Summary description for CustomerLocationBLL
/// </summary>
public class CustomerLocationBLL
{
    public CustomerLocationBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public CustomerLocation Get_byID(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var los = from l in db.CustomerLocations
                  where l.ID == ID
                  select l;


        if (los.Count() != 1) return null;
        else return los.First();
    }
    public void Update(Guid ID, string name, string phone, string fax, Guid? geoID1, Guid? geoID2, Guid? geoID3, string address)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var los = from location in db.CustomerLocations
                  where location.ID == ID
                  select location;

        if (los.Count() != 1) return;

        CustomerLocation l = los.First();

        l.Name = name;
        l.Phone = phone;
        l.Fax = fax;
        l.GeoID1 = geoID1;
        l.GeoID2 = geoID2;
        l.GeoID3 = geoID3;
        l.Address = address;

        db.SubmitChanges();
    }
}
