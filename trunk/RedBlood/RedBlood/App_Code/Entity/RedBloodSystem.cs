﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RedBloodSystem
/// </summary>
public class RedBloodSystem
{

    public static string Url4CampaignDetail = "~/FindAndReport/CampaignDetail.aspx?";
    public static string Url4PackDetail = "~/FindAndReport/PackDetail.aspx?";
    public static string Url4PeopleDetail = "~/FindAndReport/PeopleDetail.aspx?";
    public static string Url4OrderDetail = "~/Order/Order.aspx?";
    public static string Url4FindPeople = "~/FindAndReport/FindPeople.aspx?";

    public static string Url4Collect4Rpt_Campaign = "~/Collect/Rpt_Campaign.aspx?";

    public static TimeSpan ExpTime4ProduceFFPlasma = new TimeSpan(0, 18, 0, 0);

    public static string SODActor
    {
        get
        {
            return "SOD";
        }
    }

    public static string EODActor
    {
        get
        {
            return "EOD";
        }
    }

    public static string CurrentActor
    {
        get
        {
            return System.Web.HttpContext.Current.User.Identity.Name;
        }
    }

    public static string RootUrl { get; set; }

    public static DateTime RootTime = new DateTime(1900, 1, 1);

    public static List<Infection> checkingInfection = new List<Infection>() { 
          Infection.HIV_Ab
        , Infection.HIV_Ag
        , Infection.HCV_Ab
        , Infection.HBs_Ag
        , Infection.Syphilis
        , Infection.Malaria
    };

    public RedBloodSystem()
    {
        //
        // TODO: Add constructor logic here
        //
    }

}