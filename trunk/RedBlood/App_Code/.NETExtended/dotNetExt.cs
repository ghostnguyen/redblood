﻿using System;
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
        return s.Replace("+", "%2B");
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
            return (dt < new DateTime(1883, 1, 1)) ? false : true;
        }
        else
        {
            return false;
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

    public static string ToStringVN(this DateTime dt)
    {
        return dt.ToString("dd/MM/yyyy");
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

    public static int Decade(this DateTime dt)
    {
        return dt.Year - dt.Year % 10;
    }

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

    public static int? ToIntNullable(this object o)
    {
        if (o == null) return null;
        else return o.ToString().ToIntNullable();
    }
}