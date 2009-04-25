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

public partial class FurniturePage : System.Web.UI.Page
{
    FurnitureBLL bll = new FurnitureBLL();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ucFur1.SaveHandler += new EventHandler(ucFur1_btnSaveClick);
        if (!Page.IsPostBack)
        {
            
        }
    }

    void ucFur1_btnSaveClick(object sender, EventArgs e)
    {
        GridViewFur.DataBind();        
    }


    protected void LinkButtonNew_Click(object sender, EventArgs e)
    {
        Furniture item = bll.Insert(ucCat.Cat1ID, ucCat.Cat2ID, ucCat.Cat3ID, ucCat.Cat4ID, ucCat.Cat5ID);
        if (item != null)
        {
            ucFur1.Set_ID(item.ID);
            ucFur1.Furniture_Load();
            
            GridViewFur.SelectedIndex = -1;
            GridViewFur.DataBind();
        }
    }


    protected void btnFind_Click(object sender, EventArgs e)
    {
        GridViewFur.DataBind();
    }

    protected void GridViewFur_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Select":

                Guid ID = new Guid(e.CommandArgument.ToString());

                ucFur1.Set_ID(ID);
                ucFur1.Furniture_Load();

                break;
            case "":

                break;
        }
    }
    protected void GridViewFur_PageIndexChanged(object sender, EventArgs e)
    {
        GridViewFur.SelectedIndex = -1;
        ucFur1.Set_ID(Guid.Empty);
        ucFur1.Furniture_Load();
    }
    protected void LinqDataSourceFur_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var items = (from i in db.Furnitures
                     join ic in db.ItemCats on i.ID equals ic.ItemID

                     where i.Code.Contains(txtCode.Text)
                     && ((ic.CatID1 == ucCat.Cat1ID) || (ucCat.Cat1ID == null))
                     && ((ic.CatID2 == ucCat.Cat2ID) || (ucCat.Cat2ID == null))
                     && ((ic.CatID3 == ucCat.Cat3ID) || (ucCat.Cat3ID == null))
                     && ((ic.CatID4 == ucCat.Cat4ID) || (ucCat.Cat4ID == null))
                     && ((ic.CatID5 == ucCat.Cat5ID) || (ucCat.Cat5ID == null))

                     select i).Distinct();


        e.Result = items;
    }


}
