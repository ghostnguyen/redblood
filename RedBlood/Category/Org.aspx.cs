using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category_Org : System.Web.UI.Page
{
    public int OrgID
    {
        get
        {
            if (ViewState["OrgID"] == null)
                return 0;
            return (int)ViewState["OrgID"];
        }
        set
        {
            Clear();
            ViewState["OrgID"] = value;
            if (value == 0)
            { }
            else
            {
                LoadOrg();
            }
        }
    }

    GeoBLL geoBLL = new GeoBLL();
    CodabarBLL codabarBLL = new CodabarBLL();
    OrgBLL bll = new OrgBLL();

    string styleHidden = "visibility: hidden; height: 0px; width: 0px;";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'");
            // This will be the back ground color of the GridView Control
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='White'");
        }
    }
    protected void LinqDataSourceFind_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = bll.Search(txtNameFind.Text.Trim());
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string ID = e.CommandArgument.ToString();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        OrgID = (int)GridView1.SelectedValue;
    }

    public void LoadOrg()
    {
        Org e = bll.GetByID(OrgID);

        if (e == null)
        { }
        else
        {
            imgCodabar.ImageUrl = "../Codabar/Image.aspx?hasText=true&code="
                + codabarBLL.GenStringCode(Resources.Codabar.orgSSC, e.ID.ToString());
            txtName.Text = e.Name;

            txtAddress.Text = e.Address;

            if (e.Geo1 != null)
                txtGeo.Text = e.FullGeo;

            txtNote.Text = e.Note;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (OrgID == 0)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            Org p = new Org();

            if (LoadFromGUI(p))
            {
                db.Orgs.InsertOnSubmit(p);
                db.SubmitChanges();

                OrgID = p.ID;
            }
            else return;
        }
        else
        {
            RedBloodDataContext db = new RedBloodDataContext();

            Org p = bll.GetByID(OrgID, out db);

            if (p == null) return;

            if (LoadFromGUI(p))
            {
                db.SubmitChanges();
                OrgID = p.ID;
            }
            else return;
        }

        GridView1.DataBind();
        ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType(), "SaveDone", "alert ('Lưu thành công.');", true);

    }

    private bool LoadFromGUI(Org p)
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

        p.Address = txtAddress.Text;

        try
        {
            p.SetResidentGeo3(txtGeo.Text.Trim());
            divErrGeo.Attributes["style"] = styleHidden;
        }
        catch (Exception ex)
        {
            divErrGeo.Attributes["style"] = "color:red;";
            divErrGeo.InnerText = ex.Message;
            isDone = false;
        }

        p.Note = txtNote.Text;

        return isDone;
    }

    public void Clear()
    {
        ViewState["OrgID"] = 0;
        imgCodabar.ImageUrl = "none";
        txtName.Text = "";
        txtAddress.Text = "";
        txtGeo.Text = "";
        txtNote.Text = "";

        divErrName.Attributes["style"] = styleHidden;
        divErrGeo.Attributes["style"] = styleHidden;
    }

    public void New()
    {
        Clear();
        txtName.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        New();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (OrgID == 0)
        {
            return;
        }
        else
        {
            try
            {
                string m = bll.Delete(OrgID);
                Clear();

                GridView1.DataBind();

                ScriptManager.RegisterStartupScript(btnDelete, btnDelete.GetType(), "", "alert ('" + m + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(btnDelete, btnDelete.GetType(), "", "alert ('" + ex.Message + "');", true);
            }
        }
    }
}
