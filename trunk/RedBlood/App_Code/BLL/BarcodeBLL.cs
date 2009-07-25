using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BarcodeBLL
/// </summary>
public class BarcodeBLL
{
    //http://localhost:8449/RedBlood/Barcode/Image.aspx
    public static string BarcodeImgPage { get; set; }

    public BarcodeBLL()
    {
    }

    #region Validation

    public static bool IsValidPeopleCode(string code)
    {
        if (code.Length == Resources.Barcode.peopleLength.ToInt()
            && code.Substring(0, 2) == Resources.Barcode.peopleIdChar
            && code.Substring(2, Resources.Barcode.peopleLength.ToInt() - 2).ToGuid() != Guid.Empty)
        {
            return true;
        }

        return false;
    }

    public static bool IsValidDINCode(string code)
    {
        string pattern = Resources.Barcode.DINIdChar + "[A-NP-Z1-9]{1}[0-9]{14}";

        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static bool IsValidCampaignCode(string code)
    {
        if (code.Length != Resources.Barcode.campaignLength.ToInt()) return false;

        string pattern = Resources.Barcode.campaignIdChar + "[0-9]";
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static bool IsValidOrderCode(string code)
    {
        if (code.Length != Resources.Barcode.orderLength.ToInt()) return false;

        string pattern = Resources.Barcode.orderIdChar + "[0-9]";
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    #endregion

    #region ParseCode

    public static Guid ParsePeopleCode(string code)
    {
        if (IsValidPeopleCode(code))
            return code.Substring(2, Resources.Barcode.peopleLength.ToInt() - 2).ToGuid();
        else
            return Guid.Empty;
    }

    public static string ParseDIN(string code)
    {
        if (IsValidDINCode(code))
            //=V01000912345600
            return code.Substring(1, Resources.Barcode.DINLength.ToInt() - 3);
        else
            return "";
    }

    public static int ParseCampaignID(string code)
    {
        if (IsValidCampaignCode(code))
            //&c1234
            return code.Substring(2, Resources.Barcode.campaignLength.ToInt() - 2).ToInt();
        else
            return 0;
    }

    public static int ParseOrderID(string code)
    {
        if (IsValidOrderCode(code))
            //&o123456789
            return code.Substring(1, Resources.Barcode.orderLength.ToInt() - 2).ToInt();
        else
            return 0;
    }

    #endregion

    #region Url

    public static string Url4People(Guid ID)
    {
        return BarcodeImgPage + "?code=" + Resources.Barcode.productIdChar + ID.ToString();
    }

    public static string Url4Product(string code)
    {
        return BarcodeImgPage + "?code=" + Resources.Barcode.productIdChar + code;
    }

    public static string Url4Campaign(int ID)
    {
        return BarcodeImgPage + "?hasText=true&code=";
    }

    public static string Url4Org(int ID)
    {
        return BarcodeImgPage + "?hasText=true&code=";
    }

    public static string Url4Order(int ID)
    {
        return BarcodeImgPage + "?hasText=true&code=";
    }

    #endregion

    public static string JScript4Postback()
    {
        StringBuilder script = new StringBuilder();

        script.Append("function checkLength(text) \n");
        script.Append("{ \n");
        script.Append("var len = text.length;  \n");

        //script.Append("if (len == "
        //    + Resources.Barcode.testResultLength
        //    + " && text[0] == "
        //    + "\"" + Resources.Codabar.testResultStartCode + "\""
        //    + " && text[len - 1] == "
        //    + "\"" + Resources.Codabar.testResultStopCode + "\""
        //    + ") \n");
        //script.Append("{ \n");
        //script.Append("document.forms[0].submit(); \n");
        //script.Append("} \n");

        //script.Append("if (len == "
        //    + Resources.Barcode.productLength
        //    + " && text[0] == "
        //    + "\"" + Resources.Barcode.packStarCode + "\""
        //    + " && text[len - 1] == "
        //    + "\"" + Resources.Barcode.packStopCode + "\""
        //    + ") \n");
        //script.Append("{ \n");
        //script.Append("document.forms[0].submit(); \n");
        //script.Append("} \n");

        //script.Append("if (len == "
        //    + Resources.Codabar.peopleLength
        //    + " && text[0] == "
        //    + "\"" + Resources.Codabar.peopleStarCode + "\""
        //    + " && text[len - 1] == "
        //    + "\"" + Resources.Codabar.peopleStopCode + "\""
        //    + ") \n");
        //script.Append("{ \n");
        //script.Append("document.forms[0].submit(); \n");
        //script.Append("} \n");

        script.Append("} \n");

        return script.ToString();
    }
}
