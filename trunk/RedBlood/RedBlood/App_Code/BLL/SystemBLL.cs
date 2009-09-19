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

    public static void EOD()
    {

    }


    private static void BackupPackRemain(bool overwrite)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var v = db.PackRemainDailies.Where(r => r.Date == DateTime.Now.Date);
        if (v.Count() > 0)
        {
            if (overwrite)
            {
                db.PackRemainDailies.DeleteAllOnSubmit(v);
                db.SubmitChanges();

                LogBLL.Add(Task.TaskX.DeleteBackupPackRemain, RedBloodSystem.CurrentActor, "");
            }
            else
            {
                return;
            }
        }

        IQueryable<Pack> rows = db.Packs.Where(r =>
            (r.Status == Pack.StatusX.Product || r.Status == Pack.StatusX.Expired)
            && r.Date == date.Date
            );

        //Insert
        foreach (Pack item in rows)
        {
            PackRemainDaily r = new PackRemainDaily();
            r.PackID = item.ID;
            r.Status = item.Status;
            r.Date = DateTime.Now.Date;

            db.PackRemainDailies.InsertOnSubmit(r);
        }

        db.SubmitChanges();

        LogBLL.Add(Task.TaskX.BackupPackRemain, RedBloodSystem.EODActor, date.ToString());
    }

    private static void CountStore(DateTime date, bool overwrite)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        var v = db.StoreFinalizes.Where(r => r.Date == date.Date);

        if (v.Count() > 0)
        {
            if (overwrite)
            {
                db.StoreFinalizes.DeleteAllOnSubmit(v);
                db.SubmitChanges();

                LogBLL.Add(Task.TaskX.DeleteCountStore, RedBloodSystem.CurrentActor, date.ToString());
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
        LogBLL.Add(Task.TaskX.CountStore, RedBloodSystem.CurrentActor, date.ToString());
    }

    private static void CountStoreRemain(bool overwrite)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        var v = db.StoreFinalizes.Where(r => r.Date == DateTime.Now.Date
            && r.Type == PackTransaction.TypeX.Remain);

        if (v.Count() > 0)
        {
            if (overwrite)
            {
                db.StoreFinalizes.DeleteAllOnSubmit(v);
                db.SubmitChanges();

                LogBLL.Add(Task.TaskX.DeleteCountStoreRemain, RedBloodSystem.CurrentActor, date.ToString());
            }
            else
                return;
        }



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
        LogBLL.Add(Task.TaskX.FinalizeStore, RedBloodSystem.CurrentActor, date.ToString());

    }

    public static string DoFinalizeStore(bool overwriteFinalized, bool finalizeIfEmpty)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        DateTime? lastFinalizeDate = db.StoreFinalizes.Select(r => r.Date).Last();
        DateTime? lastPackTransactionDate = db.PackTransactions.Select(r => r.Date).Last();
        DateTime? lastPackRemainDate = db.PackRemainDailies.Select(r => r.Date).Last();

        //Validation
        //Newer data in DB. Data error or system datetime error
        if ((lastFinalizeDate != null && lastFinalizeDate.Value.Date > DateTime.Now.Date)
            || (lastPackTransactionDate != null && lastPackTransactionDate.Value.Date > DateTime.Now.Date)
            || (lastPackRemainDate != null && lastPackRemainDate.Value.Date > DateTime.Now.Date)
           )
        {
            LogBLL.Add(Task.TaskX.FinalizeStore, RedBloodSystem.EODActor, "DataErr. Newer data in DB.");
            return "DataErr. Newer data in DB.";
        }

        //Finalize last previous day if not yet
        for (DateTime i = lastFinalizeDate.Value.Date.AddDays(1);
            i <= lastPackTransactionDate.Value.Date;
            i = i.AddDays(1))
        {
            //Finaliza
        }

        if (lastFinalizeDate != null && lastFinalizeDate.Value.Date != DateTime.Now.Date)
        {

        }



        //If today has finalized store
        if (db.StoreFinalizes.Where(r => r.Date == DateTime.Now.Date).Count() > 0)
        {
            if (overwriteFinalized)
            {
                //Remove current date data
                db.PackRemainDailies.DeleteAllOnSubmit(db.PackRemainDailies.Where(r => r.Date == DateTime.Now.Date));
                db.StoreFinalizes.DeleteAllOnSubmit(db.StoreFinalizes.Where(r => r.Date == DateTime.Now.Date));

                db.SubmitChanges();
                LogBLL.Add(Task.TaskX.DeleteOldDataForFinalizeStore, RedBloodSystem.CurrentActor, "Remove of date: " + DateTime.Now.ToString());
            }
            else
            {
                return "Already Finalize.";
            }
        }
        else
        {

        }






        if (db.StoreFinalizes.Where(r => r.Date == DateTime.Now.Date).Count() > 0)
        {
            //Remove current date data


            db.SubmitChanges();

            LogBLL.Add(Task.TaskX.FinalizeStore, RedBloodSystem.EODActor, "Remove current date data.");
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
