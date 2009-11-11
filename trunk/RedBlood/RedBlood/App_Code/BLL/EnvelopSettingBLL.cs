using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EnvelopSettingBLL
/// </summary>
public class EnvelopSettingBLL
{
    public static EnvelopSetting Name { get; set; }
    public static EnvelopSetting Address { get; set; }
    public static EnvelopSetting Geo { get; set; }
    
        

    public EnvelopSettingBLL()
    {
        Reload();
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Reload()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<EnvelopSetting> list = db.EnvelopSettings.ToList();

        Name = list.Where(r => r.Name == "Name").FirstOrDefault();
        Address = list.Where(r => r.Name == "Address").FirstOrDefault();
        Geo = list.Where(r => r.Name == "Geo").FirstOrDefault();
    }
}
