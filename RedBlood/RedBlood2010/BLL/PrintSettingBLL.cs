using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
namespace RedBlood.BLL
{
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
            public static PrintSetting UCSize { get; set; }
            public static PrintSetting PaperSize { get; set; }
        }

        public class Card
        {
            public static PrintSetting Name { get; set; }
            public static PrintSetting DOB { get; set; }
            public static PrintSetting Autonum { get; set; }
            public static PrintSetting BloodGroup { get; set; }
            public static PrintSetting Address { get; set; }
            public static PrintSetting lbl1 { get; set; }
            public static PrintSetting Date1 { get; set; }
            public static PrintSetting CardSize { get; set; }
            public static PrintSetting PaperSize { get; set; }
        }

        public class DINLabel
        {
            public static PrintSetting ImageDIN { get; set; }
            public static PrintSetting CheckChar { get; set; }

            public static PrintSetting Label1 { get; set; }
            public static PrintSetting Label2 { get; set; }
            public static PrintSetting Label3 { get; set; }
            public static PrintSetting Label4 { get; set; }
            public static PrintSetting Label5 { get; set; }
            public static PrintSetting Label6 { get; set; }
            public static PrintSetting Label7 { get; set; }
            public static PrintSetting Label8 { get; set; }
            public static PrintSetting Label9 { get; set; }
            public static PrintSetting Label10 { get; set; }
            public static PrintSetting PaperSize { get; set; }

        }

        public class ProductLabel
        {
            public static PrintSetting PaperSize { get; set; }
            public static PrintSetting ProductBarcode { get; set; }
            public static PrintSetting ProductDesc { get; set; }
            public static PrintSetting Label1 { get; set; }
            public static PrintSetting Label2 { get; set; }
        }

        public class BloodGroupLabel
        {
            public static PrintSetting PaperSize { get; set; }
            public static PrintSetting Barcode { get; set; }
            public static PrintSetting BloodGroupDesc { get; set; }
            public static PrintSetting Label1 { get; set; }
            public static PrintSetting Label2 { get; set; }
            public static PrintSetting Label3 { get; set; }
        }

        public class DINCert
        {
            public static PrintSetting Province1 { get; set; }
            public static PrintSetting Name { get; set; }
            public static PrintSetting DOBDate { get; set; }
            public static PrintSetting DOBMonth { get; set; }
            public static PrintSetting DOBYear { get; set; }
            public static PrintSetting Address { get; set; }
            public static PrintSetting CMND { get; set; }
            public static PrintSetting Org { get; set; }
            public static PrintSetting Vol250 { get; set; }
            public static PrintSetting Vol350 { get; set; }
            public static PrintSetting Vol450 { get; set; }
            public static PrintSetting Province2 { get; set; }
            public static PrintSetting NowDate { get; set; }
            public static PrintSetting NowMonth { get; set; }
            public static PrintSetting NowYear { get; set; }
            public static PrintSetting PaperSize { get; set; }
            public static PrintSetting CardSize { get; set; }
        }

        public class FinalLabel
        {
            public static PrintSetting DINBarcode { get; set; }
            public static PrintSetting DIN { get; set; }
            public static PrintSetting CheckChar { get; set; }

            public static PrintSetting ABOBarcode { get; set; }
            public static PrintSetting ABOCode { get; set; }
            public static PrintSetting ABOLetter { get; set; }
            public static PrintSetting ABORh { get; set; }

            public static PrintSetting Geo { get; set; }

            public static PrintSetting CollectedDateLabel { get; set; }
            public static PrintSetting CollectedDate { get; set; }

            public static PrintSetting ProductBarcode { get; set; }
            public static PrintSetting ProductCode { get; set; }
            public static PrintSetting ProductDesc { get; set; }
            
            public static PrintSetting VolumeLabel { get; set; }
            public static PrintSetting Volume { get; set; }

            public static PrintSetting ExpiryDateLabel { get; set; }
            public static PrintSetting ExpiryDate { get; set; }

            public static PrintSetting OrgLine1 { get; set; }
            public static PrintSetting OrgLine2 { get; set; }
            public static PrintSetting OrgLine3 { get; set; }
            public static PrintSetting OrgLine4 { get; set; }

            public static PrintSetting FinalLabelSize { get; set; }
            public static PrintSetting PaperSize { get; set; }
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
            LoadData(typeof(Envelope));
            LoadData(typeof(Card));
            LoadData(typeof(DINLabel));
            LoadData(typeof(ProductLabel));
            LoadData(typeof(BloodGroupLabel));
            LoadData(typeof(DINCert));
            LoadData(typeof(FinalLabel));


            //RedBloodDataContext db = new RedBloodDataContext();

            //List<PrintSetting> list = db.PrintSettings.Where(r => r.Type != null).ToList();

            ////Envelope
            //List<PrintSetting> envelope = list.Where(r => r.Type == PrintSetting.TypeX.Envelope).ToList();
            //Envelope.Name = envelope.Where(r => r.Name == "Name").FirstOrDefault();
            //Envelope.Address = envelope.Where(r => r.Name == "Address").FirstOrDefault();
            //Envelope.Geo = envelope.Where(r => r.Name == "Geo").FirstOrDefault();

            ////Card
            //List<PrintSetting> card = list.Where(r => r.Type == PrintSetting.TypeX.Card).ToList();
            //Card.Name = card.Where(r => r.Name == "Name").FirstOrDefault();
            //Card.Autonum = card.Where(r => r.Name == "Autonum").FirstOrDefault();
            //Card.BloodGroup = card.Where(r => r.Name == "BloodGroup").FirstOrDefault();
            //Card.Address = card.Where(r => r.Name == "Address").FirstOrDefault();
            //Card.lbl1 = card.Where(r => r.Name == "lbl1").FirstOrDefault();
            //Card.Date1 = card.Where(r => r.Name == "Date1").FirstOrDefault();

            ////DINLabel
            //List<PrintSetting> DINLableList = list.Where(r => r.Type == PrintSetting.TypeX.DINLabel).ToList();

            //DINLabel.LabelSize = DINLableList.Where(r => r.Name == "LabelSize").FirstOrDefault();
            //DINLabel.ImageDIN = DINLableList.Where(r => r.Name == "ImageDIN").FirstOrDefault();
            //DINLabel.CheckChar = DINLableList.Where(r => r.Name == "CheckChar").FirstOrDefault();

            //DINLabel.Label1 = DINLableList.Where(r => r.Name == "Label1").FirstOrDefault();
            //DINLabel.Label2 = DINLableList.Where(r => r.Name == "Label2").FirstOrDefault();
            //DINLabel.Label3 = DINLableList.Where(r => r.Name == "Label3").FirstOrDefault();
            //DINLabel.Label4 = DINLableList.Where(r => r.Name == "Label4").FirstOrDefault();
            //DINLabel.Label5 = DINLableList.Where(r => r.Name == "Label5").FirstOrDefault();
            //DINLabel.Label6 = DINLableList.Where(r => r.Name == "Label6").FirstOrDefault();
            //DINLabel.Label7 = DINLableList.Where(r => r.Name == "Label7").FirstOrDefault();
            //DINLabel.Label8 = DINLableList.Where(r => r.Name == "Label8").FirstOrDefault();
            //DINLabel.Label9 = DINLableList.Where(r => r.Name == "Label9").FirstOrDefault();
            //DINLabel.Label10 = DINLableList.Where(r => r.Name == "Label10").FirstOrDefault();

            ////Product
            //List<PrintSetting> productLableList = list.Where(r => r.Type == PrintSetting.TypeX.ProductLabel).ToList();

            //ProductLabel.LabelSize = productLableList.Where(r => r.Name == "LabelSize").FirstOrDefault();
            //ProductLabel.Barcode = productLableList.Where(r => r.Name == "ProductBarcode").FirstOrDefault();
            //ProductLabel.Note = productLableList.Where(r => r.Name == "ProductDesc").FirstOrDefault();
            //ProductLabel.Label1 = productLableList.Where(r => r.Name == "Label1").FirstOrDefault();
            //ProductLabel.Label2 = productLableList.Where(r => r.Name == "Label2").FirstOrDefault();

            ////BloodGroup
            //List<PrintSetting> bloodGroupLableList = list.Where(r => r.Type == PrintSetting.TypeX.BloodGroupLabel).ToList();

            //BloodGroupLabel.LabelSize = bloodGroupLableList.Where(r => r.Name == "LabelSize").FirstOrDefault();
            //BloodGroupLabel.Barcode = bloodGroupLableList.Where(r => r.Name == "BloodGroupBardcode").FirstOrDefault();
            //BloodGroupLabel.Note = bloodGroupLableList.Where(r => r.Name == "BloodGroupDesc").FirstOrDefault();
            //BloodGroupLabel.Label1 = bloodGroupLableList.Where(r => r.Name == "Label1").FirstOrDefault();
            //BloodGroupLabel.Label2 = bloodGroupLableList.Where(r => r.Name == "Label2").FirstOrDefault();
            //BloodGroupLabel.Label3 = bloodGroupLableList.Where(r => r.Name == "Label3").FirstOrDefault();

            ////DINCert
            //List<PrintSetting> DINCertList = list.Where(r => r.Type == PrintSetting.TypeX.DINCertificate).ToList();

            //DINCert.Province1 = DINCertList.Where(r => r.Name == "Province1").FirstOrDefault();
            //DINCert.Name = DINCertList.Where(r => r.Name == "Name").FirstOrDefault();
            //DINCert.DOBDate = DINCertList.Where(r => r.Name == "DOBDate").FirstOrDefault();
            //DINCert.DOBMonth = DINCertList.Where(r => r.Name == "DOBMonth").FirstOrDefault();
            //DINCert.DOBYear = DINCertList.Where(r => r.Name == "DOBYear").FirstOrDefault();
            //DINCert.Address = DINCertList.Where(r => r.Name == "Address").FirstOrDefault();
            //DINCert.CMND = DINCertList.Where(r => r.Name == "CMND").FirstOrDefault();
            //DINCert.Org = DINCertList.Where(r => r.Name == "Org").FirstOrDefault();
            //DINCert.Vol250 = DINCertList.Where(r => r.Name == "Vol250").FirstOrDefault();
            //DINCert.Vol350 = DINCertList.Where(r => r.Name == "Vol350").FirstOrDefault();
            //DINCert.Vol450 = DINCertList.Where(r => r.Name == "Vol450").FirstOrDefault();
            //DINCert.Province2 = DINCertList.Where(r => r.Name == "Province2").FirstOrDefault();
            //DINCert.NowDate = DINCertList.Where(r => r.Name == "NowDate").FirstOrDefault();
            //DINCert.NowMonth = DINCertList.Where(r => r.Name == "NowMonth").FirstOrDefault();
            //DINCert.NowYear = DINCertList.Where(r => r.Name == "NowYear").FirstOrDefault();


        }

        static void LoadData(Type type)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            List<PrintSetting> list = db.PrintSettings.Where(r => r.Type == type.Name).ToList();

            var l = type
                .GetMembers(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)
                .Where(r => r is PropertyInfo)
                .Select(r => r as PropertyInfo)
                .Where(r => r.PropertyType == typeof(PrintSetting));

            foreach (var item in l)
            {
                item.SetValue(type, list.Where(r => r.Name == item.Name).FirstOrDefault(), null);
            }
        }
    }
}