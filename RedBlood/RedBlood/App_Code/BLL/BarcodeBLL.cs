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

    #region Constant

    public const string campaignIdChar = "&c";
    public const int campaignLength = 6;

    public const int CMNDLength = 9;

    public const string DINIdChar = "=";
    public const int DINLength = 16;

    public const string orderIdChar = "&o";
    public const int orderLength = 11;

    public const string orgIdChar = "&e";
    public const int orgLength = 6;

    public const string peopleIdChar = "&;";
    public const int peopleLength = 18;

    public const string productIdChar = "=<";
    public const int productLength = 10;

    public const int InfectiousMarkersLength = 20;
    public const string InfectiousMarkersIdChar = "&\"";

    public const string bloodGroupIdChar = "=%";
    public const int bloodGroupLength = 6;

    #endregion

    public BarcodeBLL()
    {
    }

    #region Validation

    public static bool IsValidPeopleCode(string code)
    {
        if (code.Length == peopleLength
            && code.Substring(0, 2) == peopleIdChar
            && code.Substring(2, peopleLength - 2).ToInt() != 0)
        {
            return true;
        }

        return false;
    }

    public static bool IsValidDINCode(string code)
    {
        string pattern = DINIdChar + "[A-NP-Z1-9]{1}[0-9]{14}";

        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static bool IsValidCampaignCode(string code)
    {
        if (code.Length != campaignLength) return false;

        string pattern = campaignIdChar + "[0-9]";
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static bool IsValidOrderCode(string code)
    {
        if (code.Length != orderLength) return false;

        string pattern = orderIdChar + "[0-9]";
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static bool IsValidProductCode(string code)
    {
        if (code.Length != productLength) return false;

        string pattern = productIdChar + "[A-DE-Z1-9]{1}[0-9]{4}[A-Za-z0-9]{1}[A-Z0-9]{1}[a-z0-9]{1}";
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static bool IsValidBloodGroupCode(string code)
    {
        if (code.Length != bloodGroupLength) return false;

        string pattern = bloodGroupIdChar + "[A-Za-a0-9]{2}[A-Z0-9]{2}";
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static bool IsValidInfectiousMarkersCode(string code)
    {
        if (code.Length != InfectiousMarkersLength) return false;

        string pattern = InfectiousMarkersIdChar + "[0-9]";
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    #endregion

    #region ParseCode

    public static int ParsePeopleCode(string code)
    {
        if (IsValidPeopleCode(code))
            return code.Substring(2, peopleLength - 2).ToInt();
        else
            return 0;
    }

    public static string ParseDIN(string code)
    {
        if (IsValidDINCode(code))
            //=V01000912345600
            return code.Substring(1, DINLength - 3);
        else
            return "";
    }

    public static int ParseCampaignID(string code)
    {
        if (IsValidCampaignCode(code))
            //&c1234
            return code.Substring(2, campaignLength - 2).ToInt();
        else
            return 0;
    }

    public static int ParseOrderID(string code)
    {
        if (IsValidOrderCode(code))
            //&o123456789
            return code.Substring(2, orderLength - 2).ToInt();
        else
            return 0;
    }

    public static string ParseProductCode(string code)
    {
        if (IsValidProductCode(code))
            return code.Substring(2, productLength - 2);
        else
            return "";
    }

    public static string ParseBloodGroupCode(string code)
    {
        if (IsValidBloodGroupCode(code))
            return code.Substring(2, bloodGroupLength - 2);
        else
            return "";
    }

    public static string ParseInfectiousMakersCode(string code)
    {
        if (IsValidInfectiousMarkersCode(code))
            return code.Substring(2, InfectiousMarkersLength - 2);
        else
            return "";
    }

    #endregion

    #region Url

    public static string Url4DIN(string DIN)
    {
        return Url4DIN(DIN, "00");
    }

    public static string Url4DIN(string DIN, string flag)
    {
        return BarcodeImgPage + "?hasText=true&checkChar=true&IdChar=" + DINIdChar + "&code=" + DIN + flag;
    }


    public static string Url4People(int autonum)
    {
        return BarcodeImgPage + "?hasText=true&checkChar=true&IdChar=" + peopleIdChar.ToURLCompatible() + "&code=" + autonum.ToString("D" + (peopleLength - 2).ToString());
    }

    public static string Url4Product(string code)
    {
        return BarcodeImgPage + "?hasText=true&code=" + productIdChar.ToURLCompatible() + code;
    }

    public static string Url4BloodGroup(string code)
    {
        return BarcodeImgPage + "?hasText=true&code=" + bloodGroupIdChar.ToURLCompatible() + code;
    }

    public static string Url4Campaign(int ID)
    {
        return (BarcodeImgPage + "?hasText=true&code=" + campaignIdChar.ToURLCompatible() + ID.ToString("D" + (campaignLength - 2).ToString()));
    }

    public static string Url4Org(int ID)
    {
        return BarcodeImgPage + "?hasText=true&code=" + orgIdChar.ToURLCompatible() + ID.ToString("D" + (orderLength - 2).ToString());
    }

    public static string Url4Order(int ID)
    {
        return BarcodeImgPage + "?hasText=true&code=" + orderIdChar.ToURLCompatible() + ID.ToString("D" + (orderLength - 2).ToString());
    }

    #endregion

    public static string JScript4Postback()
    {
        StringBuilder script = new StringBuilder();

        script.Append("function checkLength(text) \n");
        script.Append("{ \n");
        script.Append("text = trim(text);  \n");
        script.Append("var len = text.length;  \n");

        JScript4Postback4EachElement(script, BarcodeBLL.DINLength - 2, BarcodeBLL.DINIdChar);
        JScript4Postback4EachElement(script, BarcodeBLL.DINLength, BarcodeBLL.DINIdChar);
        JScript4Postback4EachElement(script, BarcodeBLL.peopleLength, BarcodeBLL.productIdChar);
        JScript4Postback4EachElement(script, BarcodeBLL.bloodGroupLength, BarcodeBLL.bloodGroupIdChar);
        JScript4Postback4EachElement(script, BarcodeBLL.campaignLength, BarcodeBLL.campaignIdChar);
        JScript4Postback4EachElement(script, BarcodeBLL.InfectiousMarkersLength, BarcodeBLL.InfectiousMarkersIdChar);
        JScript4Postback4EachElement(script, BarcodeBLL.orderLength, BarcodeBLL.orgIdChar);
        JScript4Postback4EachElement(script, BarcodeBLL.productLength, BarcodeBLL.productIdChar);
        //JScript4Postback4EachElement(script, BarcodeBLL.CMNDLength, "");

        script.Append("} \n");

        return script.ToString();
    }

    private static void JScript4Postback4EachElement(StringBuilder script, int length, string IdChar)
    {
        script.Append("if (len == "
            + length
            + " && text.substring(0," + IdChar.Length + ") == "
            //+ "\"" + IdChar + "\""
            + "'" + IdChar + "'"
            + ") \n");
        script.Append("{ \n");
        //script.Append("alert('sdf'); \n");
        script.Append("document.forms[0].submit(); \n");
        script.Append("} \n");
    }


    public static string CalculateISO7064Mod37_2(string inputString)
    {
        int sum, charValue;
        bool isDigit, isUpperAlpha;
        string iso7064ValueToCharTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ*";
        // Read the characters from left to right. 
        sum = 0;
        foreach (char ch in inputString)
        {
            // Ignore invalid characters as per ISO 7064. 
            isDigit = ((ch >= '0') && (ch <= '9'));

            isUpperAlpha = ((ch >= 'A') && (ch <= 'Z'));
            if (isDigit || isUpperAlpha)
            {
                // Convert the character to its ISO 7064 value. 
                if (isDigit)
                    charValue = ch - '0';
                else
                    charValue = ch - 'A' + 10;
                // Add the character value to the accumulating sum, 
                // multiply by two, and do an intermediate modulus to 
                // prevent integer overflow. 
                sum = ((sum + charValue) * 2) % 37;
            }
        }
        // Find the value, that when added to the result of the above 
        // calculation, would result in a number who’s modulus 37 
        // result is equal to 1. 
        charValue = (38 - sum) % 37;

        // Convert the value to a character and return it. 
        return (iso7064ValueToCharTable[charValue]).ToString();
    }


}
