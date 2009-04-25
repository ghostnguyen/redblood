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
/// Summary description for BloodType
/// </summary>
public partial class BloodType
{
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {

        }
    }

    partial void OnLoaded()
    {
        //string dataErr = "Dữ liệu nhóm máu bị lỗi.";

        //if (Pack == null) return;

        //Pack p = this.Pack;

        //if (p.Status == Pack.StatusX.Assign)
        //{
        //    if (!string.IsNullOrEmpty(Actor) || CommitDate != null || Times != 1)
        //        throw new Exception(dataErr);
        //}

        //if (p.Status == Pack.StatusX.CommitReceived)
        //{
        //    if (string.IsNullOrEmpty(Actor) || CommitDate == null || Times != 1
        //        || rhID == null || aboID == null)
        //        throw new Exception(dataErr);
        //}
    }
}
