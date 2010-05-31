using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using RedBlood;
using RedBlood.BLL;
/// <summary>
/// Summary description for dotNetExt
/// </summary>
public static class dotNetExt
{
    #region String

    public static int? ToIntNullable4Zero(this string s)
    {
        if (string.IsNullOrEmpty(s.Trim())) return null;

        int i;

        return int.TryParse(s, out i) && i != 0 ? i : new Nullable<int>();
    }

    public static int? ToIntNullable(this string s)
    {
        if (string.IsNullOrEmpty(s.Trim())) return null;

        int i;

        return int.TryParse(s, out i) ? i : new Nullable<int>();
    }

    public static int ToInt(this string s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(s.Trim())) return 0;

        int i;

        return int.TryParse(s, out i) ? i : 0;
    }

    public static string AddNumber(this string s, int num)
    {
        return (s.ToInt() + num).ToString();
    }

    public static Guid ToGuid(this string s)
    {
        if (string.IsNullOrEmpty(s.Trim())) return Guid.Empty;

        Guid g = Guid.Empty;

        try
        {
            g = new Guid(s);
        }
        catch (Exception)
        {

        }

        return g;
    }

    public static string FromGuidToCodabar(this string s)
    {
        string r = s.ToLower().Replace("-", "");

        return r.Replace('a', '-').Replace('b', '$').Replace('c', ':').Replace('d', '/').Replace('e', '.').Replace('f', '+');
    }

    public static string FromCodabarToGuid(this string s)
    {
        return s.Replace('-', 'a').Replace('$', 'b').Replace(':', 'c').Replace('/', 'd').Replace('.', 'e').Replace('+', 'f');
    }

    public static string ToURLCompatible(this string s)
    {
        //return HttpUtility.UrlPathEncode(s);
        return HttpUtility.UrlEncode(s);

        //'	%27	Foot Sign
        //"	%22	Quote
        //#	%23	Number sign
        //%	%25	Percent sign
        //&	%26	Ampersand
        //+	\%2B	Plus sign ¹
        ///	%2F	Solidus (slash)
        //:	%3A	Colon
        //;	%3B	Semicolon
        //<	%3C	Less than
        //>	%3EGreater than
        //?	%3F

        //return s
        //    .Replace("%", "%25")
        //    .Replace("+", "%2B")
        //    .Replace("&", "%26")
        //    .Replace("=", "%3D")
        //    .Replace("<", "%3C")
        //    .Replace(";", "%3B")
        //    ;
    }



    public static string RemoveDiacritics(this string s)
    {
        String normalizedString = s.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < normalizedString.Length; i++)
        {
            Char c = normalizedString[i];

            if (c == 'Đ') c = 'D';
            if (c == 'đ') c = 'd';

            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }

        return stringBuilder.ToString();
    }

    public static bool IsValidDOB(this string s)
    {
        CultureInfo MyCultureInfo = new CultureInfo("vi-VN");
        DateTime dt;

        if (DateTime.TryParse(s.Trim(), MyCultureInfo, DateTimeStyles.None, out dt))
        {
            return (dt < new DateTime(1888, 1, 1)) ? false : true;
        }
        else
        {
            return false;
        }
    }

    public static DateTime? ToShortDate(this string s)
    {
        DateTime dt;

        if (DateTime.TryParse(s.Trim(), out dt))
        {
            return (DateTime?)dt;
        }
        else
        {
            return null;
        }
    }

    public static DateTime? ToDatetimeFromVNFormat(this string s)
    {
        CultureInfo MyCultureInfo = new CultureInfo("vi-VN");
        DateTime dt;

        if (DateTime.TryParse(s.Trim(), MyCultureInfo, DateTimeStyles.None, out dt))
        {
            return (DateTime?)dt;
        }
        else
        {
            return null;
        }
    }

    public static string Replace(this string s, int index, char c)
    {
        string sub = s.Substring(index, 1);

        if (string.IsNullOrEmpty(sub)) return s;

        return s.Substring(0, index) + c.ToString() + s.Substring(index + 1);
    }

    public static bool IsValidProductCode(this string s)
    {
        return BarcodeBLL.IsValidProductCode(s);
    }

    public static bool IsValidDINCode(this string s)
    {
        return BarcodeBLL.IsValidDINCode(s);
    }

    public static bool IsValidProductCodeList(this List<string> s)
    {
        return s.Where(r => BarcodeBLL.IsValidProductCode(r) == false).Count() == 0;
    }

    public static bool IsValidDINList(this List<string> s)
    {
        return s.Where(r => BarcodeBLL.IsValidDINCode(r) == false).Count() == 0;
    }

    #endregion

    public static TreeNode Find(this System.Web.UI.WebControls.TreeNode node, string childValue)
    {
        foreach (TreeNode childNode in node.ChildNodes)
        {
            if (childNode.Value == childValue)
                return childNode;
        }

        return null;
    }

    public static SiteMapNode Find(this SiteMapNode node, string url)
    {
        SiteMapNode f;
        foreach (SiteMapNode e in node.ChildNodes)
        {
            if (e.Url.ToLower().Trim() == url.ToLower().Trim())
            {
                f = e;
            }
            else f = e.Find(url);

            if (f != null) return f;
        }
        return null;
    }

    #region DateTime

    public static string ToStringVN(this DateTime dt)
    {
        return dt.ToString("dd/MM/yyyy");
    }

    public static string ToStringVNLong(this DateTime dt)
    {
        return dt.ToString("dd/MM/yyyy HH:mm:ss");
    }

    public static string ToStringVN(this DateTime? dt)
    {
        if (dt.HasValue)
        {
            return dt.Value.ToString("dd/MM/yyyy");
        }
        return "";
    }

    public static string ToStringVN_Hour(this DateTime? dt)
    {
        if (dt.HasValue)
        {
            return dt.Value.ToString("dd/MM/yyyy HH:mm");
        }
        return "";
    }

    public static DateTime AddMonthsAvoidWeekend(this DateTime dt, int months)
    {
        DateTime temp = dt.AddMonths(months);

        if (temp.DayOfWeek == DayOfWeek.Saturday)
            temp = temp.AddDays(2);

        if (temp.DayOfWeek == DayOfWeek.Sunday)
            temp = temp.AddDays(1);

        return temp;
    }

    /// <summary>
    /// Ex: 1970, 1980, 1990.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static int Decade(this DateTime dt)
    {
        return dt.Year - dt.Year % 10;
    }

    public static int DurationInDays(this DateTime dt)
    {
        return (dt - RedBloodSystem.RootTime).Days;
    }

    public static bool Expired(this DateTime dt)
    {
        return dt <= DateTime.Now.Date;
    }

    public static bool ExpiredInDays(this DateTime dt, int days)
    {
        return dt <= DateTime.Now.Date.AddDays(days);
    }

    #endregion

    public static void SelectByText(this DropDownList ddl, string text)
    {
        ListItem li = ddl.Items.FindByText(text);
        if (li != null)
            ddl.SelectedValue = li.Value;
    }

    public static object SelectedValue(this BulletedList bl)
    {
        foreach (ListItem item in bl.Items)
        {
            if (item.Selected == true)
            {
                return item.Value;
            }
        }
        return 0;
    }

    public static string ToStringRemoveZero(this int i)
    {
        return i == 0 ? "" : i.ToString();
    }

    public static string ToStringRemoveZero(this int? i)
    {
        return i.HasValue ? i.Value.ToStringRemoveZero() : "";
    }

    public static int ToInt(this object o)
    {
        if (o == null) return 0;
        else return o.ToString().ToInt();
    }

    public static int? ToIntNullable(this object o)
    {
        if (o == null) return null;
        else return o.ToString().ToIntNullable();
    }

    public static void Apply(this CssStyleCollection style, PrintSetting setting)
    {
        if (setting == null) return;

        if (string.IsNullOrEmpty(setting.Top))
        { style.Remove("top"); }
        else
        { style.Add("top", setting.Top); }

        if (string.IsNullOrEmpty(setting.Left))
        { style.Remove("left"); }
        else
        { style.Add("left", setting.Left); }

        if (string.IsNullOrEmpty(setting.Font))
        { style.Remove("font"); }
        else
        { style.Add("font", setting.Font); }

        if (string.IsNullOrEmpty(setting.Size))
        { style.Remove("font-size"); }
        else
        { style.Add("font-size", setting.Size); }

        if (string.IsNullOrEmpty(setting.Width))
        { style.Remove("width"); }
        else
        { style.Add("width", setting.Width); }

        if (string.IsNullOrEmpty(setting.Height))
        { style.Remove("height"); }
        else
        { style.Add("height", setting.Height); }
    }

    public static void Visibility(this CssStyleCollection style, bool visibility)
    {
        style.Remove("visibility");

        if (visibility)
            style.Add("visibility", "visible");
        else
            style.Add("visibility", "hidden");
    }

    public static void Alert(this Page p, string msg)
    {
        ScriptManager.RegisterStartupScript(p, p.GetType(), "",
                     "alert ('" + msg + "');", true);
    }

}
