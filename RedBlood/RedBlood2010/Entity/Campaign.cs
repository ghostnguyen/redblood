﻿using System;
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
using System.Collections.Generic;
using RedBlood.BLL;
namespace RedBlood
{
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
                if (CampaignBLL.IsExistNameInSameDate(value.Trim(), ID, Date.Value))
                    throw new Exception("Trùng tên");
        }


        partial void OnNameChanged()
        {
            if (!string.IsNullOrEmpty(Name))
                NameNoDiacritics = Name.RemoveDiacritics();
        }

        partial void OnSourceIDChanging(int? value)
        {
            if (value == null || value == 0)
                throw new Exception("Nhập nguồn thu máu.");
        }

        public IEnumerable<Donation> CollectedDonations
        {
            get { return Donations.Where(r => r.Pack != null); }
        }

    }
}