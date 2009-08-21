using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindAndReport_StoreCount : System.Web.UI.Page
{
    List<vw_ProductCount> list = new List<vw_ProductCount>();

    List<Product> wholeBloodproductList = new List<Product>();
       

    protected void Page_Load(object sender, EventArgs e)
    {
        Calc();
    }

    void Calc()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack.StatusX> statusList = new List<Pack.StatusX>() { Pack.StatusX.Expired, Pack.StatusX.Product };

        List<vw_ProductCount> list = db.vw_ProductCounts.Where(r => statusList.Contains(r.Status)).ToList();

        wholeBloodproductList.Add(new Product() { Code = "E0009V00" });
        wholeBloodproductList.Add(new Product() { Code = "E0023V00" });
        wholeBloodproductList.Add(new Product() { Code = "E0037V00" });
        wholeBloodproductList.Add(new Product() { Code = "E0052V00" });

        Calc4Product(wholeBloodproductList, lblFullNonTR, lblFullPos, lblFullNeg, lblFullExpire);
        //Calc4Component(TestDef.Component.RBC, Pack.StatusX.Product, lblRBCNonTR, lblRBCPos, lblRBCNeg, lblRBCDeliver, lblRBCExpire, lblRBCDelete);
        //Calc4Component(TestDef.Component.WBC, Pack.StatusX.Product, lblWBCNonTR, lblWBCPos, lblWBCNeg, lblWBCDeliver, lblWBCExpire, lblWBCDelete);
        //Calc4Component(TestDef.Component.FFPlasma, Pack.StatusX.Product, lblFFPlasmaNonTR, lblFFPlasmaPos, lblFFPlasmaNeg, lblFFPlasmaDeliver, lblFFPlasmaExpire, lblFFPlasmaDelete);
        //Calc4Component(TestDef.Component.Platelet, Pack.StatusX.Product, lblPlateletNonTR, lblPlateletPos, lblPlateletNeg, lblPlateletDeliver, lblPlateletExpire, lblPlateletDelete);
        //Calc4Component(TestDef.Component.FactorVIII, Pack.StatusX.Product, lblFactorVIIINonTR, lblFactorVIIIPos, lblFactorVIIINeg, lblFactorVIIIDeliver, lblFactorVIIIExpire, lblFactorVIIIDelete);
        //Calc4Component(TestDef.Component.FFPlasma_Poor, Pack.StatusX.Product, lblFFPlasmaPoorNonTR, lblFFPlasmaPoorPos, lblFFPlasmaPoorNeg, lblFFPlasmaPoorDeliver, lblFFPlasmaPoorExpire, lblFFPlasmaPoorDelete);
        //Calc4Component(TestDef.Component.PlateletApheresis, Pack.StatusX.Collected, lblPlateletApheresisNonTR, lblPlateletApheresisPos, lblPlateletApheresisNeg, lblPlateletApheresisDeliver, lblPlateletApheresisExpire, lblPlateletApheresisDelete);
    }

    void Calc4Product(List<Product> productList, Label lblNonTR, Label lblPos, Label lblNeg, Label lblExpire)
    {
        List<vw_ProductCount> listNonExpire = list.Where(r => r.Status == Pack.StatusX.Product).ToList();

        lblNonTR.Text = listNonExpire.Where(r => r.TestResultStatus == Donation.TestResultStatusX.Non).Count().ToStringRemoveZero();
        
        lblPos.Text = listNonExpire.Where(r => r.TestResultStatus == Donation.TestResultStatusX.Positive
            || r.TestResultStatus == Donation.TestResultStatusX.PositiveLocked).Count().ToStringRemoveZero();

        lblNeg.Text = listNonExpire.Where(r => r.TestResultStatus == Donation.TestResultStatusX.Negative
            || r.TestResultStatus == Donation.TestResultStatusX.NegativeLocked).Count().ToStringRemoveZero();

        if (productList == wholeBloodproductList)
        {
            //250
            lblFull250_AB_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.AB, TestDef.RH.Pos, 250);
            lblFull250_A_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.A, TestDef.RH.Pos, 250);
            lblFull250_B_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.B, TestDef.RH.Pos, 250);
            lblFull250_O_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.O, TestDef.RH.Pos, 250);

            lblFull250_AB_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.AB, TestDef.RH.Neg, 250);
            lblFull250_A_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.A, TestDef.RH.Neg, 250);
            lblFull250_B_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.B, TestDef.RH.Neg, 250);
            lblFull250_O_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.O, TestDef.RH.Neg, 250);

            //350
            lblFull350_AB_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.AB, TestDef.RH.Pos, 350);
            lblFull350_A_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.A, TestDef.RH.Pos, 350);
            lblFull350_B_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.B, TestDef.RH.Pos, 350);
            lblFull350_O_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.O, TestDef.RH.Pos, 350);

            lblFull350_AB_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.AB, TestDef.RH.Neg, 350);
            lblFull350_A_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.A, TestDef.RH.Neg, 350);
            lblFull350_B_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.B, TestDef.RH.Neg, 350);
            lblFull350_O_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.O, TestDef.RH.Neg, 350);

            //450
            lblFull450_AB_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.AB, TestDef.RH.Pos, 450);
            lblFull450_A_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.A, TestDef.RH.Pos, 450);
            lblFull450_B_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.B, TestDef.RH.Pos, 450);
            lblFull450_O_RhPos.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.O, TestDef.RH.Pos, 450);

            lblFull450_AB_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.AB, TestDef.RH.Neg, 450);
            lblFull450_A_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.A, TestDef.RH.Neg, 450);
            lblFull450_B_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.B, TestDef.RH.Neg, 450);
            lblFull450_O_RhNeg.Text = Calc_ABO_Rh(db, componentID, status, TestDef.ABO.O, TestDef.RH.Neg, 450);
        }

        //lblDeliver.Text = db.Packs.Where(r => r.ComponentID == componentID
        //    && r.DeliverStatus == Pack.DeliverStatusX.Yes)
        //    .Count().ToStringRemoveZero();

        lblExpire.Text = list.Where(r => r.Status == Pack.StatusX.Expired).Count().ToStringRemoveZero();

        //lblDelete.Text = db.Packs.Where(r => r.ComponentID == componentID
        //    && r.Status == Pack.StatusX.Delete)
        //    .Count().ToStringRemoveZero();
    }

    string Calc_ABO_Rh(List<vw_ProductCount> listNonExpire, int componentID, Pack.StatusX status, int abo, int rh, int volume)
    {
        //int count = db.Packs.Where(r => r.ComponentID == componentID
        //   && r.DeliverStatus == Pack.DeliverStatusX.Non
        //   && r.Status == status
        //   && r.Volume == volume
        //   &&
        //       (r.TestResultStatus == Pack.TestResultStatusX.Negative
        //       || r.TestResultStatus == Pack.TestResultStatusX.NegativeLocked)
        //   && r.RhID == rh
        //   && r.ABOID == abo)
        //   .Count();

        //if (count > 0)
        //{
        //    if (rh == TestDef.RH.Pos)
        //    {
        //        return count.ToString();
        //    }

        //    if (rh == TestDef.RH.Neg)
        //    {
        //        return count.ToString("0 Rh-") ;
        //    }
        //}

        return "";
    }
}
