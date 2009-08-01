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
/// Summary description for Org
/// </summary>
public partial class Org
{
    OrgBLL bll = new OrgBLL();
    GeoBLL geoBLL = new GeoBLL();
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            OnNameChanging(Name);
        }
    }

    partial void OnNameChanging(string value)
    {
        if (string.IsNullOrEmpty(value.Trim()))
            throw new Exception("Nhập tên đơn vị");

        if (bll.IsExistName(value.Trim(), ID))
            throw new Exception("Trùng tên");
    }

    partial void OnNameChanged()
    {
        if (!string.IsNullOrEmpty(Name))
            NameNoDiacritics = Name.RemoveDiacritics();
    }

    public void SetResidentGeo3(string value)
    {
        value = value.Trim();
        if (String.IsNullOrEmpty(value))
        {
            GeoID1 = null;
            GeoID2 = null;
            GeoID3 = null;
        }
        else
        {
            Geo g = GeoBLL.GetByFullname(value);
            if (g == null)
            {
                throw new Exception("Nhập sai đơn vị hành chính.");
            }
            else
            {
                GeoID1 = null;
                GeoID2 = null;
                GeoID3 = null;

                if (g.Level == 1)
                {
                    GeoID1 = g.ID;
                }

                if (g.Level == 2)
                {
                    GeoID2 = g.ID;
                    GeoID1 = g.ParentGeo.ID;
                }

                if (g.Level == 3)
                {
                    GeoID3 = g.ID;
                    GeoID2 = g.ParentGeo.ID;
                    GeoID1 = g.ParentGeo.ParentGeo.ID;
                }
            }
        }
    }
    public string FullGeo
    {
        get
        {
            string r = "";
            if (Geo3 != null)
                r += Geo3.Fullname;
            else if (Geo2 != null)
                r += Geo2.Fullname;
            else if (Geo1 != null)
                r += Geo1.Fullname;

            return r;
        }
    }

    public string FullAddress
    {
        get
        {
            return Address + ", " + FullGeo;
        }
    }
}
