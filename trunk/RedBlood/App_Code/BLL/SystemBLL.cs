using System;
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
    }

    //isSOD: isStartOfDate
    public static void ScanExp(bool isSOD)
    {
        if (!isSOD || !LogBLL.IsLog(Task.TaskX.ScanExp))
        {
            RedBloodDataContext db = new RedBloodDataContext();

            Pack.StatusX[] statusList = new Pack.StatusX[] { 
                Pack.StatusX.Init, Pack.StatusX.Collected, Pack.StatusX.Production};

            List<Pack> rs = PackBLL.Get(db,statusList);
            
            foreach (Pack r in rs)
            {
                PackBLL.ValidateAndChangeStatus(db, r, SystemActor.SOD);                
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
}
