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


        //Expired = 40,
        //Quá_hạn = 40,
        //ExpireEnter = 41,
        //ExpireCommitReceived = 42,
        DataErr = 49
    }

   
}
