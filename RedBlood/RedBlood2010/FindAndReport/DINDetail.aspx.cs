using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class FindAndReport_DINDetail : System.Web.UI.Page
{
    public string DIN
    {
        get
        {
            return (string)ViewState["DIN"];
        }
        set
        {
            ViewState["DIN"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DIN = Request.Params["key"];

            var v = DonationBLL.Get(DIN);
            if (v != null && v.PeopleID.HasValue)
            {
                People1.PeopleID = v.PeopleID.Value;
                PeopleHistory1.PeopleID = v.PeopleID.Value;

                GridViewPacks.DataSource = v.Packs.Select(r => new
                {
                    r.ID,
                    r.DIN,
                    r.ProductCode,
                    r.Status,
                    Date = r.Date.ToStringVN_Hour(),
                    ExpirationDate = r.ExpirationDate.ToStringVN_Hour(),
                    r.Note,
                    r.Volume,
                }).OrderBy(r => r.Date);
                GridViewPacks.DataBind();

                GridViewDINLog.DataSource = v.DonationTestLogs.ToList()
                    .Select(r => new
                {
                    r.ID,
                    Date = r.Date.ToStringVN_Hour(),
                    r.Actor,
                    r.Type,
                    Result = GetResult(r.Type, r.Result),
                }).OrderByDescending(r => r.Date);
                GridViewDINLog.DataBind();
            }
        }
    }

    public string GetResult(string type, string result)
    {
        string str = "";

        if (type == "BloodGroup")
            str = BloodGroupBLL.GetDescription(result);

        if (type == "Markers")
        {
            if (InfectiousMarker.IsValidate(result))
            {
                InfectiousMarker maker = new InfectiousMarker();
                maker.Code = result;
                str = maker.Description;
            }
            else
            {
                str = "";
            }

        }

        return str;
    }
    protected void btnSelectedPack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Production/FinalLabelPrint.aspx?PackList=" + GetSelectedPack());
    }

    string GetSelectedPack()
    {
        string selected = "";
        foreach (GridViewRow item in GridViewPacks.Rows)
        {
            CheckBox chk = item.Cells[8].Controls[1] as CheckBox;

            if (chk != null && chk.Checked)
            {
                //selected += item.Cells[1].Text + ",";
                selected += GridViewPacks.DataKeys[item.DataItemIndex].Value.ToString() + ",";
            }
        }
        return selected;
    }


}
