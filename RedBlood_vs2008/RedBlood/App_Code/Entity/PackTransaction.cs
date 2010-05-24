using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PackTransaction
/// </summary>
public partial class PackTransaction
{
    //the sign (pos or neg) of value of TypeX is used to calculate the remain pack in store.
    //If the sign is (-), it means the total pack is reduced one pack.
    //If the sign is (+), it means the total pack is added one pack.
    //Zero value is exception.
    public enum TypeX : int
    {
        Remain = 0,
        In_Collect = 10,
        In_Product = 11,
        In_Return = 12,
        
        Out_OrderGen = -20,
        Out_Order4Org = -21,
        Out_Order4CR = -22,
        
        Out_Product = -23,
        Out_Delete = -24,
        
        
    }
}
