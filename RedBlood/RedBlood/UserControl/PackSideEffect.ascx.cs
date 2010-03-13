using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_PackSideEffect : System.Web.UI.UserControl
{
    public string Code
    {
        set
        {
            string code = value.Trim();
            if (BarcodeBLL.IsValidDINCode(code))
            {
                LoadCurrentDIN(BarcodeBLL.ParseDIN(code));
            }
            else if (BarcodeBLL.IsValidProductCode(code))
            {
                LoadPack(BarcodeBLL.ParseProductCode(code));
            }
        }
    }

    public string CurrentDIN
    {
        get
        {
            if (ViewState["CurrentDIN"] == null)
                return "";
            return (string)ViewState["CurrentDIN"];
        }
        set
        {
            ViewState["CurrentDIN"] = value;
        }
    }

    public Guid PackID
    {
        get
        {
            if (ViewState["PackID"] == null)
                return Guid.Empty;
            return (Guid)ViewState["PackID"];
        }
        set
        {
            ViewState["PackID"] = value;
        }
    }

    void LoadCurrentDIN(string code)
    {
        Donation e = DonationBLL.Get(code);
        if (e == null) return;

        CurrentDIN = e.DIN;
        ImageCurrentDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN);

        PackID = Guid.Empty;
        ImageProduct.ImageUrl = "none";
    }

    void LoadPack(string productCode)
    {
        //Check Pack
        RedBloodDataContext db = new RedBloodDataContext();
        Pack p = db.Packs.Where(r => r.DIN == CurrentDIN && r.ProductCode == productCode).FirstOrDefault();

        if (p == null) return;
        if (p.Status != Pack.StatusX.Delivered)
        {
            Page.Alert("Túi máu chưa cấp phát.");
            return;
        }

        PackID = p.ID;

        ImageProduct.ImageUrl = BarcodeBLL.Url4Product(p.ProductCode);
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack p = db.Packs.Where(r => r.ID == PackID).FirstOrDefault();

        if (p == null) return;

        if (p.Status != Pack.StatusX.Delivered)
        {
            Page.Alert("Túi máu chưa cấp phát.");
            return;
        }

        if (string.IsNullOrEmpty(txtSideEffect.Text.Trim()))
            return;

        PackSideEffect se = new PackSideEffect();

        se.PackID = p.ID;
        se.SetSideEffect(txtSideEffect.Text.Trim());
        se.Actor = RedBloodSystem.CurrentActor;
        se.Date = DateTime.Now;
        se.Note = txtNote.Text.Trim();

        db.PackSideEffects.InsertOnSubmit(se);

        db.SubmitChanges();

        GridViewSideEffect.DataBind();

        Page.Alert("Lưu thành công.");
    }

    protected void LinqDataSourceSideEffect_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = PackSideEffectBLL.Get(PackID);
    }
}
