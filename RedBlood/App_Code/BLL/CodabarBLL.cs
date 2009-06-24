using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CodabarBLL
/// </summary>
public class CodabarBLL
{
    //http://localhost:8449/RedBlood/CodarBar/Image.aspx
    public static string CodabarImgPage { get; set; }

    static string hospitalCode;
    public CodabarBLL()
    {
        HospitalBLL hospitalBLL = new HospitalBLL();
        Hospital h = hospitalBLL.Select_First();
        hospitalCode = h.Code;
    }

    public static string GenPackCode(int autonum)
    {
        return GenStringCode(Resources.Codabar.packSSC, autonum.ToString());
    }

    public static string GenStringCode(string ssc, string code)
    {
        if (code.Length > 0)
        {
            if (ssc == Resources.Codabar.packSSC)
            {
                code = hospitalCode + "-" + code.ToInt().ToString("D" + Resources.Codabar.packLength.AddNumber(-2 - 1 - hospitalCode.Length));
            }
            if (ssc == Resources.Codabar.testResultSSC)
            {
                code = code.ToInt().ToString("D" + Resources.Codabar.testResultLength.AddNumber(-2));
            }
            if (ssc == Resources.Codabar.peopleSSC)
            {
                code = code.FromGuidToCodabar().ToURLCompatible();
            }
            if (ssc == Resources.Codabar.orgSSC)
            {
                code = code.ToInt().ToString("D" + Resources.Codabar.orgLength.AddNumber(-2));
            }
            if (ssc == Resources.Codabar.campaignSSC)
            {
                code = code.ToInt().ToString("D" + Resources.Codabar.campaignLength.AddNumber(-2));
            }
            if (ssc == Resources.Codabar.orderSSC)
            {
                code = code.ToInt().ToString("D" + Resources.Codabar.orderLength.AddNumber(-2));
            }

            return ssc[0].ToString() + code + ssc[1].ToString();
        }

        return "";
    }

    public static bool IsValidPeopleCode(string code)
    {
        if (code.Length == Resources.Codabar.peopleLength.ToInt()
            && code[0].ToString() == Resources.Codabar.peopleStarCode
            && code[code.Length - 1].ToString() == Resources.Codabar.peopleStopCode
            && code.Substring(1, Resources.Codabar.peopleLength.ToInt() - 1).FromCodabarToGuid().ToGuid() != Guid.Empty)
        {
            return true;
        }

        return false;
    }

    public static bool IsValidPackCode(string code)
    {
        string pattern = Resources.Codabar.packStarCode + "[0-9]{2}-[0-9]{" + Resources.Codabar.packLength.AddNumber(-5) + "}" + Resources.Codabar.packStopCode;
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static bool IsValidTestResultCode(string code)
    {
        string pattern = Resources.Codabar.testResultStartCode + "[0-9]{" + Resources.Codabar.packLength.AddNumber(-2) + "}" + Resources.Codabar.testResultStopCode;
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static bool IsValidCampaignCode(string code)
    {
        string pattern = Resources.Codabar.campaignStartCode + "[0-9]{" + Resources.Codabar.campaignLength.AddNumber(-2) + "}" + Resources.Codabar.campaignStopCode;
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static bool IsValidOrderCode(string code)
    {
        string pattern = Resources.Codabar.orderStartCode + "[0-9]{" + Resources.Codabar.orderLength.AddNumber(-2) + "}" + Resources.Codabar.orderStopCode;
        Regex regx = new Regex(pattern);
        return regx.IsMatch(code);
    }

    public static Guid ParsePeopleCode(string code)
    {
        if (IsValidPeopleCode(code))
            return code.Substring(1, Resources.Codabar.peopleLength.ToInt() - 1).ToGuid();
        else
            return Guid.Empty;
    }

    public static int ParsePackAutoNum(string code)
    {
        if (IsValidPackCode(code))
            //a01-123456789b
            return code.Substring(4, Resources.Codabar.packLength.ToInt() - 5).ToInt();
        else
            return 0;
    }

    public static int ParseCampaignID(string code)
    {
        if (IsValidCampaignCode(code))
            //a0001c
            return code.Substring(1, Resources.Codabar.campaignLength.ToInt() - 2).ToInt();
        else
            return 0;
    }

    public static int ParseOrderID(string code)
    {
        if (IsValidOrderCode(code))
            //a123456789d
            return code.Substring(1, Resources.Codabar.orderLength.ToInt() - 2).ToInt();
        else
            return 0;
    }

    public static string Url4People(Guid ID)
    {
        return CodabarImgPage + "?code=" + GenStringCode(Resources.Codabar.peopleSSC, ID.ToString());
    }

    public static string Url4Pack(int? autonum)
    {
        return Url4Pack(autonum.Value);
    }
    
    public static string Url4Pack(int autonum)
    {
        Pack p = PackBLL.Get(autonum);

        if (p == null) return "none";

        string topleft ="";
        string topright ="";
        string top = "";

        if(p.ComponentID == TestDef.Component.RBC
            || p.ComponentID == TestDef.Component.FFPlasma)
        {
            topleft = p.Component.Name;
            PackExtract pe = p.PackExtractsByExtract.FirstOrDefault();

            if (pe != null) topright = pe.SourcePack.Autonum.ToString();

            top = "&topleft=" + topleft + "&topright=" + topright;
        }

        return CodabarImgPage + "?hasText=true&code=" + CodabarBLL.GenPackCode(autonum) + top;
    }

    public static string Url4Campaign(int ID)
    {
        return CodabarImgPage + "?hasText=true&code=" + GenStringCode(Resources.Codabar.campaignSSC, ID.ToString());
    }

    public static string Url4Org(int ID)
    {
        return CodabarImgPage + "?hasText=true&code=" + GenStringCode(Resources.Codabar.orgSSC, ID.ToString());
    }

    public static string Url4Order(int ID)
    {
        return CodabarImgPage + "?hasText=true&code=" + GenStringCode(Resources.Codabar.orderSSC, ID.ToString());
    }

    public static string JScript4Postback()
    {
        StringBuilder script = new StringBuilder();

        script.Append("function checkLength(text) \n");
        script.Append("{ \n");
        script.Append("var len = text.length;  \n");

        script.Append("if (len == "
            + Resources.Codabar.testResultLength
            + " && text[0] == "
            + "\"" + Resources.Codabar.testResultStartCode + "\""
            + " && text[len - 1] == "
            + "\"" + Resources.Codabar.testResultStopCode + "\""
            + ") \n");
        script.Append("{ \n");
        script.Append("document.forms[0].submit(); \n");
        script.Append("} \n");

        script.Append("if (len == "
            + Resources.Codabar.packLength
            + " && text[0] == "
            + "\"" + Resources.Codabar.packStarCode + "\""
            + " && text[len - 1] == "
            + "\"" + Resources.Codabar.packStopCode + "\""
            + ") \n");
        script.Append("{ \n");
        script.Append("document.forms[0].submit(); \n");
        script.Append("} \n");

        script.Append("if (len == "
            + Resources.Codabar.peopleLength
            + " && text[0] == "
            + "\"" + Resources.Codabar.peopleStarCode + "\""
            + " && text[len - 1] == "
            + "\"" + Resources.Codabar.peopleStopCode + "\""
            + ") \n");
        script.Append("{ \n");
        script.Append("document.forms[0].submit(); \n");
        script.Append("} \n");

        script.Append("} \n");

        return script.ToString();
    }
}
