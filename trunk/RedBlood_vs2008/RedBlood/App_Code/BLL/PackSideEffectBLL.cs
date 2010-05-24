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

    public static List<PackSideEffect> Get(string DIN, string productCode)
    {
        return PackBLL.Get4ReportSideEffects(DIN, productCode).PackSideEffects.ToList();
    }

    public static void Add(string DIN, string productCode, string fullSideEffects, string note)
    {
        Pack p = PackBLL.Get4ReportSideEffects(DIN, productCode);

        if (string.IsNullOrEmpty(fullSideEffects))
            throw new Exception("Phản ứng phụ trống.");

        PackSideEffect se = new PackSideEffect();

        se.PackID = p.ID;
        se.SetSideEffect(fullSideEffects);

        se.Actor = RedBloodSystem.CurrentActor;
        se.Date = DateTime.Now;
        se.Note = note;

        RedBloodDataContext db = new RedBloodDataContext();
        
        db.PackSideEffects.InsertOnSubmit(se);

        db.SubmitChanges();
    }
}
