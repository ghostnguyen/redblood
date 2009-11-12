using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EnvelopSettingBLL
/// </summary>
public class EnvelopeSettingBLL
{
    public static EnvelopeSetting Name { get; set; }
    public static EnvelopeSetting Address { get; set; }
    public static EnvelopeSetting Geo { get; set; }

    static EnvelopeSettingBLL()
    {
        Reload();
    }

    public EnvelopeSettingBLL()
    {
       
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Reload()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<EnvelopeSetting> list = db.EnvelopeSettings.ToList();

        Name = list.Where(r => r.Name == "Name").FirstOrDefault();
        Address = list.Where(r => r.Name == "Address").FirstOrDefault();
        Geo = list.Where(r => r.Name == "Geo").FirstOrDefault();
    }
}
