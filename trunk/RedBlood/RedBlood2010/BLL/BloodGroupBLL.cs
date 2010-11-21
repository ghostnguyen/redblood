using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RedBlood.BLL
{
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

        public static string GetLetter(string code)
        {
            if (string.IsNullOrEmpty(code)) return "";

            BloodGroup e = BloodGroup.BloodGroupList.Where(r => r.Code == code.Trim()).FirstOrDefault();

            if (e != null)
                return e.ABO;
            else
                return "";
        }

        public static string GetRh(string code)
        {
            if (string.IsNullOrEmpty(code)) return "";

            BloodGroup e = BloodGroup.BloodGroupList.Where(r => r.Code == code.Trim()).FirstOrDefault();

            if (e != null)
                return e.Rh;
            else
                return "";
        }
    }
}