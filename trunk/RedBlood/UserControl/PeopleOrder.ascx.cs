using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class UserControl_PeopleOrder : System.Web.UI.UserControl
{
    GeoBLL geoBLL = new GeoBLL();
    CodabarBLL codabarBLL = new CodabarBLL();
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
            if (value == null)
            { }
            else
            {
                LoadPeople();
            }
            if (PeopleChanged != null)
                PeopleChanged(value, null);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadPeople()
    {
        People e = PeopleBLL.GetByID(PeopleID);

        if (e == null)
        {
        }
        else
        {
            txtName.Text = e.Name;
            txtCMND.Text = e.CMND;

            if (e.DOB != null)
                txtDOB.Text = e.DOB.ToStringVN();

            ddlSex.SelectedValue = e.SexID.ToString();

            txtResidentAddress.Text = e.ResidentAddress;
            if (e.ResidentGeo1 != null)
                txtResidentGeo.Text = e.FullResidentalGeo;
        }
    }

    public void Clear()
    {
        ViewState["PeopleID"] = Guid.Empty;
        txtName.Text = "";
        txtCMND.Text = "";
        txtDOB.Text = "";
        ddlSex.SelectedIndex = 0;
        txtResidentAddress.Text = "";
        txtResidentGeo.Text = "";
        divErrResidentalGeo.Attributes["class"] = "hidden";
    }

    public void New(string CMND)
    {
        Clear();
        txtCMND.Text = CMND;
        txtName.Focus();
    }
}
