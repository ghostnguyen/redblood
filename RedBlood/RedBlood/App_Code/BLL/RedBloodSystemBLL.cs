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
public class RedBloodSystemBLL
{
    static DateTime? firstFinalizeDate;
    static DateTime? lastFinalizeDate;
    static DateTime? lastBackupPackRemainDate;
    static DateTime? lastPackTransactionDate;
    static DateTime? firstPackTransactionDate;

    public RedBloodSystemBLL()
    {
    }

    static bool GetAndValidateFinalizeData()
    {
        GetLastFinalizeDate();

        if (
            !lastFinalizeDate.HasValue
            && !lastPackTransactionDate.HasValue
            && !lastBackupPackRemainDate.HasValue
            )
            return true;

        if (
            lastFinalizeDate.HasValue && lastFinalizeDate.Value.Date > DateTime.Now.Date
            || (lastPackTransactionDate.HasValue && lastPackTransactionDate.Value.Date > DateTime.Now.Date)
            || (lastBackupPackRemainDate.HasValue && lastBackupPackRemainDate.Value.Date > DateTime.Now.Date)
            )
        {
            LogBLL.LogsFailAndThrow("Error. Data or Date in future.");
        }

        if (lastFinalizeDate < lastBackupPackRemainDate)
        {
            LogBLL.LogsFailAndThrow("Error. lastFinalizeDate != lastBackupPackRemainDate");
        }

        if (lastFinalizeDate.HasValue
            && !lastPackTransactionDate.HasValue
            )
        {
            if (lastFinalizeDate == firstFinalizeDate)
            { }
            else
            {
                LogBLL.LogsFailAndThrow("Error. lastFinalizeDate.HasValue && !lastPackTransactionDate.HasValue");
            }
        }

        if (lastPackTransactionDate.HasValue)
        {
            if (lastFinalizeDate.HasValue)
            {
                int days = (lastPackTransactionDate.Value.Date - lastFinalizeDate.Value.Date).Days;

                if (days == 0 || days == 1)
                { }
                else
                {
                    //LogBLL.LogsFailAndThrow("Error. days == 0 || days == 1 fail.");
                }
            }
            else
            {
                if (firstPackTransactionDate != lastPackTransactionDate)
                {
                    LogBLL.LogsFailAndThrow("firstPackTransactionDate != lastPackTransactionDate");
                }
            }
        }

        return true;
    }

    public static void EOD()
    {

    }

    public static void SOD()
    {
        if (!LogBLL.IsLogged())
        {
            FinalizeStoreInPast();
            OrderBLL.CloseOrder();
            FacilityBLL.ResetCounting();

            LogBLL.Logs(RedBloodSystem.SODActor);
        }
    }

    public static void FinalizeStoreInPast()
    {
        GetAndValidateFinalizeData();

        DateTime yesterday = DateTime.Now.Date.AddDays(-1);
        DateTime startDate = lastFinalizeDate.HasValue ? lastFinalizeDate.Value.Date.AddDays(1) : yesterday;

        for (DateTime i = startDate; i < DateTime.Now.Date; i = i.Date.AddDays(1))
        {
            StoreFinalizeBLL.FinalizeStore(i, false);
        }

        LogBLL.Logs();
    }

    public static void GetLastFinalizeDate()
    {
        GetLastFinalizeDate(out firstFinalizeDate, out lastFinalizeDate, out lastPackTransactionDate, out firstPackTransactionDate, out lastBackupPackRemainDate);
    }

