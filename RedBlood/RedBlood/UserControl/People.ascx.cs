using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;

public partial class UserControl_People : System.Web.UI.UserControl
{
    GeoBLL geoBLL = new GeoBLL();
    BarcodeBLL codabarBLL = new BarcodeBLL();
    PeopleBLL bll = new PeopleBLL();

    public event EventHandler PeopleChanged;

    public Guid PeopleID
    {
        get
        {
            if (ViewState["PeopleID"] == null)
                return Guid.Empty;
            return (Guid)ViewState["PeopleID"];
        }
        set
        {
            Clear();

            ViewState["PeopleID"] = value;
            if (PeopleID == Guid.Empty)
            { }
            else
            {
                LoadPeople();
            }
            if (PeopleChanged != null)
                PeopleChanged(value, null);
        }
    }

    string _code;
    public string Code
    {
        get
        {
            return _code;
        }
        set
        {
            _code = value;
            if (BarcodeBLL.IsValidPeopleCode(Code))
            {
                int autonum = BarcodeBLL.ParsePeopleCode(Code);
                People p = PeopleBLL.GetByID(autonum);
                if (p != null)
                    PeopleID = p.ID;
                else
                    PeopleID = Guid.Empty; 
            }
            else if (Code.Length >= 9)
            {
                People r = PeopleBLL.GetByCMND(Code);
                if (r != null)
                {
                    PeopleID = r.ID;
                }
                else
                {
                    New(Code);
                }
            }
            else
            { }
        }
    }

    public bool HideMoreDetail
    {
        set
        {
            divMoreDetail.Visible = !value;
        }
    }

