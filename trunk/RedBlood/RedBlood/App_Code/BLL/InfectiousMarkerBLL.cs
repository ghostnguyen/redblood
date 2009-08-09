using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InfectiousMarkerBLL
/// </summary>
public class InfectiousMarkerBLL
{
    public InfectiousMarkerBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string DecodeHIV(string code)
    {
        string s = code.Substring(1, 1);

        if (s == "0"
            || s == "1"
            || s == "2") return InfectiousMarker.na.Name;

        if (s == "3"
            || s == "4"
            || s == "5") return InfectiousMarker.neg.Name;

        if (s == "6"
            || s == "7"
            || s == "8") return InfectiousMarker.pos.Name;

        throw new Exception(); 
    }

    public static string DecodeHCV(string code)
    {
        string s = code.Substring(2, 1);

        if (s == "0"
            || s == "3"
            || s == "6") return InfectiousMarker.na.Name;

        if (s == "1"
            || s == "4"
            || s == "7") return InfectiousMarker.neg.Name;

        if (s == "2"
            || s == "5"
            || s == "8") return InfectiousMarker.pos.Name;

        throw new Exception();
    }

    public static string DecodeHBsAg(string code)
    {
        string s = code.Substring(3, 1);

        if (s == "0"
            || s == "3"
            || s == "6") return InfectiousMarker.na.Name;

        if (s == "1"
            || s == "4"
            || s == "7") return InfectiousMarker.neg.Name;

        if (s == "2"
            || s == "5"
            || s == "8") return InfectiousMarker.pos.Name;

        throw new Exception();
    }

    public static string DecodeSyphilis(string code)
    {
        string s = code.Substring(5, 1);

        if (s == "0"
             || s == "1"
             || s == "2") return InfectiousMarker.na.Name;

        if (s == "3"
            || s == "4"
            || s == "5") return InfectiousMarker.neg.Name;

        if (s == "6"
            || s == "7"
            || s == "8") return InfectiousMarker.pos.Name;

        throw new Exception(); 
    }

    public static string DecodeMalaria(string code)
    {
        string s = code.Substring(17, 1);

        if (s == "0"
            || s == "1"
            || s == "2") return InfectiousMarker.na.Name;

        if (s == "3"
            || s == "4"
            || s == "5") return InfectiousMarker.neg.Name;

        if (s == "6"
            || s == "7"
            || s == "8") return InfectiousMarker.pos.Name;

        throw new Exception();
    }
}
