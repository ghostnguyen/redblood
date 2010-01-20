using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_PackCountByProvince : System.Web.UI.UserControl
{
    public Guid ProvinceID
    {
        get
        {
            if (ViewState["ProvinceID"] == null)
                return new Guid();
            else
                return (Guid)ViewState["ProvinceID"];
        }
        set
        {
            ViewState["ProvinceID"] = value;
        }
    }

    public DateTime? From
    {
        get
        {
            return (DateTime?)ViewState["From"];
        }
        set
        {
            ViewState["From"] = value;
        }
    }

    public DateTime? To
    {
        get
        {
            return (DateTime?)ViewState["To"];
        }
        set
        {
            ViewState["To"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.LoadComplete += new EventHandler(Page_LoadComplete);
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        Count();
    }

    public void Count()
    {
        List<int> camIDL = CampaignBLL.Get(new List<Guid>() { ProvinceID }, From, To, Campaign.TypeX.Short_run).Select(r => r.ID).ToList();
        this.Visible = camIDL.Count != 0;

        //Calc4Component(camIDL, TestDef.Component.Full, Pack.StatusX.Collected, lblFullNonTR, lblFullPos, lblFullNeg, lblFullDeliver, lblFullExpire, lblFullDelete);
        //Calc4Component(camIDL, TestDef.Component.PlateletApheresis, Pack.StatusX.Collected, lblPlateletApheresisNonTR, lblPlateletApheresisPos, lblPlateletApheresisNeg, lblPlateletApheresisDeliver, lblPlateletApheresisExpire, lblPlateletApheresisDelete);
    }

    void Calc4Component(List<int> camIDL, int componentID, Pack.StatusX status, Label lblNonTR, Label lblPos, Label lblNeg, Label lblDeliver, Label lblExpire, Label lblDelete)
    {
        //RedBloodDataContext db = new RedBloodDataContext();



        //List<Pack> l = db.Packs.Where(r => r.ComponentID == componentID && r.NonNegativeTestResult
        //    && camIDL.Contains(r.CampaignID.Value)
        //    ).ToList();

        //List<Pack> l1 = l.Where(r => r.Status == status).ToList();

        //lblNonTR.Text = l1.Where(r =>
        //    r.DeliverStatus == Pack.DeliverStatusX.Non
        //    && r.TestResultStatus == Pack.TestResultStatusX.Non)
        //    .Count().ToStringRemoveZero();

        //lblPos.Text = l.Where(r =>
        //                    (r.TestResultStatus == Pack.TestResultStatusX.Positive
        //        || r.TestResultStatus == Pack.TestResultStatusX.PositiveLocked))
        //    .Count().ToStringRemoveZero();

        //lblNeg.Text = l1.Where(r =>
        //        (r.TestResultStatus == Pack.TestResultStatusX.Negative
        //        || r.TestResultStatus == Pack.TestResultStatusX.NegativeLocked))
        //    .Count().ToStringRemoveZero();

        //if (componentID == TestDef.Component.Full)
        //{

        //    List<Pack> l3 = l1.Where(r => r.TestResultStatus == Pack.TestResultStatusX.Negative
        //       || r.TestResultStatus == Pack.TestResultStatusX.NegativeLocked).ToList();

        //    //250
        //    List<Pack> l250 = l3.Where(r => r.Volume == 250).ToList();

        //    lblFull250_AB_RhPos.Text = Calc_ABO_Rh(l250, TestDef.ABO.AB, TestDef.RH.Pos);
        //    lblFull250_A_RhPos.Text = Calc_ABO_Rh(l250, TestDef.ABO.A, TestDef.RH.Pos);
        //    lblFull250_B_RhPos.Text = Calc_ABO_Rh(l250, TestDef.ABO.B, TestDef.RH.Pos);
        //    lblFull250_O_RhPos.Text = Calc_ABO_Rh(l250, TestDef.ABO.O, TestDef.RH.Pos);

        //    lblFull250_AB_RhNeg.Text = Calc_ABO_Rh(l250, TestDef.ABO.AB, TestDef.RH.Neg);
        //    lblFull250_A_RhNeg.Text = Calc_ABO_Rh(l250, TestDef.ABO.A, TestDef.RH.Neg);
        //    lblFull250_B_RhNeg.Text = Calc_ABO_Rh(l250, TestDef.ABO.B, TestDef.RH.Neg);
        //    lblFull250_O_RhNeg.Text = Calc_ABO_Rh(l250, TestDef.ABO.O, TestDef.RH.Neg);

        //    //350
        //    List<Pack> l350 = l3.Where(r => r.Volume == 350).ToList();

        //    lblFull350_AB_RhPos.Text = Calc_ABO_Rh(l350, TestDef.ABO.AB, TestDef.RH.Pos);
        //    lblFull350_A_RhPos.Text = Calc_ABO_Rh(l350, TestDef.ABO.A, TestDef.RH.Pos);
        //    lblFull350_B_RhPos.Text = Calc_ABO_Rh(l350, TestDef.ABO.B, TestDef.RH.Pos);
        //    lblFull350_O_RhPos.Text = Calc_ABO_Rh(l350, TestDef.ABO.O, TestDef.RH.Pos);

        //    lblFull350_AB_RhNeg.Text = Calc_ABO_Rh(l350, TestDef.ABO.AB, TestDef.RH.Neg);
        //    lblFull350_A_RhNeg.Text = Calc_ABO_Rh(l350, TestDef.ABO.A, TestDef.RH.Neg);
        //    lblFull350_B_RhNeg.Text = Calc_ABO_Rh(l350, TestDef.ABO.B, TestDef.RH.Neg);
        //    lblFull350_O_RhNeg.Text = Calc_ABO_Rh(l350, TestDef.ABO.O, TestDef.RH.Neg);

        //    //450
        //    List<Pack> l450 = l3.Where(r => r.Volume == 450).ToList();

        //    lblFull450_AB_RhPos.Text = Calc_ABO_Rh(l450, TestDef.ABO.AB, TestDef.RH.Pos);
        //    lblFull450_A_RhPos.Text = Calc_ABO_Rh(l450, TestDef.ABO.A, TestDef.RH.Pos);
        //    lblFull450_B_RhPos.Text = Calc_ABO_Rh(l450, TestDef.ABO.B, TestDef.RH.Pos);
        //    lblFull450_O_RhPos.Text = Calc_ABO_Rh(l450, TestDef.ABO.O, TestDef.RH.Pos);

        //    lblFull450_AB_RhNeg.Text = Calc_ABO_Rh(l450, TestDef.ABO.AB, TestDef.RH.Neg);
        //    lblFull450_A_RhNeg.Text = Calc_ABO_Rh(l450, TestDef.ABO.A, TestDef.RH.Neg);
        //    lblFull450_B_RhNeg.Text = Calc_ABO_Rh(l450, TestDef.ABO.B, TestDef.RH.Neg);
        //    lblFull450_O_RhNeg.Text = Calc_ABO_Rh(l450, TestDef.ABO.O, TestDef.RH.Neg);
        //}

        //lblDeliver.Text = l.Where(r =>
        //    r.DeliverStatus == Pack.DeliverStatusX.Yes)
        //    .Count().ToStringRemoveZero();

        //lblExpire.Text = l.Where(r =>
        //     r.Status == Pack.StatusX.Expired)
        //    .Count().ToStringRemoveZero();

        //lblDelete.Text = l.Where(r =>
        //    r.Status == Pack.StatusX.Delete)
        //    .Count().ToStringRemoveZero();
    }

    string Calc_ABO_Rh(List<Pack> l, int abo, int rh)
    {
        //int count = l
        //   .Where(r => r.RhID == rh && r.ABOID == abo)
        //   .Count();

        //if (count > 0)
        //{
        //    if (rh == TestDef.RH.Pos)
        //    {
        //        return count.ToString();
        //    }

        //    if (rh == TestDef.RH.Neg)
        //    {
        //        return count.ToString() + " (Rh-)";
        //    }
        //}

        return "";
    }
}
