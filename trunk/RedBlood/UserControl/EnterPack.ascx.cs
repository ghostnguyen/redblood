using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class UserControl_EnterPack : System.Web.UI.UserControl
{
    PackBLL bll = new PackBLL();
    CodabarBLL codabarBLL = new CodabarBLL();

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
            PackErr err = PackErrList.Non;
            Pack e = bll.GetEnterPackByPeopleID(PeopleID, err);

            if (!err.Equals(PackErrList.Non))
            {
                lblPackMsg.Text = err.Message + " Không thể nhập túi máu mới.";
                return;
            }

            //people has enter pack
            if (e != null)
            {
                Autonum = e.Autonum;
                ImageCodabar.ImageUrl = "~/Codabar/Image.aspx?hasText=true&code=" + CodabarBLL.GenStringCode(Resources.Codabar.packSSC, e.Autonum.ToString());

                if (e.ComponentID != null)
                    DropDownListComponent.SelectedValue = e.ComponentID.ToString();

                if (e.Volume != null && e.Volume != 0)
                    DropDownListVolume.SelectByText(e.Volume.Value.ToString());

                if (e.BloodTypes.Count == 1)
                {
                    if (e.BloodTypes[0].aboID != null && e.BloodTypes[0].aboID != 0)
                        DropDownListABO.SelectedValue = e.BloodTypes[0].aboID.ToString();

                    if (e.BloodTypes[0].rhID != null && e.BloodTypes[0].rhID != 0)
                        DropDownListRH.SelectedValue = e.BloodTypes[0].rhID.ToString();
                }

                if (e.Status == Pack.StatusX.CommitReceived)  //ABO test 1 Commited
                {
                    if (e.BloodTypes.Count == 1)
                        lblPackMsg.Text = "Xác nhận thu máu ngày: " + e.CollectedDate.ToStringVN() + " - " + e.Actor;

                    if (e.BloodTypes.Count == 0)
                        lblPackMsg.Text = "Xác nhận thu máu (không có ABO test) ngày: " + e.CollectedDate.ToStringVN() + " - " + e.Actor;
                }
                else
                {
                }
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
        PackErr err = bll.Assign(autonum, PeopleID, Page.User.Identity.Name, campaignID);
        if (err == PackErrList.Non)
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
        DropDownListVolume.SelectedIndex = 0;

        DropDownListABO.SelectedIndex = 0;
        DropDownListRH.SelectedIndex = 0;

        DropDownListComponent.Enabled = false;
        DropDownListVolume.Enabled = false;

        DropDownListABO.Enabled = false;
        DropDownListRH.Enabled = false;

        btnRemove.Visible = false;
        btnDelete.Visible = false;
        btnCommit.Visible = false;
        btnCommitWithout.Visible = false;
    }

    private void ResetGUI(Pack e)
    {
        btnRemove.Visible = false;
        btnDelete.Visible = false;

        btnCommit.Visible = false;
        btnCommitWithout.Visible = false;

        DropDownListComponent.Enabled = false;
        DropDownListVolume.Enabled = false;

        DropDownListABO.Enabled = false;
        DropDownListRH.Enabled = false;

        if (e == null) return;

        if (e.Status == Pack.StatusX.Assign)
        {
            btnRemove.Visible = true;
            btnDelete.Visible = true;

            DropDownListComponent.Enabled = true;
            DropDownListVolume.Enabled = true;

            DropDownListABO.Enabled = true;
            DropDownListRH.Enabled = true;

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
        else if (e.Status == Pack.StatusX.CommitReceived)
        {
            btnDelete.Visible = true;
        }
    }

    void Update()
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Pack p = PackBLL.Get(Autonum, db, Pack.StatusX.Assign);

        if (p != null)
        {
            PackBLL.Update(p, DropDownListComponent.SelectedValue.ToIntNullable4Zero(),
                DropDownListVolume.SelectedValue.ToIntNullable4Zero());

            BloodTypeBLL.Update(db, p, 1
                , DropDownListABO.SelectedValue.ToIntNullable4Zero()
                , DropDownListRH.SelectedValue.ToIntNullable4Zero()
                , Page.User.Identity.Name, "");

            db.SubmitChanges();

            Load_EnterPack();
        }
    }

    protected void DropDownListComponent_SelectedIndexChanged(object sender, EventArgs e)
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
        bll.CommitEnterPack(Autonum, false, Page.User.Identity.Name);
        Load_EnterPack();
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        bll.CommitEnterPack(Autonum, true, Page.User.Identity.Name);
        Load_EnterPack();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        PackBLL.DeletePack(null, Autonum, "DeleteEnterPack", Page.User.Identity.Name);
        Load_EnterPack();
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        bll.RemovePeople(Autonum, Page.User.Identity.Name);
        Load_EnterPack();
    }



}
