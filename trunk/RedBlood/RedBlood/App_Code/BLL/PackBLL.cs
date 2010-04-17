using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PackBLL
{
    public PackBLL()
    {
    }

    public static Pack Get4Extract(string DIN, string productCode)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack r = Get(db, DIN, productCode);

        if (r.Donation.TestResultStatus == Donation.TestResultStatusX.Positive)
        {
            throw new Exception(PackErrEnum.Positive.Message);
        }

        return r;
    }

   


    public static Pack Get4Extract(Guid ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack r = Get(db, ID);

        if (r.Donation.TestResultStatus == Donation.TestResultStatusX.Positive)
        {
            throw new Exception(PackErrEnum.Positive.Message);
        }

        return r;
    }

    public static Pack Get(RedBloodDataContext db, Guid ID)
    {

        Pack p = db.Packs.Where(r => r.ID == ID).FirstOrDefault();

        if (p == null)
            throw new Exception("Không tìm thấy túi máu.");

        return p;
    }

    public static bool IsExist(string DIN, string productCode)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var l = db.Packs.Where(r => r.DIN == DIN && r.ProductCode == productCode).ToList();

        if (l.Count > 1)
        {
            throw new Exception("Dữ liệu túi máu bị trùng.");
        }

        return l.Count == 1;
    }
     

    public static Pack Get(string DIN, string productCode)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        return Get(db, DIN, productCode);
    }

    public static Pack Get(RedBloodDataContext db, string DIN, string productCode)
    {
        var l = db.Packs.Where(r => r.DIN == DIN && r.ProductCode == productCode).ToList();

        if (l.Count > 1)
        {
            throw new Exception("Dữ liệu túi máu bị trùng.");
        }

        if (l.Count == 0)
        {
            throw new Exception("Không có túi máu.");
        }

        return l.FirstOrDefault();
    }

    public static Pack Get4ReportSideEffects(string DIN, string productCode)
    {
        Pack p = Get(DIN, productCode);

        if (p.Status != Pack.StatusX.Delivered)
        {
            throw new Exception("Không có cấp phát túi máu.");
        }

        return p;
    }

    public static void Add(string DIN, string productCode, bool isOriginal)
    {
        Product product = ProductBLL.Get(productCode);
        Add(DIN, productCode, product.OriginalVolume, isOriginal);
    }
    public static void Add(string DIN, string productCode, int? volume, bool isOriginal)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Donation d = null;
        
        if (isOriginal)
        {
            d = DonationBLL.Get4CreateOriginal(db, DIN);
        }
        else
        {
            d = DonationBLL.Get(DIN);
        }

        Product product = ProductBLL.Get(productCode);

        if(IsExist(DIN, productCode))
            throw new Exception(PackErrEnum.Existed.Message);


        //TODO: Check to see valid product code in collection
        //Code will be here

        //TODO: Check to see if the pack is collector too late
        //Code check will be here.

        Pack pack = new Pack();

        pack.DIN = DIN;
        pack.ProductCode = productCode;
        pack.Status = Pack.StatusX.Product;
        pack.Actor = RedBloodSystem.CurrentActor;
        //pack.Volume = product.OriginalVolume.HasValue ? product.OriginalVolume : defaultVolume;
        pack.Volume = volume;
        pack.ExpirationDate = DateTime.Now.Add(product.Duration.Value - RedBloodSystem.RootTime);

        db.Packs.InsertOnSubmit(pack);
        db.SubmitChanges();


        PackTransactionBLL.Add(pack.ID, Pack.StatusX.Non, Pack.StatusX.Product,
            isOriginal ? PackTransaction.TypeX.In_Collect : PackTransaction.TypeX.In_Product);

        if (isOriginal)
        {
            DonationBLL.SetOriginalPack(DIN, pack.ID);
        }
    }

    //public static void Add(string DIN, string productCode)
    //{
    //    Pack toPack = new Pack();

    //    toPack.DIN = DIN;
    //    toPack.ProductCode = productCode;
    //    toPack.Status = Pack.StatusX.Product;
    //    toPack.Actor = RedBloodSystem.CurrentActor;
    //    //toPack.Volume = p.OriginalVolume;
    //    toPack.ExpirationDate = DateTime.Now.Add(p.Duration.Value - RedBloodSystem.RootTime);

    //    db.Packs.InsertOnSubmit(toPack);
    //    db.SubmitChanges();

    //    PackTransactionBLL.Add(toPack.ID, PackTransaction.TypeX.In_Product);

    //    //Update fromPack
    //    PackStatusHistory h = PackBLL.Update(db, pack, Pack.StatusX.Produced, "");
    //    if (h != null)
    //    {
    //        db.SubmitChanges();
    //        PackTransactionBLL.Add(pack.ID, PackTransaction.TypeX.Out_Product);
    //    }

    //    return PackErrEnum.Non;
    //}

    public static Pack Get4Order(string DIN, string productCode)
    {
        Pack p = PackBLL.Get(DIN, productCode);

        if (p.Status != Pack.StatusX.Product)
            throw new Exception("Không thể cấp phát. Túi máu: " + p.Status);

        if (p.Donation.TestResultStatus != Donation.TestResultStatusX.Negative)
        {
            throw new Exception("Không thể cấp phát túi máu này. KQ xét nghiệm sàng lọc: " + p.Donation.TestResultStatus);
        }

        return p;
    }

    public static void ChangeStatus(Guid ID, Pack.StatusX toStatus, PackTransaction.TypeX transType, string note)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = Get(db, ID);

        if (p.Status == toStatus)
        {
            throw new Exception("Can not change statuses which are the same.");
        }

        PackTransactionBLL.Add(ID, p.Status, toStatus, transType, note);

        p.Status = toStatus;
        db.SubmitChanges();
    }

    public static void ChangeStatus(Guid ID, Pack.StatusX toStatus, PackTransaction.TypeX transType)
    {
        ChangeStatus(ID, toStatus, transType, MyMethodBase.Current.Caller.Name);
    }
}

