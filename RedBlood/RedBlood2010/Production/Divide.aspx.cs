using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;

namespace RedBlood.Production
{
    public partial class Divide : System.Web.UI.Page
    {
        public List<string> ProductCodeInList
        {
            get
            {
                if (ViewState["ProductCodeInList"] == null)
                {
                    ViewState["ProductCodeInList"] = new List<string>();
                }
                return (List<string>)ViewState["ProductCodeInList"];
            }
            set
            {
                ViewState["ProductCodeInList"] = value;
            }
        }

        public List<RedBlood.BLL.ProductionBLL.Division> DivisionList
        {
            get
            {
                if (ViewState["DivisionList"] == null)
                {
                    ViewState["DivisionList"] = new List<RedBlood.BLL.ProductionBLL.Division>();
                }
                return (List<RedBlood.BLL.ProductionBLL.Division>)ViewState["DivisionList"];
            }
            set
            {
                ViewState["DivisionList"] = value;
            }
        }

        public List<string> DINInList
        {
            get
            {
                if (ViewState["DINInList"] == null)
                {
                    ViewState["DINInList"] = new List<string>();
                }
                return (List<string>)ViewState["DINInList"];
            }
            set
            {
                ViewState["DINInList"] = value;
            }
        }

        public ProductionBLL productionBLL
        {
            get
            {
                return new ProductionBLL()
                {
                    ProductCodeInList = ProductCodeInList,
                    DivisionList = DivisionList,
                    DINInList = DINInList
                };
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            string code = Master.TextBoxCode.Text.Trim();
            Master.TextBoxCode.Text = "";

            if (code.Length == 0) return;


            if (rdbProductCodeIn.Checked)
            {
                if (BarcodeBLL.IsValidProductCode(code))
                {
                    ProductCodeInList = productionBLL.AddProductCodeIn4Divide(BarcodeBLL.ParseProductCode(code));
                    DataListProductIn.DataBind();

                    GridViewVolume.DataSource = ProductionBLL.GetDivideList(code).Select(r => new
                    {
                        Division = r,
                        Volume = "",
                    });
                    GridViewVolume.DataBind();
                }
            }
            else if (rdbDINIn.Checked)
            {
                if (BarcodeBLL.IsValidDINCode(code))
                {
                    DINInList = productionBLL.AddDIN4Divide(BarcodeBLL.ParseDIN(code));
                    DataListDINIn.DataBind();
                }
            }
        }

        protected void LinqDataSourceProductIn_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            string productCode = ProductCodeInList.FirstOrDefault();
            if (string.IsNullOrEmpty(productCode))
            {
                e.Cancel = true;
            }
            else
            {
                string productCodeShort = productCode.Substring(0, productCode.Length - 2);
                e.Result = db.Products.Where(r => r.Code.Contains(productCodeShort)).Select(r => new
                {
                    Code = productCode,
                    r.Description
                });
            }

        }

        protected void LinqDataSourceDINIn_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            var list = db.Donations.Where(r => DINInList.Contains(r.DIN)).ToList();
            list.Reverse();
            e.Result = list;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ProductCodeInList.Clear();
            DataListProductIn.DataBind();

            GridViewVolume.DataBind();

            DINInList.Clear();
            DataListDINIn.DataBind();

            rdbDINIn.Checked = false;
            rdbProductCodeIn.Checked = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var list = new List<ProductionBLL.Division>();

            foreach (var item in GridViewVolume.Rows)
            {
                var row = item as GridViewRow;
                var label = row.Cells[0].Controls[1] as Label;
                var txtVolume = row.Cells[1].Controls[1] as TextBox;
                
                if (!string.IsNullOrEmpty(label.Text)
                    && txtVolume.Text.ToInt() > 0)
                {
                    ProductionBLL.Division a = new ProductionBLL.Division() { Ext = label.Text, Volume = txtVolume.Text.ToInt() };
                    list.Add(a);
                }
            }

            if (list.Count > 0)
            {
                DivisionList = list;
                productionBLL.Divide();
                this.Alert("Tách thành công.");
            }
        }

        protected void btnDINRemove_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;

            if (btn != null)
            {
                DINInList.Remove(btn.CommandArgument);
                DataListDINIn.DataBind();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;

            if (btn != null)
            {
                DINInList.Remove(btn.CommandArgument);
                DataListDINIn.DataBind();
            }
        }
    }
}