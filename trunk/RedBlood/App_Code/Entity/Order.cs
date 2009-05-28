using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public partial class Order
{
    public enum TypeX : int    
    { 
        ToOrg = 1,
        ToPeople = 2
    }

    public enum StatusX : int
    {
        Init = 1,
        Done = 2
    }

    partial void OnNameChanging(string value)
    {
        if (string.IsNullOrEmpty(value.Trim()))
            throw new Exception("Nhập tên.");
    }

    public void SetOrgID(string value)
    {
        value = value.Trim();
        if (String.IsNullOrEmpty(value))
        {
            OrgID = null;
        }
        else
        {
            Org g = OrgBLL.GetByName(value);
            if (g == null)
            {
                throw new Exception("Nhập sai tên đơn vị.");
            }
            else
            {
                OrgID = g.ID;
            }
        }
    }
}
