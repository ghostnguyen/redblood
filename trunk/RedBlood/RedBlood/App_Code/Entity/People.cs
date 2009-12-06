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
public partial class People
{
    PeopleBLL bll = new PeopleBLL();
    GeoBLL geoBLL = new GeoBLL();
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            if (string.IsNullOrEmpty(this.Name) ||
                string.IsNullOrEmpty(this.Name.Trim()))
                throw new Exception("Nhập họ và tên.");

            if (string.IsNullOrEmpty(this.CMND) ||
                string.IsNullOrEmpty(this.CMND.Trim()))
            { }
            else
            {
                if (this.CMND.Length < 9)
                    throw new Exception("Số CMND phải từ 9 số trở lên.");

                RedBloodDataContext db = new RedBloodDataContext();

                int count = (from e in db.Peoples
                             where object.Equals(e.CMND, this.CMND.Trim()) && e.ID != this.ID
                             select e).Count();

                if (count > 0)
                {
                    throw new Exception("Trùng số CMND với người khác.");
                }
            }
        }
    }

    partial void OnNameChanging(string value)
    {
        if (string.IsNullOrEmpty(value) ||
                string.IsNullOrEmpty(value.Trim()))
            throw new Exception("Nhập họ và tên.");
    }

    partial void OnNameChanged()
    {
        NameNoDiacritics = Name.RemoveDiacritics();
    }



    public void SetDOBFromVNFormat(string dd, string mm, string yyyy)
    {
        dd = dd.Trim();
        mm = mm.Trim();
        yyyy = yyyy.Trim();
    }

    public void SetDOBFromVNFormat(string value, bool allowYearOnly)
    {
        value = value.Trim();

        if (String.IsNullOrEmpty(value))
        {
            //DOB = null;
            throw new Exception("Nhập ngày tháng năm (dd/mm/yyyy)");
        }
        else
        {
            if (value.IsValidDOB())
            {
                DOB = value.ToDatetimeFromVNFormat();
                DOBYear = null;
            }
            else
            {
                if (allowYearOnly)
                {
                    if (value.ToInt() > 1900 && value.ToInt() <= DateTime.Now.Year)
                    {
                        DOB = null;
                        DOBYear = value.ToInt();
                        return;
                    }
                }
                throw new Exception("Nhập sai ngày tháng năm (dd/mm/yyyy)");
            }
        }
    }

    partial void OnCMNDChanging(string value)
    {
        value = value.Trim();

        if (String.IsNullOrEmpty(value))
        {
        }
        else
        {
            if (value.Length < BarcodeBLL.CMNDLength.ToInt())
            {
                throw new Exception("Số CMND phải từ 9 số trở lên.");
            }

            if (bll.IsCMNDDuplicated(value, ID))
            {
                throw new Exception("Số CMND này đã có trong dữ liệu.");
            }
        }
    }

    public void SetResidentGeo3(string value)
    {
        value = value.Trim();
        if (String.IsNullOrEmpty(value))
        {
            //ResidentGeoID1 = null;
            //ResidentGeoID2 = null;
            //ResidentGeoID3 = null;
            throw new Exception("Nhập đơn vị hành chính.");
        }
        else
        {
            //Geo g = geoBLL.GetByFullnameAndLevel(value, 3);
            Geo g = GeoBLL.GetByFullname(value);
            if (g == null)
            {
                throw new Exception("Nhập sai đơn vị hành chính.");
            }
            else
            {
                ResidentGeoID1 = null;
                ResidentGeoID2 = null;
                ResidentGeoID3 = null;

                if (g.Level == 1)
                {
                    ResidentGeoID1 = g.ID;
                }

                if (g.Level == 2)
                {
                    ResidentGeoID2 = g.ID;
                    ResidentGeoID1 = g.ParentGeo.ID;
                }

                if (g.Level == 3)
                {
                    ResidentGeoID3 = g.ID;
                    ResidentGeoID2 = g.ParentGeo.ID;
                    ResidentGeoID1 = g.ParentGeo.ParentGeo.ID;
                }
            }
        }
    }

    public void SetMailingGeo3(string value)
    {
        value = value.Trim();
        if (String.IsNullOrEmpty(value))
        {
            MailingGeoID1 = null;
            MailingGeoID2 = null;
            MailingGeoID3 = null;
        }
        else
        {
            //Geo g = geoBLL.GetByFullnameAndLevel(value, 3);
            Geo g = GeoBLL.GetByFullname(value);

            if (g == null)
            {
                throw new Exception("Nhập sai đơn vị hành chính.");
            }
            else
            {
                MailingGeoID1 = null;
                MailingGeoID2 = null;
                MailingGeoID3 = null;

                if (g.Level == 1)
                {
                    MailingGeoID1 = g.ID;
                }

                if (g.Level == 2)
                {
                    MailingGeoID2 = g.ID;
                    MailingGeoID1 = g.ParentGeo.ID;
                }

                if (g.Level == 3)
                {
                    MailingGeoID3 = g.ID;
                    MailingGeoID2 = g.ParentGeo.ID;
                    MailingGeoID1 = g.ParentGeo.ParentGeo.ID;
                }
            }
        }
    }

    public string FullResidentalGeo
    {
        get
        {
            string r = "";
            if (ResidentGeo3 != null)
                r += ResidentGeo3.Fullname;
            else if (ResidentGeo2 != null)
                r += ResidentGeo2.Fullname;
            else if (ResidentGeo1 != null)
                r += ResidentGeo1.Fullname;

            return r;
        }
    }

    public string FullResidentalAddress
    {
        get
        {
            string r = ResidentAddress + ", " + FullResidentalGeo;
            return r.Trim(',', ' ');
        }
    }


    public string FullMaillingGeo
    {
        get
        {
            string r = "";
            if (MailingGeo3 != null)
                r += MailingGeo3.Fullname;
            else if (MailingGeo2 != null)
                r += MailingGeo2.Fullname;
            else if (MailingGeo1 != null)
                r += MailingGeo1.Fullname;

            return r;
        }
    }

    public string FullMailingAddress
    {
        get
        {
            string r = MailingAddress + ", " + FullMaillingGeo;
            return r.Trim(',', ' ');
        }
    }

    public string DOBToString()
    {
        if (DOB != null)
        {
            return DOB.ToStringVN();
        }
        else if (DOBYear != null)
        {
            return DOBYear.ToString();
        }

        return string.Empty;
    }
}
