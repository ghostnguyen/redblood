﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

            Pack.StatusX[] statusList = new Pack.StatusX[] { 
                Pack.StatusX.Collected, Pack.StatusX.Production};

            List<Pack> rs = PackBLL.Get(db, statusList).Where(r => r.DeliverStatus == Pack.DeliverStatusX.Non).ToList();

            foreach (Pack r in rs)
            {
                PackBLL.ValidateAndUpdateStatus(db, r, SystemActor.SOD);
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

    public static TimeSpan GetExpire(Pack p)
    {
        if (p.Component == null) return TimeSpan.MinValue;

        if (p.SubstanceRoot == null)
        {
            return GetExpire(p.ComponentID.Value, TestDef.Substance.Non);
        }
        else
            return GetExpire(p.ComponentID.Value, p.SubstanceRoot.ID);
        
    }

    public static TimeSpan GetExpire(int componentID, int substanceID)
    {
        if (componentID == TestDef.Component.Full)
        {
            return new TimeSpan(35, 0, 0, 0);
        }

        if (componentID == TestDef.Component.RBC)
        {
            if (substanceID == TestDef.Substance.Yes)
                return new TimeSpan(42, 0, 0, 0);
            else
                return new TimeSpan(5, 0, 0, 0);
        }

        if (componentID == TestDef.Component.WBC)
        {
            return new TimeSpan(5, 0, 0, 0);
        }

        if (componentID == TestDef.Component.FactorVIII)
        {
            return new TimeSpan(5, 0, 0, 0);
        }

        if (componentID == TestDef.Component.Platelet
            || componentID == TestDef.Component.PlateletApheresis)
        {
            return new TimeSpan(5, 0, 0, 0);
        }

        if (componentID == TestDef.Component.FFPlasma
            || componentID == TestDef.Component.FFPlasma_Poor)
        {
            return new TimeSpan(730, 0, 0, 0);
        }

        return TimeSpan.MinValue;
    }
}
