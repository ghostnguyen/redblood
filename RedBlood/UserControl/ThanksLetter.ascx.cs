﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ThanksLetter : System.Web.UI.UserControl
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


    public void Fill_Letter(TestResult e)
    {
        LabelName.Text = e.Pack.People.Name;
        LabelDOB.Text = e.Pack.People.DOB.ToStringVN();

        LabelPackCode.Text = e.Pack.Code;
        LabelAddress.Text = e.Pack.People.FullResidentalAddress;

        if (e.HIVID == null)
            LabelHIV.Text = "Không có";
        else if (e.HIVID == TestDef.HIV.Neg)
            LabelHIV.Text = "Âm tính";
        else if (e.HIVID == TestDef.HIV.Pos)
            LabelHIV.Text = "Dương tính";
        else
            LabelHIV.Text = e.HIV.Name;

        if (e.HCVID == null)
            LabelHCV.Text = "Không có";
        else if (e.HCVID == TestDef.HCV.Neg)
            LabelHCV.Text = "Âm tính";
        else if (e.HCVID == TestDef.HCV.Pos)
            LabelHCV.Text = "Dương tính";
        else
            LabelHCV.Text = e.HCV.Name;

        if (e.HBsAgID == null)
            LabelHBsAg.Text = "Không có";
        else if (e.HBsAgID == TestDef.HBsAg.Neg)
            LabelHBsAg.Text = "Âm tính";
        else if (e.HBsAgID == TestDef.HBsAg.Pos)
            LabelHBsAg.Text = "Dương tính";
        else
            LabelHBsAg.Text = e.HBsAg.Name;

        if (e.MalariaID == null)
            LabelMalaria.Text = "Không có";
        else if (e.MalariaID == TestDef.Malaria.Neg)
            LabelMalaria.Text = "Âm tính";
        else if (e.MalariaID == TestDef.Malaria.Pos)
            LabelMalaria.Text = "Dương tính";
        else
            LabelMalaria.Text = e.Malaria.Name;

        if (e.SyphilisID == null)
            LabelSyphilis.Text = "Không có";
        else if (e.SyphilisID == TestDef.Syphilis.Neg)
            LabelSyphilis.Text = "Âm tính";
        else if (e.SyphilisID == TestDef.Syphilis.Pos)
            LabelSyphilis.Text = "Dương tính";
        else
            LabelSyphilis.Text = e.Syphilis.Name;

        //BloodType bt = e.Pack.BloodTypes.Where(r => r.Times == 2).First();
        BloodType bt = e.Pack.BloodType2;

        if (bt == null) return;

        if (bt.ABO != null && bt.RH != null)
            LabelABO_Rh.Text = bt.ABO.Name + ", " + bt.RH.Name;
    }
}
