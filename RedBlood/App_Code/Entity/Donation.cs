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
        Delete = 4,
        Hủy = 4,

        DataErr = 49
    }    
}
