using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_PackSideEffect : System.Web.UI.UserControl
{
    public event EventHandler PackDeleted;

    public int Autonum
    {
        get
        {
            if (ViewState["Autonum"] == null)
                return 0;
            return (int)ViewState["Autonum"];
        }
        set
        {
            ViewState["Autonum"] = value;
            if (value == 0)
            { }
            else
            {
                GridViewSideEffect.DataBind();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = PackBLL.Get(Autonum, db);

        if (p == null) return;

        if (string.IsNullOrEmpty(txtSideEffect.Text.Trim()))
            return;

        PackSideEffect se = new PackSideEffect();

        se.PackID = p.ID;
        se.SetSideEffect(txtSideEffect.Text.Trim());
        se.Actor = Page.User.Identity.Name;
        se.Date = DateTime.Now;
        se.Note = txtNote.Text.Trim();

        db.PackSideEffects.InsertOnSubmit(se);

        db.SubmitChanges();

        GridViewSideEffect.DataBind();

        ScriptManager.RegisterStartupScript(btnOk, this.GetType(), "SaveDone", "alert ('Lưu thành công.');", true);
    }

    protected void LinqDataSourceSideEffect_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = PackSideEffectBLL.Get(Autonum);
    }
}
