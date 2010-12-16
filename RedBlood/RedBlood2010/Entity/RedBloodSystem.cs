using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RedBlood
{
    /// <summary>
    /// Summary description for RedBloodSystem
    /// </summary>
    public class RedBloodSystem
    {

        public static string Url4CampaignDetail = "~/Collect/Rpt2OrgMenu.aspx?";
        public static string Url4DINDetail = "~/FindAndReport/DINDetail.aspx?";
        public static string Url4PeopleDetail = "~/FindAndReport/PeopleDetail.aspx?";
        public static string Url4Order4CR = "~/Order/Order4CR.aspx?";
        public static string Url4Order4Org = "~/Order/Order4Org.aspx?";

        public static string Url4FindPeople = "~/FindAndReport/FindPeople.aspx?";

        public static string Url4StoreCountList = "~/Store/CountList.aspx?";

        public static string Url4CollectDetailRpt = "~/Collect/CollectDetailRpt.aspx?";
        public static string Url4CollectDetailRpt2 = "~/Collect/CollectDetailRpt2.aspx?";

        public static string Url4CollectRpt11 = "~/Collect/Rpt11.aspx?";
        public static string Url4CollectRpt920 = "~/TestResult/Rpt920.aspx?";

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
                try
                {
                    return System.Web.HttpContext.Current.User.Identity.Name;
                }
                catch (Exception)
                {
                    return "Unknow Actor";
                }

            }
        }

        public static string RootUrl { get; set; }

        public static DateTime RootTime = new DateTime(1900, 1, 1);

        public static List<Infection> CheckingInfection = new List<Infection>() { 
          Infection.HIV_Ab
            
          //The result of HIV_Ag always = HIV_Ab as User requirement
          //, Infection.HIV_Ag
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
}