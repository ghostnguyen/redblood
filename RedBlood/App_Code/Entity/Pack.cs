using System;
using System.Data;
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
        Assign = 1,
        //CommitReceived = 2,
        Delete = 4,
        Hủy = 4,

        EnterTestResult = 7,
        Đang_nhập_KQ = 7,
        Đã_nhập = 8,
        CommitTestResult = 8,

        Delivered = 9,
        Cấp_phát = 9,
        Sản_xuất = 10,
        Production = 10,

        Expire = 40,
        Quá_hạn = 40,
        ExpireEnter = 41,
        ExpireCommitReceived = 42,
        DataErr = 49
    }


    PackBLL bll = new PackBLL();
    CampaignBLL campaignBLL = new CampaignBLL();
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {

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
            if (Status == StatusX.Init || Status == StatusX.Assign)
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

            Pack p = bll.GetEnterPackByPeopleID(value.Value);
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

    public TestResult TestResult2
    {
        get { return TestResults.Where(r => r.Times == 2).FirstOrDefault(); }
    }

    public BloodType BloodType2
    {
        get
        {
            return BloodTypes.Where(r => r.Times == 2).FirstOrDefault();
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

}
