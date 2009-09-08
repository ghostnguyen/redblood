﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_InvitationLetter : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Fill_Letter(Donation e)
    {
        LabelName.Text = e.People.Name;
        LabelDOB.Text = e.People.DOB.ToStringVN();
        LabelPackCode.Text = e.DIN;
        LabelAddress.Text = e.People.FullResidentalAddress;
        LabelCollectedDate.Text = e.CollectedDate.ToStringVN();
        LabelDate.Text = DateTime.Now.AddMonthsAvoidWeekend(1).ToStringVN();
    }
}