    public bool ReadOnly
    {
        set
        {
            btnUpdate.Visible = !value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void chkEnableMaillingAddress_CheckedChanged(object sender, EventArgs e)
    {
        txtMailingAddress.Enabled = chkEnableMaillingAddress.Checked;
        txtMailingGeo.Enabled = chkEnableMaillingAddress.Checked;
        chkEnableMaillingAddress.Focus();
    }

    public void LoadPeople()
    {
        People e = PeopleBLL.GetByID(PeopleID);

        if (e == null)
        {

        }
        else
        {
            imgCodabar.ImageUrl = BarcodeBLL.Url4People(e.Autonum);

            txtName.Text = e.Name;
            txtCMND.Text = e.CMND;

            txtDOB.Text = e.DOBToString;

            if (ddlSex.Items.Count == 1)
                ddlSex.DataBind();

            ddlSex.SelectedValue = e.SexID.ToString();

            txtResidentAddress.Text = e.ResidentAddress;
            if (e.ResidentGeo1 != null)
                txtResidentGeo.Text = e.FullResidentalGeo;


            if (e.EnableMailingAddress == null)
                chkEnableMaillingAddress.Checked = false;
            else
                chkEnableMaillingAddress.Checked = e.EnableMailingAddress.Value;

            txtMailingAddress.Enabled = chkEnableMaillingAddress.Checked;
            txtMailingGeo.Enabled = chkEnableMaillingAddress.Checked;

            txtMailingAddress.Text = e.MailingAddress;
            if (e.MailingGeo1 != null)
                txtMailingGeo.Text = e.FullMaillingGeo;

            txtJob.Text = e.Job;
            txtEmail.Text = e.Email;
            txtPhone.Text = e.Phone;
            txtNote.Text = e.Note;
        }
    }

    public void Clear()
    {
        //ViewState["PeopleID"] = Guid.Empty;

        imgCodabar.ImageUrl = "none";

        txtName.Text = "";
        txtCMND.Text = "";
        txtDOB.Text = "";
        ddlSex.SelectedIndex = 0;
        txtResidentAddress.Text = "";
        txtResidentGeo.Text = "";
        chkEnableMaillingAddress.Checked = false;

        txtMailingAddress.Text = "";
        txtMailingAddress.Enabled = false;

        txtMailingGeo.Text = "";
        txtMailingGeo.Enabled = false;

        txtJob.Text = "";
        txtEmail.Text = "";
        txtPhone.Text = "";
        txtNote.Text = "";

        divErrName.Attributes["class"] = "hidden";
        divErrCMND.Attributes["class"] = "hidden";
        divErrDOB.Attributes["class"] = "hidden";
        divErrSex.Attributes["class"] = "hidden";
        divErrMailingGeo.Attributes["class"] = "hidden";
        divErrResidentalGeo.Attributes["class"] = "hidden";
    }

    public void New(string CMND)
    {
        //Clear();

        PeopleID = Guid.Empty;
        txtCMND.Text = CMND;
        //txtName.Focus();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //Thread.Sleep(5000);
        if (PeopleID == Guid.Empty)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            People p = new People();

            if (LoadFromGUI(p))
            {
                db.Peoples.InsertOnSubmit(p);
                db.SubmitChanges();
                PeopleID = p.ID;
            }
            else
                return;
        }
        else
        {
            RedBloodDataContext db = new RedBloodDataContext();

            var p = (from c in db.Peoples
                     where c.ID == PeopleID
                     select c).First();

            if (p == null) return;

            if (LoadFromGUI(p))
            {
                db.SubmitChanges();
                PeopleID = p.ID;
            }
            else return;
        }
        
        Page.Alert("Lưu thành công.");
    }

    private bool LoadFromGUI(People p)
    {
        bool isDone = true;

        try
        {
            p.Name = txtName.Text.Trim();
            divErrName.Attributes["class"] = "hidden";
        }
        catch (Exception ex)
        {
            divErrName.InnerText = ex.Message;
            divErrName.Attributes["class"] = "err";
            isDone = false;
        }

        try
        {
            p.SetDOBFromVNFormat(txtDOB.Text.Trim(), true);
            divErrDOB.Attributes["class"] = "hidden";
        }
        catch (Exception ex)
        {
            divErrDOB.Attributes["class"] = "err";
            divErrDOB.InnerText = ex.Message;
            isDone = false;
        }

        try
        {
            p.CMND = txtCMND.Text.Trim();
            divErrCMND.Attributes["class"] = "hidden";
        }
        catch (Exception ex)
        {
            divErrCMND.InnerText = ex.Message;
            divErrCMND.Attributes["class"] = "err";
            isDone = false;
        }

        if (ddlSex.SelectedValue.ToGuid() == Guid.Empty)
        {
            //p.SexID = null;
            divErrSex.Attributes["class"] = "err";
            divErrSex.InnerText = "Chọn giới tính";
            isDone = false;
        }
        else
        {
            p.SexID = ddlSex.SelectedValue.ToGuid();
            divErrSex.Attributes["class"] = "hidden";
        }

        p.ResidentAddress = txtResidentAddress.Text.Trim();

        try
        {
            p.SetResidentGeo3(txtResidentGeo.Text.Trim());
            divErrResidentalGeo.Attributes["class"] = "hidden";
        }
        catch (Exception ex)
        {
            divErrResidentalGeo.Attributes["class"] = "err";
            divErrResidentalGeo.InnerText = ex.Message;
            isDone = false;
        }

        p.EnableMailingAddress = chkEnableMaillingAddress.Checked;
        p.MailingAddress = txtMailingAddress.Text.Trim();

        if (p.EnableMailingAddress.Value)
        {
            try
            {
                p.SetMailingGeo3(txtMailingGeo.Text.Trim());
                divErrMailingGeo.Attributes["class"] = "hidden";
            }
            catch (Exception ex)
            {
                divErrMailingGeo.Attributes["class"] = "err";
                divErrMailingGeo.InnerText = ex.Message;
                isDone = false;
            }
        }

        p.Job = txtJob.Text;
        p.Email = txtEmail.Text;
        p.Phone = txtPhone.Text;
        p.Note = txtNote.Text;

        return isDone;
    }
}
