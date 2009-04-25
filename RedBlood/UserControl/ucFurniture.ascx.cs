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


public partial class UserControl_Furniture : System.Web.UI.UserControl
{
    FurnitureBLL bll = new FurnitureBLL();
    public event EventHandler SaveHandler;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Set_ID(Guid ID)
    {
        txtFurnitureID.Text = ID.ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string mess = bll.Update(txtFurnitureID.Text.ToGuid(), txtName.Text, txtCode.Text, txtSerialNumber.Text, txtDimension.Text, txtColor.Text, txtMaterial.Text, 
            txtUnit1Name.Text, txtUnit2Name.Text, txtUnit3Name.Text, txtUnit12Factor.Text.ToIntNullable(), txtUnit23Factor.Text.ToIntNullable(), 
            //ucCat.Cat1ID, ucCat.Cat2ID, ucCat.Cat3ID, ucCat.Cat4ID, ucCat.Cat5ID);
            ucCatTree.Cat1ID, ucCatTree.Cat2ID, ucCatTree.Cat3ID, ucCatTree.Cat4ID, ucCatTree.Cat5ID);

        if (!string.IsNullOrEmpty(mess))
            ActionStatus.Text = mess;
        else ActionStatus.Text = "";

        if (SaveHandler != null)
        {
            SaveHandler(sender, e);
        }
    }

    public void Furniture_Load()
    {
        Furniture item = bll.GetByID(txtFurnitureID.Text.ToGuid());
        if (item == null)
        {
            Furniture_Clear();
            return;
        }

        txtName.Text = item.Name;
        txtCode.Text = item.Code;
        txtSerialNumber.Text = item.SerialNumber;

        txtDimension.Text = item.Dimension;
        txtMaterial.Text = item.Material;
        txtColor.Text = item.Color;

        txtUnit1Name.Text = item.Unit1Name;
        txtUnit2Name.Text = item.Unit2Name;
        txtUnit3Name.Text = item.Unit3Name;

        txtUnit12Factor.Text = item.Unit12Factor.ToString();
        txtUnit23Factor.Text = item.Unit23Factor.ToString();

        if (item.FurCats.Count == 1)
        {
            ItemCat ic = item.FurCats.First();
            ucCatTree.SelectCat(ic.CatID1, ic.CatID2, ic.CatID3, ic.CatID4, ic.CatID5);

        }
    }
    public void Furniture_Clear()
    {
        txtName.Text = "";
        txtCode.Text = "";
        txtSerialNumber.Text = "";

        txtDimension.Text = "";
        txtMaterial.Text = "";
        txtColor.Text = "";

        txtUnit1Name.Text = "";
        txtUnit2Name.Text = "";
        txtUnit3Name.Text = "";

        txtUnit12Factor.Text = "";
        txtUnit23Factor.Text = "";

        ucCatTree.SelectCat(null, null, null, null, null);
    }
}

