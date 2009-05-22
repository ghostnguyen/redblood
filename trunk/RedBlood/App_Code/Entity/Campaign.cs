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
/// Summary description for Campaign
/// </summary>
public partial class Campaign
{
    public enum TypeX
    {
        Short_run = 1,
        Long_run = 8
    }

    public enum StatusX
    {
        All = -1,
        Init = 0,
        Assign = 1,
        TSIn = 2,
        Delete = 4
    }

    OrgBLL orgBLL = new OrgBLL();
    CampaignBLL bll = new CampaignBLL();

    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            OnNameChanging(Name);
        }
    }

    partial void OnNameChanging(string value)
    {
        if (string.IsNullOrEmpty(value.Trim()))
            throw new Exception("Nhập tên chiến dịch");

        if (Date != null)
            if (bll.IsExistNameInSameDate(value.Trim(), ID, Date.Value))
                throw new Exception("Trùng tên");
    }


    partial void OnNameChanged()
    {
        if (!string.IsNullOrEmpty(Name))
            NameNoDiacritics = Name.RemoveDiacritics();
    }

    public void SetDateFromVNFormat(string value)
    {
        value = value.Trim();

        if (String.IsNullOrEmpty(value))
        {
            //throw new Exception("Nhập thời gian (dd/mm/yyyy hh:mm)");
            Date = null;
        }
        else
        {
            DateTime? dt = value.ToDatetimeFromVNFormat();

            if (dt != null)
            {
                Date = dt;
            }
            else
            {
                throw new Exception("Nhập sai thời gian (dd/mm/yyyy hh:mm)");
            }
        }
    }

    public void SetCoopOrgID(string value)
    {
        value = value.Trim();
        if (String.IsNullOrEmpty(value))
        {
            CoopOrgID = null;
        }
        else
        {
            Org g = OrgBLL.GetByName(value);
            if (g == null)
            {
                throw new Exception("Nhập sai tên đơn vị phối hợp.");
            }
            else
            {
                CoopOrgID = g.ID;
            }
        }
    }

    public void SetHostOrgID(string value)
    {
        value = value.Trim();
        if (String.IsNullOrEmpty(value))
        {
            HostOrgID = null;
        }
        else
        {
            Org g = OrgBLL.GetByName(value);
            if (g == null)
            {
                throw new Exception("Nhập sai tên địa điểm thu máu.");
            }
            else
            {
                HostOrgID = g.ID;
            }
        }
    }

    partial void OnSourceIDChanging(int? value)
    {
        if (value == null || value == 0)
            throw new Exception("Nhập nguồn thu máu.");
    }
}
