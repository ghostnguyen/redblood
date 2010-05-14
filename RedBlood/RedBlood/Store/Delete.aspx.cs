using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class Store_Delete : System.Web.UI.Page
{
    public int DeleteID
    {
        get
        {
            if (ViewState["DeleteID"] == null)
                return 0;
            return (int)ViewState["DeleteID"];
        }
        set
        {
            ViewState["DeleteID"] = value;
            LoadDelete();
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

    public List<Guid> PackList
    {
        get
        {
            if (ViewState["PackList"] == null)
            {
                ViewState["PackList"] = new List<Guid>();
            }
            return (List<Guid>)ViewState["PackList"];
        }
        set
        {
            ViewState["PackList"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int returnID = Request.Params["key"].ToInt();

            if (returnID != 0)
            {
                DeleteID = returnID;
            }
        }
        else
        {
            string code = Master.TextBoxCode.Text.Trim();
            Master.TextBoxCode.Text = "";

            if (BarcodeBLL.IsValidDINCode(code))
            {
                LoadCurrentDIN(BarcodeBLL.ParseDIN(code));
            }
            else if (BarcodeBLL.IsValidDeleteCode(code))
            {
                DeleteID = BarcodeBLL.ParseDeleteID(code);
            }
            else if (BarcodeBLL.IsValidProductCode(code))
            {
                AddPack(BarcodeBLL.ParseProductCode(code));
            }
        }
    }

    void AddPack(string productCode)
    {
        Pack p = PackBLL.Get(CurrentDIN, productCode);

        if (PackList.Contains(p.ID))
        {
            throw new Exception("Đã nhập túi máu này.");
        }

        PackList.Add(p.ID);

        GridViewPack.DataBind();

        CurrentDIN = "";
        imgCurrentDIN.ImageUrl = "none";
    }

    void LoadCurrentDIN(string DIN)
    {
        Donation e = DonationBLL.Get(DIN);

        CurrentDIN = e.DIN;
        imgCurrentDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN);
    }

    protected void btnNewReturn_Click(object sender, EventArgs e)
    {
        DeleteID = 0;
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (DeleteID != 0) return;

        LinkButton btn = sender as LinkButton;
        if (btn != null)
        {
            if (PackList.Remove(btn.CommandArgument.ToGuid()))
                GridViewPack.DataBind();
        }
    }

    public void LoadDelete()
    {
        Delete e = new Delete();
        if (DeleteID == 0)
        {
            imgOrder.ImageUrl = "none";
            btnOk.Enabled = true;
        }
        else
        {
            e = DeleteBLL.Get(DeleteID);
            imgOrder.ImageUrl = BarcodeBLL.Url4Delete(e.ID);
            btnOk.Enabled = false;
        }

        txtNote.Text = e.Note;

        txtDate.Text = e.Date.ToStringVN_Hour();

        PackList = e.Packs.Select(r => r.ID).ToList();

        GridViewPack.DataBind();

        CurrentDIN = "";
        imgCurrentDIN.ImageUrl = "none";
    }

    protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.Packs.Where(r => PackList.Contains(r.ID))
            .Select(r => new
            {
                r.ID,
                r.DeleteID,
                DIN = r.Donation.DIN,
                r.ProductCode,
                Expired = r.ExpirationDate.Value.Expired()
            });
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        DeleteID = DeleteBLL.Add(PackList, txtNote.Text.Trim());

        this.Alert("Lưu thành công.");
    }
}
