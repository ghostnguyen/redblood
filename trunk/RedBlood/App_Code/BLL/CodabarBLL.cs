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

    public bool IsValidTestResultCode(string code)
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
}
