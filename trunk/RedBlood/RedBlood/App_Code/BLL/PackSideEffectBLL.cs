using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for PackSideEffectBLL
/// </summary>
public class PackSideEffectBLL
{
    public PackSideEffectBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<PackSideEffect> Get(Guid packID)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return db.PackSideEffects.Where(r => r.PackID == packID).ToList();
    }
}
