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

public partial class WarehousePage : System.Web.UI.Page
{
    CompanyBLL companyBLL = new CompanyBLL();
    WarehouseBLL bll = new WarehouseBLL();
    WarehouseDivisionBLL whDivisionBLL = new WarehouseDivisionBLL();
    WarehouseKeeperBLL whKeeperBLL = new WarehouseKeeperBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        Company com = companyBLL.Select_First();
        if (com != null) txtCompanyID.Text = com.ID.ToString();
    }

    protected void LinkButtonDelete_Click(object sender, EventArgs e)
    {
        if (GridViewWarehouseList.SelectedValue != null)
        {
            int i = bll.Delete(new Guid(GridViewWarehouseList.SelectedValue.ToString()));

            //Can not delete
            if (i != 0)
            {
                ScriptManager.RegisterStartupScript(LinkButtonDelete, this.GetType(), "openpopup", "alert('Không thể xóa. Còn các thông tin khác liên quan.');", true);
            }
            else
            {
                GridViewWarehouseList.DataBind();
                //UpdateWarehouseID();
            }
        }
    }

    protected void LinkButtonNew_Click(object sender, EventArgs e)
    {
        bll.Insert();
        GridViewWarehouseList.DataBind();
        //UpdateWarehouseID();
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        GridViewWarehouseList.DataBind();
        //UpdateCustomerID();
    }
    protected void DetailsViewWarehouse_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            ActionStatus.Text = e.Exception.Message;
            e.KeepInEditMode = true;
            e.ExceptionHandled = true;
        }
        else
        {
            ActionStatus.Text = "";
            GridViewWarehouseList.DataBind();
        }
    }
    protected void LinkButtonNewWarehouseKeeper_Click(object sender, EventArgs e)
    {
        if (GridViewWarehouseList.SelectedValue == null) return;

        whKeeperBLL.Insert((Guid)GridViewWarehouseList.SelectedValue);

        GridViewWarehouseKeeper.DataBind();
    }
    protected void LinkButtonNewWarehouseDivision_Click(object sender, EventArgs e)
    {
        if (GridViewWarehouseList.SelectedValue == null) return;

        whDivisionBLL.Insert((Guid)GridViewWarehouseList.SelectedValue);

        GridViewWarehouseDivision.DataBind();
    }
    protected void GridViewWarehouseDivision_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            ActionStatusViTriKho.Text = e.Exception.Message;
            e.KeepInEditMode = true;
            e.ExceptionHandled = true;
        }
        else
        {
            ActionStatusViTriKho.Text = "";
            GridViewWarehouseList.DataBind();
        }
    }
}
