using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PackTransaction
/// </summary>
public partial class PackTransaction
{
    public enum TypeX : int
    {
        In_Collect = 10,
        In_Product = 11,
        In_Return = 12,
        Out_Order = 20,
        Out_Product = 21,
        //22
        //23
        Out_Delete = 24,
        Remain = 99
    }
}
