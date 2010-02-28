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


    static DateTime? lastFinalizeDate;
    static DateTime? lastPackTransactionDate;
    static DateTime? lastBackupPackRemainDate;

    public static void GetLastTransactionDate()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        lastFinalizeDate = db.StoreFinalizes.OrderByDescending(r => r.Date).Select(r => r.Date).FirstOrDefault();
        lastPackTransactionDate = db.PackTransactions.OrderByDescending(r => r.Date).Select(r => r.Date).FirstOrDefault();
        lastBackupPackRemainDate = db.PackRemainDailies.OrderByDescending(r => r.Date).Select(r => r.Date).FirstOrDefault();
    }

    static RedBloodSystemBLL()
    {
        GetLastTransactionDate();
    }

    public RedBloodSystemBLL()
    {
    }

    public static void EOD()
    {

    }

    public static void SOD()
    {
        DoFinalizeStore(DateTime.Now.Date, false, RedBloodSystem.SODActor);
        ScanExp(true);
        CloseOrder(true);
        LockTestResult(true);

        FacilityBLL.ResetCounting();
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
                PackStatusHistory h = PackBLL.Update(db, r, Pack.StatusX.Expired, RedBloodSystem.SODActor, "");

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
    public static void LockTestResult(bool isSOD)
    {
        if (!isSOD || !LogBLL.IsLog(Task.TaskX.LockEnterTestResult))
        {
            RedBloodDataContext db = new RedBloodDataContext();

            PackBLL.LockEnterTestResult();

            LogBLL.Add(db, Task.TaskX.LockEnterTestResult);

            db.SubmitChanges();
        }
    }

    /// <summary>
    /// if true, count remaining packs directly in store
    /// else count by sum up the remaining of previous date and total transaction in day
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    static bool IsCountDirectly(DateTime date)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        bool isCountDirectly = false;

        //new system, no data
        if (db.PackTransactions.Count() == 0)
            isCountDirectly = true;
        else
        {
            if (lastPackTransactionDate == null) throw new Exception("");
            else
            {
                GetLastTransactionDate();

                //All pack transactions were in the previous of the date.
                if (lastPackTransactionDate.Value.Date <= date.Date)
                    isCountDirectly = true;
            }
        }
        return isCountDirectly;
    }


    static void CountPackTransaction(DateTime date, bool overwrite, string username)
    {
        if (date.Date > DateTime.Now.Date) return;

        RedBloodDataContext db = new RedBloodDataContext();
        var v = db.StoreFinalizes.Where(r => r.Date == date.Date);

        if (v.Count() > 0)
        {
            if (overwrite)
            {
                db.StoreFinalizes.DeleteAllOnSubmit(v);
                db.SubmitChanges();

                LogBLL.Add(Task.TaskX.DeleteCountPackTransaction, username, date.ToString());
            }
            else
                return;
        }

        var trans = from r in db.PackTransactions
                    where r.Date.Value.Date == date.Date
                    group r by r.Type into rs
                    select new { rs.Key, Count = rs.Count() };

        foreach (PackTransaction.TypeX item in Enum.GetValues(typeof(PackTransaction.TypeX)))
        {
            if (item != PackTransaction.TypeX.Remain)
            {
                //Insert 
                StoreFinalize r = new StoreFinalize();
                r.Date = date;
                r.Type = item;

                int? count = trans.Where(rs => rs.Key == item).Select(rs => rs.Count).FirstOrDefault();
                r.Count = count != null ? count.Value : 0;

                db.StoreFinalizes.InsertOnSubmit(r);
            }
        }

        db.SubmitChanges();
        LogBLL.Add(Task.TaskX.CountPackTransaction, username, date.ToString());
    }

    static void CountPackRemain(DateTime date, bool overwrite, string username)
    {
        if (date.Date > DateTime.Now.Date) return;

        RedBloodDataContext db = new RedBloodDataContext();

        if (IsCountDirectly(date))
        {
            if (date.Date > DateTime.Now.Date) return;

            var v = db.StoreFinalizes.Where(r => r.Date == date.Date
                && r.Type == PackTransaction.TypeX.Remain);

            if (v.Count() > 0)
            {
                if (overwrite)
                {
                    db.StoreFinalizes.DeleteAllOnSubmit(v);
                    db.SubmitChanges();

                    LogBLL.Add(Task.TaskX.DeleteCountPackRemain, username, date.ToString());
                }
                else
                    return;
            }

            IQueryable<Pack> rows = db.Packs.Where(r =>
                (r.Status == Pack.StatusX.Product || r.Status == Pack.StatusX.Expired)
                );

            //Insert 
            StoreFinalize s = new StoreFinalize();
            s.Date = date;
            s.Type = PackTransaction.TypeX.Remain;
            s.Count = rows.Count();

            db.StoreFinalizes.InsertOnSubmit(s);

            db.SubmitChanges();

            LogBLL.Add(Task.TaskX.CountPackRemain, username, date.ToString());
        }
        else
        {
            DateTime previousDate = date.Date.AddDays(-1);

            var pv = db.StoreFinalizes.Where(r => r.Date == previousDate.Date
                && r.Type == PackTransaction.TypeX.Remain);

            //if the previous day have NO data. Exit code.
            if (pv.Count() != 1 || pv.FirstOrDefault().Count == null) return;

            //check the date has data
            var v = db.StoreFinalizes.Where(r => r.Date == date.Date
                && r.Type == PackTransaction.TypeX.Remain);

            if (v.Count() > 0)
            {
                if (overwrite)
                {
                    db.StoreFinalizes.DeleteAllOnSubmit(v);
                    db.SubmitChanges();

                    LogBLL.Add(Task.TaskX.DeleteCountPackRemain, username, date.ToString());
                }
                else
                    return;
            }

            //Count remaining based on IN and OUT in the date and the remaining of the previous date.
            int remaingOfPreDate = pv.FirstOrDefault().Count.Value;
            var v1 = db.StoreFinalizes.Where(r => r.Date == date.Date);

            foreach (StoreFinalize item in v1)
            {
                if (item.Type > 0)
                {
                    remaingOfPreDate += item.Count == null ? 0 : item.Count.Value;
                }
                else if (item.Type < 0)
                {
                    remaingOfPreDate -= item.Count == null ? 0 : item.Count.Value;
                }
            }

            //Insert 
            StoreFinalize s = new StoreFinalize();
            s.Date = date;
            s.Type = PackTransaction.TypeX.Remain;
            s.Count = remaingOfPreDate;

            db.StoreFinalizes.InsertOnSubmit(s);

            db.SubmitChanges();

            LogBLL.Add(Task.TaskX.CountPackRemain, username, date.ToString());
        }
    }

    static void BackupPackRemain(DateTime date, bool overwrite, string username)
    {
        if (date.Date > DateTime.Now.Date) return;

        RedBloodDataContext db = new RedBloodDataContext();

        if (IsCountDirectly(date)
            //Have pack transaction. Remain transaction is NOT real transaction
            && db.PackTransactions.Where(r => r.Date.Value.Date == date.Date
                && r.Type != PackTransaction.TypeX.Remain).Count() != 0) { }
        else return;

        var v = db.PackRemainDailies.Where(r => r.Date == date);

        if (v.Count() > 0)
        {
            if (overwrite)
            {
                db.PackRemainDailies.DeleteAllOnSubmit(v);
                db.SubmitChanges();

                LogBLL.Add(Task.TaskX.DeleteBackupPackRemain, username, "");
            }
            else
            {
                return;
            }
        }

        IQueryable<Pack> rows = db.Packs.Where(r =>
            (r.Status == Pack.StatusX.Product || r.Status == Pack.StatusX.Expired)
            //&& r.Date == DateTime.Now.Date
            );

        //Insert
        foreach (Pack item in rows)
        {
            PackRemainDaily r = new PackRemainDaily();
            r.PackID = item.ID;
            r.Status = item.Status;
            r.Date = date;
            r.Note = date.Date == DateTime.Now.Date ? "" : DateTime.Now.Date.ToString();

            db.PackRemainDailies.InsertOnSubmit(r);
        }

        db.SubmitChanges();

        LogBLL.Add(Task.TaskX.BackupPackRemain, username, "");
    }

    public static string DoFinalizeStore(DateTime date, bool overwrite, string username)
    {
        if (date.Date > DateTime.Now.Date) return "Can not finalize day is in future.";

        RedBloodDataContext db = new RedBloodDataContext();

        GetLastTransactionDate();

        //Validation
        //Newer data in DB. Data error or system datetime error
        if (
            (lastFinalizeDate != null && lastFinalizeDate.Value.Date > DateTime.Now.Date)
            || (lastPackTransactionDate != null && lastPackTransactionDate.Value.Date > DateTime.Now.Date)
            || (lastBackupPackRemainDate != null && lastBackupPackRemainDate.Value.Date > DateTime.Now.Date)
           )
        {
            string err = "DataErr. Newer data in DB.";
            LogBLL.Add(Task.TaskX.DoFinalizeStore, username, err);
            return err;
        }

        if (lastFinalizeDate != null)
        {
            //Finalize all previous days if not yet
            for (DateTime i = lastFinalizeDate.Value.Date.AddDays(1);
                i < date.Date;
                i = i.AddDays(1))
            {
                CountPackTransaction(i, false, username);
                CountPackRemain(i, false, username);
                BackupPackRemain(i, false, username);
            }
        }

        //Finalize the date.
        CountPackTransaction(date, overwrite, username);
        CountPackRemain(date, overwrite, username);
        BackupPackRemain(date, overwrite, username);

        LogBLL.Add(Task.TaskX.DoFinalizeStore, username, date.ToString());

        return "";
    }

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
            Campaign r = CampaignBLL.GetByID(BarcodeBLL.ParseCampaignID(key));
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
