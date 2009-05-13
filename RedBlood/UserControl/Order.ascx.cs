using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Order : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //if (CampaignID == 0)
        //{
        //    Campaign p = new Campaign();

        //    if (LoadFromGUI(p))
        //    {
        //        bll.New(p);
        //        CampaignID = p.ID;
        //    }
        //    else
        //        return;
        //}
        //else
        //{
        //    RedBloodDataContext db;

        //    Campaign p = CampaignBLL.GetByID(CampaignID, out db);

        //    if (p == null) return;

        //    if (LoadFromGUI(p))
        //    {
        //        db.SubmitChanges();
        //        CampaignID = p.ID;
        //    }
        //    else return;
        //}

        ScriptManager.RegisterStartupScript(btnUpdate, btnUpdate.GetType(), "SaveDone", "alert ('Lưu thành công.');", true);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //if (CampaignID == 0)
        //{
        //    return;
        //}
        //else
        //{
        //    try
        //    {
        //        string m = bll.Delete(CampaignID);
        //        Clear();

        //        ScriptManager.RegisterStartupScript(btnDelete, btnDelete.GetType(), "", "alert ('" + m + "');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(btnDelete, btnDelete.GetType(), "", "alert ('" + ex.Message + "');", true);
        //    }
        //}
    }
}
