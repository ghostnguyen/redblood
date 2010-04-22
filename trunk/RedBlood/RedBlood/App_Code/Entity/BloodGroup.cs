using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BloodGroup
/// </summary>
public class BloodGroup
{
    public string Code { get; set; }
    public string ABORhD
    {
        get { return Code.Substring(0, 2); }
    }

    /// <summary>
    /// provides Rh, Kell, and Miltenberger phenotypes
    /// </summary>
    public string r { get { return Code.Substring(2, 1); } }
    public string e = "0";
    public string Description { get; set; }

    public BloodGroup()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public BloodGroup(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public static BloodGroup O_RhD_negative = new BloodGroup("9500", "O Rh-");
    public static BloodGroup O_RhD_positive = new BloodGroup("5100", "O Rh+");

    public static BloodGroup A_RhD_negative = new BloodGroup("0600", "A Rh-");
    public static BloodGroup A_RhD_positive = new BloodGroup("6200", "A Rh+");


    public static BloodGroup B_RhD_negative = new BloodGroup("1700", "B Rh-");
    public static BloodGroup B_RhD_positive = new BloodGroup("7300", "B Rh+");

    public static BloodGroup AB_RhD_negative = new BloodGroup("2800", "AB Rh-");
    public static BloodGroup AB_RhD_positive = new BloodGroup("8400", "AB Rh+");

    public static BloodGroup O = new BloodGroup("5500", "O");
    public static BloodGroup A = new BloodGroup("6600", "A");
    public static BloodGroup B = new BloodGroup("7700", "B");
    public static BloodGroup AB = new BloodGroup("8800", "AB");

    public static BloodGroup para_Bombay_RhD_negative = new BloodGroup("D600", "para-Bombay, Rh-");
    public static BloodGroup para_Bombay_RhD_positive = new BloodGroup("E600", "para-Bombay, RhD+");
    public static BloodGroup Bombay_RhD_negative = new BloodGroup("G600", "Bombay, RhD-");
    public static BloodGroup Bombay_RhD_positive = new BloodGroup("H600", "Bombay, RhD+");

    public static List<BloodGroup> BloodGroupList = new List<BloodGroup>()
    {
        O_RhD_negative,O_RhD_positive,A_RhD_negative,A_RhD_positive,
        B_RhD_negative,B_RhD_positive,AB_RhD_negative,AB_RhD_positive,
        O,A,B,AB,
        para_Bombay_RhD_negative,para_Bombay_RhD_positive,
        Bombay_RhD_negative,Bombay_RhD_positive
    };

}
