using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ThanksLetter : System.Web.UI.UserControl
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

        if (e.Markers.HIV == TR.na.Name)
            LabelHIV.Text = "Không có";
        else if (e.Markers.HIV == TR.neg.Name)
            LabelHIV.Text = "Âm tính";
        else if (e.Markers.HIV == TR.pos.Name)
            LabelHIV.Text = "Dương tính";
        else
            LabelHIV.Text = e.Markers.HIV;

        if (e.Markers.HCV_Ab == TR.na.Name)
            LabelHCV.Text = "Không có";
        else if (e.Markers.HCV_Ab == TR.neg.Name)
            LabelHCV.Text = "Âm tính";
        else if (e.Markers.HCV_Ab == TR.pos.Name)
            LabelHCV.Text = "Dương tính";
        else
            LabelHCV.Text = e.Markers.HCV_Ab;

        if (e.Markers.HBs_Ag == TR.na.Name)
            LabelHBsAg.Text = "Không có";
        else if (e.Markers.HBs_Ag == TR.neg.Name)
            LabelHBsAg.Text = "Âm tính";
        else if (e.Markers.HBs_Ag == TR.pos.Name)
            LabelHBsAg.Text = "Dương tính";
        else
            LabelHBsAg.Text = e.Markers.HBs_Ag;

        if (e.Markers.Malaria == TR.na.Name)
            LabelMalaria.Text = "Không có";
        else if (e.Markers.Malaria == TR.neg.Name)
            LabelMalaria.Text = "Âm tính";
        else if (e.Markers.Malaria == TR.pos.Name)
            LabelMalaria.Text = "Dương tính";
        else
            LabelMalaria.Text = e.Markers.Malaria;

        if (e.Markers.Syphilis == TR.na.Name)
            LabelSyphilis.Text = "Không có";
        else if (e.Markers.Syphilis == TR.neg.Name)
            LabelSyphilis.Text = "Âm tính";
        else if (e.Markers.Syphilis == TR.pos.Name)
            LabelSyphilis.Text = "Dương tính";
        else
            LabelSyphilis.Text = e.Markers.Syphilis;

        LabelABO_Rh.Text = BloodGroupBLL.GetDescription(e.BloodGroup);
    }
}
