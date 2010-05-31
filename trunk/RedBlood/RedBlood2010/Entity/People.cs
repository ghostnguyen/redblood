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
using RedBlood.BLL;

namespace RedBlood
{
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

        public int SetResidentGeo3(Guid? geo1ID, Guid? geo2ID, Guid? geo3ID)
        {
            ResidentGeoID1 = geo1ID;
            ResidentGeoID2 = geo2ID;
            ResidentGeoID3 = geo3ID;

            return 0;
        }

        public void SetResidentGeo3(string value)
        {
            GeoBLL.Set3LevelByFullname(value, SetResidentGeo3);
        }

        public int SetMailingGeo3(Guid? geo1ID, Guid? geo2ID, Guid? geo3ID)
        {
            MailingGeoID1 = geo1ID;
            MailingGeoID2 = geo2ID;
            MailingGeoID3 = geo3ID;

            return 0;
        }

        public void SetMailingGeo3(string value)
        {
            GeoBLL.Set3LevelByFullname(value, SetMailingGeo3);
        }

        public string FullResidentalGeo
        {
            get
            {
                return GeoBLL.GetFullname(ResidentGeo1, ResidentGeo2, ResidentGeo3);
            }
        }

        public string FullResidentalAddress
        {
            get
            {
                return GeoBLL.GetFullAddress(ResidentAddress, FullResidentalGeo);
            }
        }

        public string FullMaillingGeo
        {
            get
            {
                return GeoBLL.GetFullname(MailingGeo1, MailingGeo2, MailingGeo3);
            }
        }

        public string FullMailingAddress
        {
            get
            {
                return GeoBLL.GetFullAddress(MailingAddress, FullMaillingGeo);
            }
        }

        public string DOBToString
        {
            get
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

        public int DOBInDecade
        {
            get
            {
                if (DOB != null)
                {
                    return DOB.Value.Decade();
                }
                else if (DOBYear != null)
                {
                    return (new DateTime(DOBYear.Value, 1, 1)).Decade();
                }

                return 0;
            }
        }
    }
}