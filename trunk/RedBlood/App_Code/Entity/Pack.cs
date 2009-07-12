using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
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
/// Summary description for Geo
/// </summary>

public partial class Pack
{
    public enum StatusX : int
    {
        Non = -2,
        All = -1,
        Init = 0,
        Đã_thu = 1,
        Collected = 1,
        //CommitReceived = 2,
        Delete = 4,
        Hủy = 4,

        //EnterTestResult = 7,
        //Đang_nhập_KQ = 7,
        //CommitTestResult = 8,
        //Đã_nhập = 8,

        //Đã_cấp_phát = 9,
        //Delivered = 9,

        Production = 10,
        Thành_phẩm = 10,

        Produced = 11,
        Đã_sản_xuất = 11,


        Expire = 40,
        Quá_hạn = 40,
        //ExpireEnter = 41,
        //ExpireCommitReceived = 42,
        DataErr = 49
    }

    public enum TestResultStatusX : int
    {
        Chưa_có = 0,
        Non = 0,

        Âm_tính = 1,
        Negative = 1,

        Dương_tính = 2,
        Positive = 2,

        Âm_tính_K = 3,
        NegativeLocked = 3,

        Dương_tính_K = 4,
        PositiveLocked = 4
    }

    public enum DeliverStatusX : int
    {
        Non = 0,
        Yes = 1
    }


    PackBLL bll = new PackBLL();
    CampaignBLL campaignBLL = new CampaignBLL();
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert)
        {
            if (Status == StatusX.Init)
            {
                //TestResultStatus = Pack.TestResultStatusX.Non;
                //DeliverStatus = Pack.DeliverStatusX.Non;
            }
        }
    }

    partial void OnLoaded()
    {
    }

    partial void OnPeopleIDChanging(Guid? value)
    {
        //Remove PeopleID
        if (value == null)
        {
            if (Status == StatusX.Init || Status == StatusX.Collected)
            { }
            else
            {
                throw new Exception("Không thể đổi người cho máu.");
            }
        }
        else
        {
            if (PeopleID != null)
                throw new Exception("Túi máu đã có người.");

            Pack p = PackBLL.GetEnterPackByPeopleID(value.Value);
            if (p != null)
                throw new Exception("Người này có túi máu chưa xử lý.");
        }
    }

    partial void OnCampaignIDChanging(int? value)
    {
        if (value == null) return;

        Campaign c = CampaignBLL.GetByID(value.Value);

        if (c.Type == Campaign.TypeX.Short_run)
        {
            if (c.Status == Campaign.StatusX.Init
                || c.Status == Campaign.StatusX.Assign)
            { }
            else
                throw new Exception("Đợt thu máu kết thúc.");
        }
    }



    //public TestResult TestResult2
    //{
    //    get { return TestResults.Where(r => r.Times == 2).FirstOrDefault(); }
    //}

    //public BloodType BloodType2
    //{
    //    get
    //    {
    //        return BloodTypes.Where(r => r.Times == 2).FirstOrDefault();
    //    }
    //}

    public bool CanUpdateTestResult
    {
        get
        {
            return ComponentID != null
                && ComponentID == TestDef.Component.Full
                && PackBLL.AllowEnterTestResult().Contains(TestResultStatus);
        }
    }

    public string Code
    {
        get { return CodabarBLL.GenPackCode(Autonum); }
    }

    public string DeleteNote
    {
        get
        {
            PackStatusHistory e = PackStatusHistories.Where(h => h.ToStatus == Pack.StatusX.Delete).FirstOrDefault();
            if (e == null) return "";
            else return e.Note;
        }
    }

    public List<Pack> SourcePacks
    {
        get
        {
            return PackExtractsByExtract.Select(r => r.SourcePack).ToList();
        }
    }

    public List<Pack> ExtractedPacks
    {
        get
        {
            return PackExtractsBySource.Select(r => r.ExtractPack).ToList();
        }
    }

    public Pack.TestResultStatusX TestResultStatusRoot
    {
        get
        {
            //Get all packs related pack and each has componet is full
            List<TestResultStatusX> l = PackBLL.GetSourcePacks_AllLevel(this)
                                            .Where(r => r.ComponentID == TestDef.Component.Full)
                                            .Select(r => r.TestResultStatus)
                                            .ToList();

            foreach (TestResultStatusX item in l)
            {
                if (item == TestResultStatusX.Positive
                    || item == TestResultStatusX.PositiveLocked)
                    return item;
            }

            foreach (TestResultStatusX item in l)
            {
                if (item == TestResultStatusX.Non)
                    return item;
            }

            foreach (TestResultStatusX item in l)
            {
                if (item == TestResultStatusX.Negative
                    || item == TestResultStatusX.NegativeLocked)
                    return item;
            }

            return TestResultStatus;
        }
    }

    public TestDef SubstanceRoot
    {
        get
        {
            //Get all packs related pack and each has componet is full
            List<TestDef> l = PackBLL.GetSourcePacks_AllLevel(this)
                                            .Where(r => r.ComponentID == TestDef.Component.Full)
                                            .Select(r => r.Substance)
                                            .ToList();

            foreach (TestDef item in l)
            {
                if (item.ID == TestDef.Substance.Yes)
                    return item;
            }

            foreach (TestDef item in l)
            {
                if (item.ID == TestDef.Substance.Non)
                    return item;
            }

            return Substance;
        }
    }

    public List<TestDef> NonNegativeTestResult()
    {

        List<TestDef> r = new List<TestDef>();

        //if (TestResultStatus == TestResultStatusX.Non)
        //    throw new Exception("Chưa nhập kết quả túi máu.");

        if (HIVID == TestDef.HIV.Pos || HIVID == TestDef.HIV.NA)
            r.Add(HIV);

        if (HBsAgID == TestDef.HBsAg.Pos || HBsAgID == TestDef.HBsAg.NA)
            r.Add(HBsAg);

        if (HCVID == TestDef.HCV.Pos || HCVID == TestDef.HCV.NA)
            r.Add(HCV);

        if (SyphilisID == TestDef.Syphilis.Pos || SyphilisID == TestDef.Syphilis.NA)
            r.Add(Syphilis);

        if (MalariaID == TestDef.Malaria.Pos || MalariaID == TestDef.Malaria.NA)
            r.Add(Malaria);

        return r;

    }

    public PackErr Err { get; set; }
    public List<int> CanExtractTo { get; set; }
    public List<Pack> RelatedPack
    {
        get
        {
            return PackBLL.GetRelatedPacks_AllLevel(this);
        }
    }

}
