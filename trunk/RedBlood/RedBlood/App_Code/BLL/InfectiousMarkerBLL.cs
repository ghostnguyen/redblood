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

    #region decode

    public static string DecodeHIV_Ag_Ab(string code)
    {
        string s = code.Substring(0, 1);

        if (s == "0") return InfectiousMarker.na.Name;

        if (s == "4") return InfectiousMarker.neg.Name;

        if (s == "8") return InfectiousMarker.pos.Name;

        throw new Exception();
    }

    public static string DecodeHCV_Ab(string code)
    {
        string s = code.Substring(1, 1);

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

    #endregion

    #region encode

    public static string EncodeHIV_Ag_Ab(string code,InfectiousMarker.TR result)
    {
        int index = 0;
        string s = code.Substring(index, 1);

        if (result.Name == InfectiousMarker.na.Name)
        {
            if (s == "0") return code;
            else return code.Replace(index, '0');
        }

        if (result.Name == InfectiousMarker.neg.Name)
        {
            if (s == "4") return code;
            else return code.Replace(index, '4');
        }

        if (result.Name == InfectiousMarker.pos.Name)
        {
            if (s == "8") return code;
            else return code.Replace(index, '8');
        }

        throw new Exception();
    }

    public static string EncodeHCV_Ab(string code, InfectiousMarker.TR result)
    {
        int index = 1;
        string s = code.Substring(index, 1);

        if (result.Name == InfectiousMarker.na.Name)
        {
            if (s == "0"
            || s == "3"
            || s == "6") return code;
            else
            {
                
                
                return code.Replace(index, '0');
            }
               
        }

        if (result.Name == InfectiousMarker.neg.Name)
        {
            if (s == "4") return code;
            else return code.Replace(index, '4');
        }

        if (result.Name == InfectiousMarker.pos.Name)
        {
            if (s == "8") return code;
            else return code.Replace(index, '8');
        }

        throw new Exception();

        
        string s = code.Substring(1, 1);

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

    #endregion
}
