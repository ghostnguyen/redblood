﻿using System;
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
            //RedBloodDataContext db = new RedBloodDataContext();

            //List<Pack.StatusX> statusList = new List<Pack.StatusX> { 
            //    Pack.StatusX.Collected, Pack.StatusX.Product};

            //List<Pack> rs = PackBLL.Get(db, statusList).Where(r => r.DeliverStatus == Pack.DeliverStatusX.Non).ToList();

            //foreach (Pack r in rs)
            //{
            //    PackBLL.ValidateAndUpdateStatus(db, r, RedBloodSystem.SODActor);
            //}

            //LogBLL.Add(db, Task.TaskX.ScanExp);

            //db.SubmitChanges();
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

    public static TimeSpan GetExpire(Pack p)
    {
        //if (p == null || p.Component == null) return TimeSpan.MinValue;


        //if (p.ComponentID == TestDef.Component.Full)
        //{
        //    return new TimeSpan(35, 0, 0, 0);
        //}

        //if (p.ComponentID == TestDef.Component.RBC)
        //{
        //    if (p.SubstanceRoot.ID == TestDef.Substance.for21days)
        //        return new TimeSpan(21, 0, 0, 0);
        //    if (p.SubstanceRoot.ID == TestDef.Substance.for35days)
        //        return new TimeSpan(35, 0, 0, 0);
        //    if (p.SubstanceRoot.ID == TestDef.Substance.for42days)
        //        return new TimeSpan(42, 0, 0, 0);
        //    else
        //        return new TimeSpan(5, 0, 0, 0);
        //}

        //if (p.ComponentID == TestDef.Component.WBC)
        //{
        //    return new TimeSpan(5, 0, 0, 0);
        //}

        //if (p.ComponentID == TestDef.Component.FactorVIII)
        //{
        //    return new TimeSpan(5, 0, 0, 0);
        //}

        //if (p.ComponentID == TestDef.Component.Platelet
        //    || p.ComponentID == TestDef.Component.PlateletApheresis)
        //{
        //    return new TimeSpan(5, 0, 0, 0);
        //}

        //if (p.ComponentID == TestDef.Component.FFPlasma
        //    || p.ComponentID == TestDef.Component.FFPlasma_Poor)
        //{
        //    return new TimeSpan(730, 0, 0, 0);
        //}

        return TimeSpan.MinValue;
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
        //else if (BarcodeBLL.IsValidCampaignCode(key))
        //{
        //    Campaign r = CampaignBLL.GetByID(BarcodeBLL.ParseCampaignID(key));
        //    if (r != null)
        //    {
        //        Response.Redirect(SystemBLL.Url4CampaignDetail + "key=" + r.ID.ToString());
        //    }
        //}
        //else if (BarcodeBLL.IsValidOrderCode(key))
        //{
        //    Order r = OrderBLL.Get(BarcodeBLL.ParseOrderID(key));
        //    if (r != null)
        //    {
        //        Response.Redirect(SystemBLL.Url4OrderDetail + "key=" + r.ID.ToString());
        //    }
        //}
        //else if (regx.IsMatch(key) && key.Length >= Resources.Codabar.CMNDLength.ToInt())
        //{
        //    People r = PeopleBLL.GetByCMND(key);
        //    if (r != null)
        //    {
        //        Response.Redirect(SystemBLL.Url4PeopleDetail + "key=" + r.ID.ToString());
        //    }
        //}
        //else if (key.Length > 1)
        //{
        //    Response.Redirect(SystemBLL.Url4FindPeople + "key=" + key);
        //}

        txtCode.Text = "";
    }
}