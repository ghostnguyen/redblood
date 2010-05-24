using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedBlood;
/// <summary>
/// Summary description for BloodGroupBLL
/// </summary>
public class BloodGroupBLL
{
    public BloodGroupBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetDescription(string code)
    {
        if (string.IsNullOrEmpty(code)) return "";

        BloodGroup e = BloodGroup.BloodGroupList.Where(r => r.Code == code.Trim()).FirstOrDefault();

        if (e != null)
            return e.Description;
        else
            return "";
    }
}
