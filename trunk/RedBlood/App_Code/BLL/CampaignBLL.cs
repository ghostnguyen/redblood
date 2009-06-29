using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CampaignBLL
/// </summary>
public class CampaignBLL
{
    public CampaignBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Campaign GetByID(int ID, out RedBloodDataContext db)
    {
        db = new RedBloodDataContext();

        if (ID == 0) return null;

        return (from c in db.Campaigns
                where c.ID == ID
                select c).FirstOrDefault();
    }

    public static Campaign GetByID(int ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return GetByID(ID, out db);
    }

    public bool IsExistNameInSameDate(string name, int ID, DateTime dt)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        int count = (from r in db.Campaigns
                     where r.ID != ID && r.Name.Trim() == name.Trim() && r.Date == dt
                     select r).Count();

        if (count == 0) return false;
        else return true;
    }

    public string Delete(int ID)
    {
        RedBloodDataContext db;
        Campaign e = GetByID(ID, out db);

        if (e != null)
        {
            try
            {
                db.Campaigns.DeleteOnSubmit(e);
                db.SubmitChanges();
                return "Xóa thành công.";
            }
            catch (Exception)
            {
                throw new Exception("Xóa bị lỗi.");
            }
        }
        return "Không tìm thấy.";
    }

    //public object GetTSIn(bool isLongRun)
    //{
    //    RedBloodDataContext db = new RedBloodDataContext();

    //    if (isLongRun)
    //    {
    //        var rs = from r in db.Campaigns
    //                 let total = (r.Packs.Count(p => p.Status == Pack.StatusX.CommitReceived))
    //                 where total > 0 && r.Type == Campaign.TypeX.Long_run
    //                 select new { r.ID, r.Name, SourceName = r.Source.Name, r.Note, total };
    //        return rs;
    //    }
    //    else
    //    {
    //        var rs = from r in db.Campaigns
    //                 let total = (r.Packs.Count(p => p.Status == Pack.StatusX.CommitReceived))
    //                 where r.Type == Campaign.TypeX.Short_run && r.Status == Campaign.StatusX.Assign
    //                 select new { r.ID, r.Name, SourceName = r.Source.Name, r.Date, r.Note, total };
    //        return rs;
    //    }
    //}

    public void New(Campaign e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        if (e.Type == Campaign.TypeX.Long_run)
            e.Status = Campaign.StatusX.Assign;

        if (e.Type == Campaign.TypeX.Short_run)
            e.Status = Campaign.StatusX.Init;

        db.Campaigns.InsertOnSubmit(e);
        db.SubmitChanges();
    }

    public static void SetStatus(int ID)
    {
        RedBloodDataContext db;

        Campaign e = GetByID(ID, out db);

        if (e == null) return;

        if (e.Type == Campaign.TypeX.Short_run)
        {
            if (e.Status == Campaign.StatusX.Init && e.Packs.Count != 0)
                e.Status = Campaign.StatusX.Assign;

            if (e.Status == Campaign.StatusX.Assign && e.Packs.Count == 0)
                e.Status = Campaign.StatusX.Init;

            db.SubmitChanges();
        }
    }

}
