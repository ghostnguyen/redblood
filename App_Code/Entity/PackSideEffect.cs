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
/// Summary description for PackSideEffect
/// </summary>
public partial class PackSideEffect
{
    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        if (action == System.Data.Linq.ChangeAction.Insert
            || action == System.Data.Linq.ChangeAction.Update)
        {
            
        }
    }

    public void SetSideEffect(string value)
    {
        value = value.Trim();
        if (String.IsNullOrEmpty(value))
        {
            SideEffectID1 = null;
            SideEffectID2 = null;
            SideEffectID3 = null;
        }
        else
        {
            SideEffect g = SideEffectBLL.GetByFullname(value);
            if (g == null)
            {
                throw new Exception("Nhập sai triệu chứng.");
            }
            else
            {
                SideEffectID1 = null;
                SideEffectID2 = null;
                SideEffectID3 = null;

                if (g.Level == 1)
                {
                    SideEffectID1 = g.ID;
                }

                if (g.Level == 2)
                {
                    SideEffectID2 = g.ID;
                    SideEffectID1 = g.ParentSideEffect.ID;
                }

                if (g.Level == 3)
                {
                    SideEffectID3 = g.ID;
                    SideEffectID2 = g.ParentSideEffect.ID;
                    SideEffectID1 = g.ParentSideEffect.ParentSideEffect.ID;
                }
            }
        }
    }
    public string FullSideEffect
    {
        get
        {
            string r = "";
            if (SideEffect3 != null)
                r += SideEffect3.Fullname;
            else if (SideEffect2 != null)
                r += SideEffect2.Fullname;
            else if (SideEffect1 != null)
                r += SideEffect1.Fullname;

            return r;
        }
    }
}
