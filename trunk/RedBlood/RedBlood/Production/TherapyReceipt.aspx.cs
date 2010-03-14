using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_TherapyReceipt : System.Web.UI.Page
{
    string strID = "strID";
    public Guid ID
    {
        get
        {
            if (ViewState[strID] == null)
                return Guid.Empty;
            return (Guid)ViewState[strID];
        }
        set
        {
            ViewState[strID] = value;
            if (value == Guid.Empty)
            {
                ClearGUI();
            }
            else
            {
                DisplayToGUI();
            }
        }
    }

    string styleHidden = "visibility: hidden; height: 0px; width: 0px;";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
 
    protected void LinqDataSourceFind_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = ReceiptBLL.Find(txtNameFind.Text.Trim());
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (btn != null)
        {
            ID = btn.CommandArgument.ToGuid();
        }
    }

    public void DisplayToGUI()
    {
        Receipt e = ReceiptBLL.Get(ID);

        if (e != null)
        {
            txtName.Text = e.Name;
            txtNote.Text = e.Note;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ID == Guid.Empty)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            Receipt r = new Receipt();

            if (LoadFromGUI(r))
            {
                db.Receipts.InsertOnSubmit(r);
                db.SubmitChanges();

                ID = r.ID;
            }
        }
        else
        {
            RedBloodDataContext db = new RedBloodDataContext();

            Receipt r = ReceiptBLL.Get(ID, db);

            if (r == null) return;

            if (LoadFromGUI(r))
            {
                db.SubmitChanges();
            }
        }

        GridView1.DataBind();
        this.Alert("Lưu thành công.");
    }

    private bool LoadFromGUI(Receipt p)
    {
        bool isDone = true;
        try
        {
            p.Name = txtName.Text.Trim();
            divErrName.Attributes["style"] = styleHidden;
        }
        catch (Exception ex)
        {
            divErrName.InnerText = ex.Message;
            divErrName.Attributes["style"] = "color:red;";
            isDone = false;
        }

        p.Note = txtNote.Text;

        return isDone;
    }

    public void ClearGUI()
    {
        txtName.Text = "";
        txtNote.Text = "";

        divErrName.Attributes["style"] = styleHidden;
    }

    public void New()
    {
        ID = Guid.Empty;
        ClearGUI();
        txtName.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        New();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ID != Guid.Empty)
        {
            ReceiptBLL.Delete(ID);

            ID = Guid.Empty;
            ClearGUI();

            GridView1.DataBind();
        }
    }
    
}
