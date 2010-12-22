using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RedBlood;
using RedBlood.BLL;


namespace RedBlood.TestResult
{
    public partial class TestResult_Scan4Enter : System.Web.UI.Page
    {
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

        CampaignBLL campaignBLL = new CampaignBLL();
        PackBLL packBLL = new PackBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                string code = Master.TextBoxCode.Text.Trim();

                if (code.Length == 0) return;

                if (BarcodeBLL.IsValidDINCode(code))
                {
                    string DIN = BarcodeBLL.ParseDIN(code);
                    EnterDIN(DIN);
                }

                Master.TextBoxCode.Text = "";
            }
        }

        void DeletePack1_PackDeleted(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        private void EnterDIN(string DIN)
        {
            if (!DINInList.Contains(DIN))
            {
                var v = DonationBLL.Get(DIN);

                if (v.Pack == null)
                    throw new Exception("Không thu máu.");

                if (v.IsTRLocked)
                    throw new Exception("Túi máu bị khóa.");

                DINInList.Add(DIN);
                
                ShowData();
            }
        }

        private void ShowData()
        {
            GridView1.DataBind();
        }

        protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            DonationBLL bll = new DonationBLL();
            var v = bll.Get(DINInList.ToArray());
            e.Result = v;

            PanelAllNeg.Visible = v.Count > 0;
            lblTotal.Text = v.Count.ToString();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string DIN = (string)e.Keys[0];

            DonationBLL.Update(DIN, e.NewValues["Markers.HIV"].ToString(),
               e.NewValues["Markers.HCV_Ab"].ToString(),
               e.NewValues["Markers.HBs_Ag"].ToString(),
                e.NewValues["Markers.Syphilis"].ToString(),
               e.NewValues["Markers.Malaria"].ToString(),
                "");

            // It will be null if the groupbloodis NOT enter when collect blood.
            if (e.NewValues["BloodGroup"] != null)
            {
                DonationBLL.Update(DIN, e.NewValues["BloodGroup"].ToString(), "");
            }

            e.Cancel = true;
            GridView1.EditIndex = -1;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SetNegative")
            {
                DonationBLL.UpdateNegative(e.CommandArgument.ToString());
                GridView1.DataBind();
            }
        }
        protected void btnAllNegative_Click(object sender, EventArgs e)
        {
            //foreach (var item in DonationBLL.GetUnLock(CampaignDetail1.CampaignID))
            //{
            //    DonationBLL.UpdateNegative(item.DIN);
            //}
            //GridView1.DataBind();
        }
    }
}
