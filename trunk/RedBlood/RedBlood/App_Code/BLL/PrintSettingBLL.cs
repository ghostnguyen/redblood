using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EnvelopSettingBLL
/// </summary>
public class PrintSettingBLL
{
    public class Envelope
    {
        public static PrintSetting Name { get; set; }
        public static PrintSetting Address { get; set; }
        public static PrintSetting Geo { get; set; }
    }

    public class Card
    {
        public static PrintSetting Name { get; set; }
        public static PrintSetting Autonum { get; set; }
        public static PrintSetting lbl1 { get; set; }
        public static PrintSetting Date1 { get; set; }
    }





    static PrintSettingBLL()
    {
        Reload();
    }

    public PrintSettingBLL()
    {

        //
        // TODO: Add constructor logic here
        //
    }

    public static void Reload()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<PrintSetting> list = db.PrintSettings.ToList();

        //Envelope
        List<PrintSetting> envelope = list.Where(r => r.Type == PrintSetting.TypeX.Envelope).ToList();
        Envelope.Name = envelope.Where(r => r.Name == "Name").FirstOrDefault();
        Envelope.Address = envelope.Where(r => r.Name == "Address").FirstOrDefault();
        Envelope.Geo = envelope.Where(r => r.Name == "Geo").FirstOrDefault();

        //Card
        List<PrintSetting> card = list.Where(r => r.Type == PrintSetting.TypeX.Card).ToList();
        Card.Name = card.Where(r => r.Name == "Name").FirstOrDefault();
        Card.Autonum = card.Where(r => r.Name == "Autonum").FirstOrDefault();
        Card.lbl1 = card.Where(r => r.Name == "lbl1").FirstOrDefault();
        Card.Date1 = card.Where(r => r.Name == "Date1").FirstOrDefault();
    }
}
