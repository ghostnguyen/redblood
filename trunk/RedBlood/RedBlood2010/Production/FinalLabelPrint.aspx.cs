using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
namespace RedBlood.Production
{
    public partial class FinalLabelPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PrintSettingBLL.Reload();

            string strPackList = Request["PackList"];
            List<Guid> packIDList = new List<Guid>();
            if (!string.IsNullOrEmpty(strPackList))
            {
                var strArr = strPackList.Split(',');
                foreach (var item in strArr)
                {
                    Guid id;
                    if (Guid.TryParse(item, out id))
                    {
                        packIDList.Add(id);
                    }
                }
            }

            RedBloodDataContext db = new RedBloodDataContext();
            var pL = db.Packs.Where(r => r.Donation.TestResultStatus == Donation.TestResultStatusX.Negative
                && packIDList.Contains(r.ID))
                .OrderBy(r => r.ProductCode)
                .ThenBy(r => r.DIN).ToList();

            foreach (var item in pL)
            {
                Panel p = new Panel();
                p.Style.Add("position", "relative");
                p.Style.Add("page-break-after", "always");
                p.Style.Apply(PrintSettingBLL.FinalLabel.PaperSize);
                p.Style.Add("border", "1px solid white");
                divCon.Controls.Add(p);

                AddControl(item, p);
            }
        }

        void AddControl(Pack item, Panel panel)
        {
            var uc = (FinalLabelUserControl)LoadControl("~/UserControl/FinalLabelUserControl.ascx");
            uc.Fill_Letter(item);

            panel.Controls.Add(uc);
        }
    }
}