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

    public int SetGeo3(Guid? geo1ID, Guid? geo2ID, Guid? geo3ID)
    {
        GeoID1 = geo1ID;
        GeoID2 = geo2ID;
        GeoID3 = geo3ID;

        return 0;
    }

    public void SetGeo3(string value)
    {
        GeoBLL.Set3LevelByFullname(value, SetGeo3);
    }

    public string FullGeo
    {
        get
        {
            return GeoBLL.GetFullname(Geo1, Geo2, Geo3);
        }
    }

    public string FullAddress
    {
        get
        {
            return GeoBLL.GetFullAddress(Address, FullGeo);
        }
    }
}
