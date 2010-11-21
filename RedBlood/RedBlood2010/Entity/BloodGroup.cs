using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedBlood
{
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
        public string ABO { get; set; }
        public string Rh { get; set; }
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

        public BloodGroup(string code, string description, string abo, string rh = "")
        {
            Code = code;
            Description = description;
            ABO = abo;
            Rh = rh;
        }

        public static string RhNeg = "Rh NEGATIVE";
        public static string RhPos = "Rh POSITIVE";
        public static BloodGroup O_RhD_negative = new BloodGroup("9500", "O Rh-", "O", RhNeg);
        public static BloodGroup O_RhD_positive = new BloodGroup("5100", "O Rh+", "O", RhPos);

        public static BloodGroup A_RhD_negative = new BloodGroup("0600", "A Rh-", "A", RhNeg);
        public static BloodGroup A_RhD_positive = new BloodGroup("6200", "A Rh+", "A", RhPos);


        public static BloodGroup B_RhD_negative = new BloodGroup("1700", "B Rh-", "B", RhNeg);
        public static BloodGroup B_RhD_positive = new BloodGroup("7300", "B Rh+", "B", RhPos);

        public static BloodGroup AB_RhD_negative = new BloodGroup("2800", "AB Rh-", "AB", RhNeg);
        public static BloodGroup AB_RhD_positive = new BloodGroup("8400", "AB Rh+", "AB", RhPos);

        public static BloodGroup O = new BloodGroup("5500", "O", "O");
        public static BloodGroup A = new BloodGroup("6600", "A", "A");
        public static BloodGroup B = new BloodGroup("7700", "B", "B");
        public static BloodGroup AB = new BloodGroup("8800", "AB", "AB");

        public static BloodGroup para_Bombay_RhD_negative = new BloodGroup("D600", "para-Bombay, Rh-", "para-Bombay", RhNeg);
        public static BloodGroup para_Bombay_RhD_positive = new BloodGroup("E600", "para-Bombay, Rh+", "para-Bombay", RhPos);
        public static BloodGroup Bombay_RhD_negative = new BloodGroup("G600", "Bombay, Rh-", "Bombay", RhNeg);
        public static BloodGroup Bombay_RhD_positive = new BloodGroup("H600", "Bombay, Rh+", "Bombay", RhPos);

        public static List<BloodGroup> BloodGroupList = new List<BloodGroup>()
    {
        O_RhD_negative,O_RhD_positive,A_RhD_negative,A_RhD_positive,
        B_RhD_negative,B_RhD_positive,AB_RhD_negative,AB_RhD_positive,
        O,A,B,AB,
        para_Bombay_RhD_negative,para_Bombay_RhD_positive,
        Bombay_RhD_negative,Bombay_RhD_positive
    };

    }
}