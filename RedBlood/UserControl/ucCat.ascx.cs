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

public partial class UserControl_Cat : System.Web.UI.UserControl
{
    public Guid? Cat1ID
    {
        get
        {
            return GetSelectedValue(DropDownListCat1);
        }
    }

    public Guid? Cat2ID
    {
        get
        {
            return GetSelectedValue(DropDownListCat2);
        }
    }

    public Guid? Cat3ID
    {
        get
        {
            return GetSelectedValue(DropDownListCat3);
        }
    }

    public Guid? Cat4ID
    {
        get
        {
            return GetSelectedValue(DropDownListCat4);
        }
    }

    public Guid? Cat5ID
    {
        get
        {
            return GetSelectedValue(DropDownListCat5);
        }
    }

    private Guid? GetSelectedValue(DropDownList ddl)
    {
        if (ddl.SelectedValue == null ||
                    ddl.SelectedValue == Guid.Empty.ToString())
            return null;
        return new Guid(ddl.SelectedValue);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void DropDownListCat1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reload_DDL(DropDownListCat2);
        Reload_DDL(DropDownListCat3);
        Reload_DDL(DropDownListCat4);
        Reload_DDL(DropDownListCat5);
    }
    protected void DropDownListCat2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reload_DDL(DropDownListCat3);
        Reload_DDL(DropDownListCat4);
        Reload_DDL(DropDownListCat5);
    }
    protected void DropDownListCat3_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reload_DDL(DropDownListCat4);
        Reload_DDL(DropDownListCat5);
    }
    protected void DropDownListCat4_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reload_DDL(DropDownListCat5);
    }
    protected void DropDownListCat5_SelectedIndexChanged(object sender, EventArgs e)
    { }

    private void Reload_DDL(DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("--Tất cả--", "00000000-0000-0000-0000-000000000000"));
        ddl.DataBind();
    }

    public void SelectCat(Guid? cat1ID, Guid? cat2ID, Guid? cat3ID, Guid? cat4ID, Guid? cat5ID)
    {
        if (cat1ID == null) DropDownListCat1.SelectedIndex = 0;
        else
        {
            DropDownListCat1.SelectedValue = cat1ID.ToString();

            Reload_DDL(DropDownListCat2);
            Reload_DDL(DropDownListCat3);
            Reload_DDL(DropDownListCat4);
            Reload_DDL(DropDownListCat5);
        }

        if (cat2ID == null) DropDownListCat2.SelectedIndex = 0;
        else
        {
            DropDownListCat2.SelectedValue = cat2ID.ToString();

            Reload_DDL(DropDownListCat3);
            Reload_DDL(DropDownListCat4);
            Reload_DDL(DropDownListCat5);
        }

        if (cat3ID == null) DropDownListCat3.SelectedIndex = 0;
        else
        {
            DropDownListCat3.SelectedValue = cat3ID.ToString();

            Reload_DDL(DropDownListCat4);
            Reload_DDL(DropDownListCat5);
        }

        if (cat4ID == null) DropDownListCat4.SelectedIndex = 0;
        else
        {
            DropDownListCat4.SelectedValue = cat4ID.ToString();

            Reload_DDL(DropDownListCat5);
        }

        if (cat5ID == null) DropDownListCat5.SelectedIndex = 0;
        else
        {
            DropDownListCat5.SelectedValue = cat5ID.ToString();
        }
    }
}
