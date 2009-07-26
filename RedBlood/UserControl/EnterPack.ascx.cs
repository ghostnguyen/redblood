using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class UserControl_EnterPack : System.Web.UI.UserControl
{
    public event EventHandler PlateletApheresisConfirmed;

    public Guid PeopleID
    {
        get
        {
            if (ViewState["PeopleID"] == null) return Guid.Empty;
            return (Guid)ViewState["PeopleID"];
        }
        set
        {
            ViewState["PeopleID"] = value;
            Load_EnterPack();
        }
    }

    public int Autonum
    {
        get
        {
            return (int)ViewState["AutoNum"];
        }
        set
        {
            ViewState["AutoNum"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Load_EnterPack()
    {
        Clear();

        if (PeopleID != Guid.Empty)
        {
            PackErr err = PackErrEnum.Non;
            Pack e = PackBLL.GetEnterPackByPeopleID(PeopleID, err);

            if (!err.Equals(PackErrEnum.Non))
            {
                lblPackMsg.Text = err.Message + " Không thể nhập túi máu mới.";
                return;
            }

            //people has enter pack
            if (e != null)
            {
                Autonum = e.Autonum;

                ImageCodabar.ImageUrl = BarcodeBLL.Url4Pack(e.Autonum);

                if (e.ComponentID != null)
                    DropDownListComponent.SelectedValue = e.ComponentID.ToString();

                if (e.SubstanceID != null)
                    DropDownListSubstance.SelectedValue = e.SubstanceID.ToString();

                if (e.Volume != null && e.Volume != 0)
                    DropDownListVolume.SelectByText(e.Volume.Value.ToString());

                if (e.ABOID != null && e.ABOID != 0)
                    DropDownListABO.SelectedValue = e.ABOID.ToString();

                if (e.RhID != null && e.RhID != 0)
                    DropDownListRH.SelectedValue = e.RhID.ToString();


                //if (e.Status == Pack.StatusX.CommitReceived)  //ABO test 1 Commited
                //{
                //    if (e.BloodTypes.Count == 1)
                //        lblPackMsg.Text = "Xác nhận thu máu ngày: " + e.CollectedDate.ToStringVN() + " - " + e.Actor;

                //    if (e.BloodTypes.Count == 0)
                //        lblPackMsg.Text = "Xác nhận thu máu (không có ABO test) ngày: " + e.CollectedDate.ToStringVN() + " - " + e.Actor;
                //}
                //else
                //{
                //}

                ucCampaign.CampaignID = e.CampaignID.Value;

            }
            //No Pack is assigned
            else
            {

            }

            ResetGUI(e);
        }
    }

    public void Assign(int autonum, int campaignID)
    {
        PackErr err = PackBLL.Assign(autonum, PeopleID, campaignID);
        if (err == PackErrEnum.Non)
        {
            Load_EnterPack();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi", "alert ('" + err.Message + "');", true);
        }
    }

    private void Clear()
    {
        ViewState["AutoNum"] = 0;

        ucCampaign.CampaignID = 0;

        ImageCodabar.ImageUrl = "none";
        lblPackMsg.Text = "";

        DropDownListComponent.SelectedIndex = 0;
        DropDownListSubstance.SelectedIndex = 0;
        DropDownListVolume.SelectedIndex = 0;

        DropDownListABO.SelectedIndex = 0;
        DropDownListRH.SelectedIndex = 0;

        DropDownListComponent.Enabled = false;
        DropDownListSubstance.Enabled = false;
        DropDownListVolume.Enabled = false;

        DropDownListABO.Enabled = false;
        DropDownListRH.Enabled = false;

        btnRemove.Visible = false;
        btnDelete.Visible = false;
        btnCommit.Visible = false;
        btnCommitWithout.Visible = false;

        btnCommitPlateleApheresis.Visible = false;
    }

    private void ResetGUI(Pack e)
    {
        btnRemove.Visible = false;
        btnDelete.Visible = false;

        btnCommit.Visible = false;
        btnCommitWithout.Visible = false;

        DropDownListComponent.Enabled = false;
        DropDownListSubstance.Enabled = false;
        DropDownListVolume.Enabled = false;

        DropDownListABO.Enabled = false;
        DropDownListRH.Enabled = false;

        btnCommitPlateleApheresis.Visible = false;

        if (e == null) return;

        if (e.Status == Pack.StatusX.Collected)
        {
            btnRemove.Visible = true;
            btnDelete.Visible = true;

            DropDownListComponent.Enabled = true;
            DropDownListSubstance.Enabled = true;
            DropDownListVolume.Enabled = true;

            DropDownListABO.Enabled = true;
            DropDownListRH.Enabled = true;

            btnCommitPlateleApheresis.Visible
                = (DropDownListComponent.SelectedValue.ToInt() == TestDef.Component.PlateletApheresis);

            if (DropDownListComponent.SelectedValue.ToInt() != 0
                && DropDownListVolume.SelectedValue.ToInt() != 0
                && DropDownListABO.SelectedValue.ToInt() != 0
                && DropDownListRH.SelectedValue.ToInt() != 0)
            {
                btnCommit.Visible = true;
            }
            else btnCommit.Visible = false;

            if (DropDownListComponent.SelectedValue.ToInt() != 0
                && DropDownListVolume.SelectedValue.ToInt() != 0
                && DropDownListABO.SelectedValue.ToInt() == 0
                && DropDownListRH.SelectedValue.ToInt() == 0)
            {
                btnCommitWithout.Visible = true;
            }
            else btnCommitWithout.Visible = false;
        }
    }

    void Update()
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Pack p = PackBLL.Get(db, Autonum, Pack.StatusX.Collected);

        if (p != null)
        {
            PackBLL.Update(db, p,
                DropDownListComponent.SelectedValue.ToInt(),
                DropDownListVolume.SelectedValue.ToIntNullable4Zero(),
                DropDownListSubstance.SelectedValue.ToInt());

            db.SubmitChanges();

            Load_EnterPack();
        }
    }

    protected void DropDownListComponent_SelectedIndexChanged(object sender, EventArgs e)
    {
        Update();
    }

    protected void DropDownListSubstance_SelectedIndexChanged(object sender, EventArgs e)
    {
        Update();
    }

    protected void DropDownListVolume_SelectedIndexChanged(object sender, EventArgs e)
    {
        Update();
    }

    protected void DropDownListABO_SelectedIndexChanged(object sender, EventArgs e)
    {
        Update();
    }

    protected void DropDownListRH_SelectedIndexChanged(object sender, EventArgs e)
    {
        Update();
    }

    protected void btnCommitWithout_Click(object sender, EventArgs e)
    {
        //bll.CommitEnterPack(Autonum, false, Page.User.Identity.Name);
        Load_EnterPack();
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        //bll.CommitEnterPack(Autonum, true, Page.User.Identity.Name);
        Load_EnterPack();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        PackBLL.DeletePack(null, Autonum, "DeleteEnterPack");
        Load_EnterPack();
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        PackBLL.RemovePeople(Autonum);
        Load_EnterPack();
    }

    protected void btnConfirmPlateleApheresis_Click(object sender, EventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Pack p = PackBLL.Get(db, Autonum, Pack.StatusX.Collected);

        p.TestResultStatus = Pack.TestResultStatusX.NegativeLocked;

        db.SubmitChanges();

        Load_EnterPack();

        if (PlateletApheresisConfirmed != null)
            PlateletApheresisConfirmed(null, null);
    }
}
