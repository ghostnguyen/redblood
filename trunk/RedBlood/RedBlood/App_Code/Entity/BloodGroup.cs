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

    public static List<BloodGroup> BloodGroupList = new List<BloodGroup>(){
        new BloodGroup("9500", "O RhD negative")
        , new BloodGroup("5100", "O RhD positive")
        , new BloodGroup("0600", "A RhD negative")
        , new BloodGroup("6200", "A RhD positive")
        , new BloodGroup("1700", "B RhD negative")
        , new BloodGroup("7300", "B RhD Positive")
        , new BloodGroup("2800", "AB RhD negative")
        , new BloodGroup("8400", "AB RhD positive")
        , new BloodGroup("5500", "O")
        , new BloodGroup("6600", "A")
        , new BloodGroup("7700", "B")
        , new BloodGroup("8800", "AB")
        , new BloodGroup("D600", "para-Bombay, RhD negative")
        , new BloodGroup("E600", "para-Bombay, RhD positive")
        , new BloodGroup("G600", "Bombay, RhD negative")
        , new BloodGroup("H600", "Bombay, RhD positive")

    };
}
