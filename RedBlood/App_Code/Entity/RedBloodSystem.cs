using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RedBloodSystem
/// </summary>
public class RedBloodSystem
{
    public static string SODActor
    {
        get
        {
            return "SOD";
        }
    }

    public static string CurrentActor
    {
        get
        {
            return System.Web.HttpContext.Current.User.Identity.Name;
        }
    }


    public RedBloodSystem()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
