using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public partial class Order
{
    public enum Typex : int    
    { 
        ToOrg = 1,
        ToPeople = 2
    }

    partial void OnNameChanging(string value)
    {
        if (string.IsNullOrEmpty(value.Trim()))
            throw new Exception("Nhập tên.");
    }
}
