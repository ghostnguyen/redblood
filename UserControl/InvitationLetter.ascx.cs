using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_InvitationLetter : System.Web.UI.UserControl
{
    public Guid TestResultID
    {
        get
        {
            if (ViewState["TestResultID"] == null) return Guid.Empty;
            return (Guid)ViewState["TestResultID"];
        }
        set
        {
            ViewState["TestResultID"] = value;
            //Fill_Letter();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Fill_Letter(Pack e)
    {
        LabelName.Text = e.People.Name;
        LabelDOB.Text = e.People.DOB.ToStringVN();
        LabelPackCode.Text = CodabarBLL.GenPackCode(e.Autonum);
        LabelAddress.Text = e.People.FullResidentalAddress;
        LabelCollectedDate.Text = e.CollectedDate.ToStringVN();
        LabelDate.Text = DateTime.Now.AddMonthsAvoidWeekend(1).ToStringVN();
    }
}
