using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for SystemBLL
/// </summary>
public class SystemBLL
{
    public static string Url4CampaignDetail = "~/FindAndReport/CampaignDetail.aspx?";
    public static string Url4PackDetail = "~/FindAndReport/PackDetail.aspx?";
    public static string Url4PeopleDetail = "~/FindAndReport/PeopleDetail.aspx?";
    public static string Url4OrderDetail = "~/Order/Order.aspx?";
    public static string Url4FindPeople = "~/FindAndReport/FindPeople.aspx?";

    public static TimeSpan ExpTime4ProduceFFPlasma = new TimeSpan(0, 18, 0, 0);

    public SystemBLL()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public static void SOD()
    {
        ScanExp(true);
        CloseOrder(true);
        LockEnterTestResult(true);
    }

    //isSOD: isStartOfDate
    public static void ScanExp(bool isSOD)
    {
        if (!isSOD || !LogBLL.IsLog(Task.TaskX.ScanExp))
        {
            RedBloodDataContext db = new RedBloodDataContext();

            List<Pack.StatusX> statusList = new List<Pack.StatusX> { Pack.StatusX.Product };

            IQueryable<Pack> rs = db.Packs.Where(r => statusList.Contains(r.Status) && r.ExpirationDate < DateTime.Now.Date);

            foreach (Pack r in rs)
            {
                PackStatusHistory h = PackBLL.Update(db,r, Pack.StatusX.Expired, RedBloodSystem.SODActor, "");

                if (h != null) db.PackStatusHistories.InsertOnSubmit(h);
            }

            LogBLL.Add(db, Task.TaskX.ScanExp);

            db.SubmitChanges();
        }
    }

    //isSOD: isStartOfDate
    public static void CloseOrder(bool isSOD)
    {
        if (!isSOD || !LogBLL.IsLog(Task.TaskX.CloseOrder))
        {
            RedBloodDataContext db = new RedBloodDataContext();

            OrderBLL.CloseOrder(db);

            LogBLL.Add(db, Task.TaskX.CloseOrder);

            db.SubmitChanges();
        }
    }

    //isSOD: isStartOfDate
    public static void LockEnterTestResult(bool isSOD)
    {
        if (!isSOD || !LogBLL.IsLog(Task.TaskX.LockEnterTestResult))
        {
            RedBloodDataContext db = new RedBloodDataContext();

            PackBLL.LockEnterTestResult();

            LogBLL.Add(db, Task.TaskX.LockEnterTestResult);

            db.SubmitChanges();
        }
    }

    public static void Find(HttpResponse Response, TextBox txtCode)
    {
        string key = txtCode.Text.Trim();

        if (key.Length == 0) return;

        string pattern = @"\d+";
        Regex regx = new Regex(pattern);

        if (BarcodeBLL.IsValidPeopleCode(key))
        {
            People r = PeopleBLL.GetByCode(key);
            if (r != null)
            {
                Response.Redirect(SystemBLL.Url4PeopleDetail + "key=" + r.ID.ToString());
            }
        }
        //else if (BarcodeBLL.IsValidPackCode(key))
        //{
        //    Pack r = PackBLL.Get(BarcodeBLL.ParsePackAutoNum(key));
        //    if (r != null)
        //    {
        //        Response.Redirect(SystemBLL.Url4PackDetail + "key=" + r.Autonum.ToString());
        //    }
        //}
        else if (BarcodeBLL.IsValidCampaignCode(key))
        {
            Campaign r = CampaignBLL.GetByID(BarcodeBLL.ParseCampaignID(key));
            if (r != null)
            {
                Response.Redirect(SystemBLL.Url4CampaignDetail + "key=" + r.ID.ToString());
            }
        }
        else if (BarcodeBLL.IsValidOrderCode(key))
        {
            Order r = OrderBLL.Get(BarcodeBLL.ParseOrderID(key));
            if (r != null)
            {
                Response.Redirect(SystemBLL.Url4OrderDetail + "key=" + r.ID.ToString());
            }
        }
        else if (regx.IsMatch(key) && key.Length >= BarcodeBLL.CMNDLength.ToInt())
        {
            People r = PeopleBLL.GetByCMND(key);
            if (r != null)
            {
                Response.Redirect(SystemBLL.Url4PeopleDetail + "key=" + r.ID.ToString());
            }
        }
        //else if (key.length > 1)
        //{
        //    response.redirect(systembll.url4findpeople + "key=" + key);
        //}

        txtCode.Text = "";
    }
}