    public static void GetLastFinalizeDate(out DateTime? firstFinalizeDate, out DateTime? lastFinalizeDate, out DateTime? lastPackTransactionDate, out DateTime? firstPackTransactionDate, out DateTime? lastBackupPackRemainDate)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        firstFinalizeDate = db.StoreFinalizes.Min(r => r.Date);
        lastFinalizeDate = db.StoreFinalizes.Max(r => r.Date);
        lastPackTransactionDate = db.PackTransactions.Max(r => r.Date);
        firstPackTransactionDate = db.PackTransactions.Min(r => r.Date);
        lastBackupPackRemainDate = db.PackRemainDailies.Max(r => r.Date);
    }

    //isSOD: isStartOfDate
    //public static void ScanExp(bool isSOD)
    //{
    //if (!isSOD || !LogBLL.IsLog(Task.TaskX.ScanExp))
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    List<Pack.StatusX> statusList = new List<Pack.StatusX> { Pack.StatusX.Product };

    //    IQueryable<Pack> rs = db.Packs.Where(r => statusList.Contains(r.Status) && r.ExpirationDate < DateTime.Now.Date);

    //    foreach (Pack r in rs)
    //    {
    //        PackStatusHistory h = PackBLL.Update(db, r, Pack.StatusX.Expired, RedBloodSystem.SODActor, "");

    //        if (h != null) db.PackStatusHistories.InsertOnSubmit(h);
    //    }

    //    db.SubmitChanges();

    //    LogBLL.Add(Task.TaskX.ScanExp);
    //}
    //}

    //isSOD: isStartOfDate
    //public static void CloseOrder(bool isSOD)
    //{
    //    //if (!isSOD || !LogBLL.IsLog(Task.TaskX.CloseOrder))
    //    //{
    //    //    RedBloodDataContext db = new RedBloodDataContext();

    //    //    OrderBLL.CloseOrder(db);

    //    //    db.SubmitChanges();

    //    //    LogBLL.Add(Task.TaskX.CloseOrder);
    //    //}
    //}

    //isSOD: isStartOfDate
    //public static void LockTestResult()
    //{
    //if (!isSOD || !LogBLL.IsLog(Task.TaskX.LockEnterTestResult))
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    PackBLL.LockEnterTestResult();

    //    db.SubmitChanges();

    //    LogBLL.Add(Task.TaskX.LockEnterTestResult);
    //}
    //}

    /// <summary>
    /// if true, count remaining packs directly in store
    /// else count by sum up the remaining of previous date and total transaction in day
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    //static bool IsCountDirectly(DateTime date)
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    bool isCountDirectly = false;

    //    //new system, no data
    //    if (db.PackTransactions.Count() == 0)
    //        isCountDirectly = true;
    //    else
    //    {
    //        if (lastPackTransactionDate == null) throw new Exception("");
    //        else
    //        {
    //            GetLastTransactionDate();

    //            //All pack transactions were in the previous of the date.
    //            if (lastPackTransactionDate.Value.Date <= date.Date)
    //                isCountDirectly = true;
    //        }
    //    }
    //    return isCountDirectly;
    //}

    public static void Find(HttpResponse Response, TextBox txtCode)
    {
        if (txtCode == null) return;

        string key = txtCode.Text.Trim();

        if (key.Length == 0) return;

        string pattern = @"\d+";
        Regex regx = new Regex(pattern);

        if (BarcodeBLL.IsValidPeopleCode(key))
        {
            People r = PeopleBLL.GetByCode(key);
            if (r != null)
            {
                Response.Redirect(RedBloodSystem.Url4PeopleDetail + "key=" + r.ID.ToString());
            }
        }
        else if (BarcodeBLL.IsValidDINCode(key))
        {
            Response.Redirect(RedBloodSystem.Url4PackDetail + "key=" + BarcodeBLL.ParseDIN(key));
        }
        else if (BarcodeBLL.IsValidCampaignCode(key))
        {
            Campaign r = CampaignBLL.Get(BarcodeBLL.ParseCampaignID(key));
            if (r != null)
            {
                Response.Redirect(RedBloodSystem.Url4CampaignDetail + "key=" + r.ID.ToString());
            }
        }
        else if (BarcodeBLL.IsValidOrderCode(key))
        {
            Order r = OrderBLL.Get(BarcodeBLL.ParseOrderID(key));
            if (r != null)
            {
                Response.Redirect(RedBloodSystem.Url4Order4CR + "key=" + r.ID.ToString());
            }
        }
        //TODO: Search by name
        else if (key.Length > 3 && key.Substring(0, 3) == "/n:")
        {
            Response.Redirect(RedBloodSystem.Url4FindPeople + "key=" + key.Substring(3).Trim());
        }
        else if (regx.IsMatch(key) && key.Length >= BarcodeBLL.CMNDLength.ToInt())
        {
            People r = PeopleBLL.GetByCMND(key);
            if (r != null)
            {
                Response.Redirect(RedBloodSystem.Url4PeopleDetail + "key=" + r.ID.ToString());
            }
        }

        txtCode.Text = "";
    }
}
