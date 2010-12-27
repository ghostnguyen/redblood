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
    /// Summary description for Donation
    /// </summary>
    public partial class Donation
    {
        public enum StatusX : int
        {
            Non = -2,
            All = -1,
            Init = 0,

            Đã_thu = 1,
            Assigned = 1,

            //CommitReceived = 2,
            //Delete = 4,
            //Hủy = 4,

            DataErr = 49
        }

        public enum TestResultStatusX : int
        {
            Chưa_có = 0,
            Non = 0,

            Âm_tính = 1,
            Negative = 1,

            Dương_tính = 2,
            Positive = 2

            //Âm_tính_K = 3,
            //NegativeLocked = 3,

            //Dương_tính_K = 4,
            //PositiveLocked = 4
        }

        public bool IsTRLocked
        {
            get
            {
                return !DonationBLL.CanUpdateTestResult(this);
            }
        }

        public bool CanRemoveOriginalPack
        {
            get
            {
                return Pack != null
                    && Packs.Count == 1
                    && TestResultStatus == Donation.TestResultStatusX.Non;
            }
        }

        public string BloodGroupDesc
        {
            get
            {
                return BloodGroupBLL.GetDescription(this.BloodGroup);
            }
        }

        public string OrgProductDesc
        {
            get
            {
                if (this.Pack == null) return "";
                else return this.Pack.Product.Description;
            }
        }

        public string OrgProduct
        {
            get
            {
                if (this.Pack == null) return "";
                else return this.Pack.Product.Code;
            }
        }

        public string OrgVolume
        {
            get
            {
                if (this.Pack == null) return "";
                else return this.Pack.Volume.ToString();
            }
        }

        public InfectiousMarker Markers
        {
            get
            {
                return new InfectiousMarker() { Code = InfectiousMarkers };
            }
        }

        
    }
}