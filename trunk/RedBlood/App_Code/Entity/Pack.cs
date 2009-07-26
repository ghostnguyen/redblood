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
        //Init = 0,
        
        //Đã_thu = 1,
        //Collected = 1,

        //CommitReceived = 2,
        Delete = 4,
        Hủy = 4,

        //EnterTestResult = 7,
        //Đang_nhập_KQ = 7,
        //CommitTestResult = 8,
        //Đã_nhập = 8,

        Đã_cấp_phát = 9,
        Delivered = 9,

        Product = 10,
        Thành_phẩm = 10,

        Produced = 11,
        Đã_sản_xuất = 11,


        Expired = 40,
        Quá_hạn = 40,
        //ExpireEnter = 41,
        //ExpireCommitReceived = 42,
        DataErr = 49
    }

    


    
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        //if (action == System.Data.Linq.ChangeAction.Insert)
        //{
        //    if (Status == StatusX.Init)
        //    {
        //        //TestResultStatus = Pack.TestResultStatusX.Non;
        //        //DeliverStatus = Pack.DeliverStatusX.Non;
        //    }
        //}
    }

    partial void OnLoaded()
    {
    }

    //partial void OnPeopleIDChanging(Guid? value)
    //{
    //    //Remove PeopleID
    //    //if (value == null)
    //    //{
    //    //    if (Status == StatusX.Init || Status == StatusX.Collected)
    //    //    { }
    //    //    else
    //    //    {
    //    //        throw new Exception("Không thể đổi người cho máu.");
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    if (PeopleID != null)
    //    //        throw new Exception("Túi máu đã có người.");

    //    //    Pack p = PackBLL.GetEnterPackByPeopleID(value.Value);
    //    //    if (p != null)
    //    //        throw new Exception("Người này có túi máu chưa xử lý.");
    //    //}
    //}

    //partial void OnCampaignIDChanging(int? value)
    //{
    //    if (value == null) return;

    //    Campaign c = CampaignBLL.GetByID(value.Value);

    //    if (c.Type == Campaign.TypeX.Short_run)
    //    {
    //        if (c.Status == Campaign.StatusX.Init
    //            || c.Status == Campaign.StatusX.Assign)
    //        { }
    //        else
    //            throw new Exception("Đợt thu máu kết thúc.");
    //    }
    //}



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
            //return ComponentID != null
            //    && ComponentID == TestDef.Component.Full
            //    && PackBLL.AllowEnterTestResult().Contains(TestResultStatus);
            return false;
        }
    }

    public string Code
    {
        get {
            return null;
        
//            return BarcodeBLL.GenPackCode(Autonum); 
        }
    }

    public string DeleteNote
    {
        get
        {
            //PackStatusHistory e = PackStatusHistories.Where(h => h.ToStatus == Pack.StatusX.Delete).FirstOrDefault();
            //if (e == null) return "";
            //else return e.Note;
            return null;

        }
    }

    /// <summary>
    /// Get all source pack at level 1
    /// </summary>
    public List<Pack> SourcePacks
    {
        get
        {
            return null;

//            return PackExtractsByExtract.Select(r => r.SourcePack).ToList();
        }
    }

    /// <summary>
    /// Get all source pack at all level 
    /// </summary>
    public List<Pack> SourcePacks_All
    {
        get
        {
            List<Pack> l = SourcePacks;

            Pack[] temp = new Pack[l.Count];
            l.CopyTo(temp);

            foreach (Pack item in temp)
            {
                l.Add(item);
                l.AddRange(item.SourcePacks_All);
            }
            return l.Distinct().ToList();
        }
    }

    public List<Pack> ExtractedPacks
    {
        get
        {
            //return PackExtractsBySource.Select(r => r.ExtractPack).ToList();
            return null;

        }
    }

    public List<Pack> ExtractedPacks_All
    {
        get
        {
            List<Pack> l = ExtractedPacks;

            Pack[] temp = new Pack[l.Count];
            l.CopyTo(temp);

            foreach (Pack item in temp)
            {
                l.Add(item);
                l.AddRange(item.ExtractedPacks_All);
            }

            return l.Distinct().ToList();
        }
    }

    public List<Pack> RelatedPack
    {
        get
        {
            List<Pack> l = new List<Pack>();
            l.AddRange(SourcePacks_All);
            l.AddRange(ExtractedPacks_All);
            return l.Distinct().ToList();
        }
    }

    //public Pack.TestResultStatusX TestResultStatusRoot
    //{
    //    get
    //    {
    //        //Get all packs related pack and each has componet is full
    //        //List<TestResultStatusX> l = SourcePacks_All
    //        //                                .Where(r => r.ComponentID == TestDef.Component.Full)
    //        //                                .Select(r => r.TestResultStatus)
    //        //                                .ToList();

    //        //foreach (TestResultStatusX item in l)
    //        //{
    //        //    if (item == TestResultStatusX.Positive
    //        //        || item == TestResultStatusX.PositiveLocked)
    //        //        return item;
    //        //}

    //        //foreach (TestResultStatusX item in l)
    //        //{
    //        //    if (item == TestResultStatusX.Non)
    //        //        return item;
    //        //}

    //        //foreach (TestResultStatusX item in l)
    //        //{
    //        //    if (item == TestResultStatusX.Negative
    //        //        || item == TestResultStatusX.NegativeLocked)
    //        //        return item;
    //        //}

    //        //return TestResultStatus;

    //    }
    //}

    public TestDef SubstanceRoot
    {
        get
        {
            //Get all packs related pack and each has componet is full
            //List<TestDef> l = SourcePacks_All
            //                                .Where(r => r.ComponentID == TestDef.Component.Full)
            //                                .Select(r => r.Substance)
            //                                .ToList();
            List<TestDef> l = new List<TestDef>();
            foreach (TestDef item in l)
            {
                if (item.ID == TestDef.Substance.for21days)
                    return item;
            }

            foreach (TestDef item in l)
            {
                if (item.ID == TestDef.Substance.for35days)
                    return item;
            }

            foreach (TestDef item in l)
            {
                if (item.ID == TestDef.Substance.for42days)
                    return item;
            }

            //return Substance;
            return null;
        }
    }

    public List<TestDef> NonNegativeTestResult()
    {

        List<TestDef> r = new List<TestDef>();

        //if (TestResultStatus == TestResultStatusX.Non)
        //    throw new Exception("Chưa nhập kết quả túi máu.");

        //if (HIVID == TestDef.HIV.Pos || HIVID == TestDef.HIV.NA)
        //    r.Add(HIV);

        //if (HBsAgID == TestDef.HBsAg.Pos || HBsAgID == TestDef.HBsAg.NA)
        //    r.Add(HBsAg);

        //if (HCVID == TestDef.HCV.Pos || HCVID == TestDef.HCV.NA)
        //    r.Add(HCV);

        //if (SyphilisID == TestDef.Syphilis.Pos || SyphilisID == TestDef.Syphilis.NA)
        //    r.Add(Syphilis);

        //if (MalariaID == TestDef.Malaria.Pos || MalariaID == TestDef.Malaria.NA)
        //    r.Add(Malaria);

        return r;

    }

    public PackErr Err { get; set; }

    public List<int> CanExtractToList { get; set; }
    public int? CanExtractToRBC { get; set; }
    public int? CanExtractToWBC { get; set; }
    public int? CanExtractToPlatelet { get; set; }
    public int? CanExtractToFFPlasma { get; set; }
    public int? CanExtractToFFPlasma_Poor { get; set; }
    public int? CanExtractToFactorVIII { get; set; }
}
