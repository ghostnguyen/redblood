using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UserControl_CustomerEditLocation : System.Web.UI.UserControl
{
    CustomerLocationBLL locationBLL = new CustomerLocationBLL();
    CustomerContactPersonBLL contactBLL = new CustomerContactPersonBLL();

    public event EventHandler LocationChanged;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void DropDownListGeo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGeo2.Items.Clear();
        DropDownListGeo2.Items.Add(new ListItem("--Chưa rõ--", "00000000-0000-0000-0000-000000000000"));
        DropDownListGeo2.DataBind();


        DropDownListGeo3.Items.Clear();
        DropDownListGeo3.Items.Add(new ListItem("--Chưa rõ--", "00000000-0000-0000-0000-000000000000"));
        DropDownListGeo3.DataBind();
    }
    protected void DropDownListGeo2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGeo3.Items.Clear();
        DropDownListGeo3.Items.Add(new ListItem("--Chưa rõ--", "00000000-0000-0000-0000-000000000000"));
        DropDownListGeo3.DataBind();
    }

    public void SetCustomerID(string ID)
    {
        txtCustomerID.Text = ID;
        GridViewLocation.DataBind();
    }

    private void SelectGeos(Guid? geoID1, Guid? geoID2, Guid? geoID3)
    {
        if (geoID1 == null) DropDownListGeo1.SelectedIndex = 0;
        else
        {
            DropDownListGeo1.SelectedValue = geoID1.ToString();

            DropDownListGeo2.Items.Clear();
            DropDownListGeo2.Items.Add(new ListItem("--Chưa rõ--", "00000000-0000-0000-0000-000000000000"));
            DropDownListGeo2.DataBind();


            DropDownListGeo3.Items.Clear();
            DropDownListGeo3.Items.Add(new ListItem("--Chưa rõ--", "00000000-0000-0000-0000-000000000000"));
            DropDownListGeo3.DataBind();
        }

        if (geoID2 == null) DropDownListGeo2.SelectedIndex = 0;
        else
        {
            DropDownListGeo2.SelectedValue = geoID2.ToString();
            DropDownListGeo3.Items.Clear();
            DropDownListGeo3.Items.Add(new ListItem("--Chưa rõ--", "00000000-0000-0000-0000-000000000000"));
            DropDownListGeo3.DataBind();
        }

        if (geoID3 == null) DropDownListGeo3.SelectedIndex = 0;
        else DropDownListGeo3.SelectedValue = geoID3.ToString();
    }

    protected void GridViewLocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Select":

                Guid ID = new Guid(e.CommandArgument.ToString());

                txtLocationID.Text = ID.ToString();

                CustomerLocation l = locationBLL.Get_byID(ID);

                if (l == null) return;

                txtName.Text = l.Name;
                txtPhone.Text = l.Phone;
                txtFax.Text = l.Fax;

                SelectGeos(l.GeoID1, l.GeoID2, l.GeoID3);

                txtAddess.Text = l.Address;

                break;
            case "":

                break;
        }
    }

    protected void GridViewLocation_DataBinding(object sender, EventArgs e)
    {
        if (LocationChanged != null) LocationChanged(sender, e);

    }
    protected void GridViewContact_DataBinding(object sender, EventArgs e)
    {
        if (LocationChanged != null) LocationChanged(sender, e);
    }
    protected void ImageButtonNewLocation_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtCustomerID.Text) || new Guid(txtCustomerID.Text) == Guid.Empty)
            return;

        RedBloodDataContext db = new RedBloodDataContext();

        CustomerLocation lo = new CustomerLocation();
        lo.CustomerID = new Guid(txtCustomerID.Text);
        lo.Name = "Tên chi nhánh";

        db.CustomerLocations.InsertOnSubmit(lo);
        db.SubmitChanges();

        GridViewLocation.DataBind();
    }
    protected void ImageButtonSave_Click(object sender, EventArgs e)
    {
        if (GridViewLocation.SelectedValue == null)
            return;

        Guid? geoID1;
        if (new Guid(DropDownListGeo1.SelectedValue) == Guid.Empty)
            geoID1 = null;
        else
            geoID1 = new Guid(DropDownListGeo1.SelectedValue);

        Guid? geoID2;
        if (new Guid(DropDownListGeo2.SelectedValue) == Guid.Empty)
            geoID2 = null;
        else
            geoID2 = new Guid(DropDownListGeo2.SelectedValue);

        Guid? geoID3;
        if (new Guid(DropDownListGeo3.SelectedValue) == Guid.Empty)
            geoID3 = null;
        else
            geoID3 = new Guid(DropDownListGeo3.SelectedValue);

        locationBLL.Update((Guid)GridViewLocation.SelectedValue, txtName.Text, txtPhone.Text, txtFax.Text, geoID1, geoID2, geoID3, txtAddess.Text);

        GridViewLocation.DataBind();
        GridViewContact.DataBind();

    }
    protected void ImageButtonNewContact_Click(object sender, EventArgs e)
    {
        if (GridViewLocation.SelectedValue == null)
        {
            ActionStatus.Text = "Chọn chi nhánh trước khi thêm người liên hệ.";
        }
        else
        {
            ActionStatus.Text = "";
            contactBLL.Insert((Guid)GridViewLocation.SelectedValue);
            GridViewContact.DataBind();
        }
    }
    protected void ImageButtonRefeshGeo1_Click(object sender, EventArgs e)
    {
        DropDownListGeo1.Items.Clear();
        DropDownListGeo1.Items.Add(new ListItem("--Chưa rõ--", "00000000-0000-0000-0000-000000000000"));
        DropDownListGeo1.DataBind();

        DropDownListGeo2.Items.Clear();
        DropDownListGeo2.Items.Add(new ListItem("--Chưa rõ--", "00000000-0000-0000-0000-000000000000"));
        DropDownListGeo2.DataBind();

        DropDownListGeo3.Items.Clear();
        DropDownListGeo3.Items.Add(new ListItem("--Chưa rõ--", "00000000-0000-0000-0000-000000000000"));
        DropDownListGeo3.DataBind();
    }

    protected void GridViewLocation_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            ScriptManager.RegisterStartupScript(GridViewLocation, this.GetType(), "openpopup", "alert('Không thể xóa. Còn các thông tin khác liên quan.');", true);
            e.ExceptionHandled = true;
        }
    }
}
