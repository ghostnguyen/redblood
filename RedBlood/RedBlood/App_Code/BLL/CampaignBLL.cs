using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

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

    public static Campaign Get(int ID, RedBloodDataContext db)
    {
        Campaign c = (from e in db.Campaigns
                      where e.ID == ID
                      select e).FirstOrDefault();

        if (c == null)
            throw new Exception("Không tìm thấy đợt thu.");

        return c;
    }

    public static Campaign Get(int ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return Get(ID, db);
    }

    public static IQueryable<Campaign> Get(List<Guid> provinceIDList, DateTime? from, DateTime? to, Campaign.TypeX type)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        if (provinceIDList == null || provinceIDList.Count == 0) return null;

        return db.Campaigns.Where(r => provinceIDList.Contains(r.CoopOrg.GeoID1.Value)
            && r.Type == type
            && r.Date != null
            && (from == null || r.Date.Value.Date >= from.Value.Date)
            && (to == null || r.Date.Value.Date <= to.Value.Date)
            );
    }

    static public bool IsExistNameInSameDate(string name, int ID, DateTime dt)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        int count = (from r in db.Campaigns
                     where r.ID != ID && r.Name.Trim() == name.Trim() && r.Date == dt
                     select r).Count();

        if (count == 0) return false;
        else return true;
    }

    static public void Delete(int ID)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Campaign e = Get(ID, db);

        db.Campaigns.DeleteOnSubmit(e);
        db.SubmitChanges();
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

    static public void New(Campaign e)
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
        RedBloodDataContext db = new RedBloodDataContext();

        Campaign e = Get(ID, db);

        if (e == null) return;

        if (e.Type == Campaign.TypeX.Short_run)
        {
            //if (e.Status == Campaign.StatusX.Init && e.Packs.Count != 0)
            //    e.Status = Campaign.StatusX.Assign;

            //if (e.Status == Campaign.StatusX.Assign && e.Packs.Count == 0)
            //    e.Status = Campaign.StatusX.Init;

            db.SubmitChanges();
        }
    }

}
