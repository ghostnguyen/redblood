using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

public partial class EnterTestResult : System.Web.UI.Page
{
    
    TestDefBLL bll = new TestDefBLL();
    PackBLL packBLL = new PackBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.LoadComplete += new EventHandler(EnterTestResult_LoadComplete);
        ProcessCode();
    }

    void EnterTestResult_LoadComplete(object sender, EventArgs e)
    {
        //txtCode.Focus();
    }


    private void ProcessCode()
    {
        string code = txtCode.Text.Trim();

        txtCode.Text = "";

        if (code.Length < 3) return;

        string SSC = code.ParseCodabar()[0];
        string strID = code.ParseCodabar()[1];

        switch (SSC)
        {
            case "ab":

                //Pack e = packBLL.GetByAutonum(strID.ToInt());

                //if (e == null || e.PeopleID == null) return;

                //People1.PeopleID = e.PeopleID.Value;

                //txtPackID.Text = e.ID.ToString();

                //DetailsView1.DataBind();
                //GridViewTestResult.DataBind();

                break;

            case "at":

                Guid packID = txtPackID.Text.ToGuid();

                //TestResult r = bll.GetByAutonum(strID.ToInt());

                //if (r == null || packID == null) return;

                //if (r.Level.Value == 1)
                //{
                //    packTestResultBLL.UpdateTestResult(packID, r.ID, Guid.Empty);
                //}

                //if (r.Level.Value == 2)
                //{
                //    packTestResultBLL.UpdateTestResult(packID, r.ParentID.Value, r.ID);
                //}

                //GridViewTestResult.DataBind();

                break;
        }
    }

    protected void txtCode_TextChanged(object sender, EventArgs e)
    {

    }

    protected void LinqDataSourceTestResult_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        //e.Result = packBLL.GetTestResultByID(txtPackID.Text.ToGuid());
    }


    protected void GridViewTestResult_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Guid pointDefID = (Guid)e.Keys[0];

        //TextBox txt = (TextBox)GridViewCusPoint.Rows[e.RowIndex].FindControl("txtPointAdd");
        //int? value = txt.Text.ToIntNullable();

        //if (value.HasValue)
        //{
        //    bll.UpdatePoint(txtCustomerID.Text.ToGuid(), pointDefID, value.Value);
        //    GridViewCusPoint.DataBind();
        //    txt.Text = "";
        //}
    }
    protected void ddlTR2_SelectedIndexChanged(object sender, EventArgs e)
    {

        //DropDownList ddl = (DropDownList)sender;

        //// Has only one default value
        //if (ddl.Items.Count == 1) return;

        //Guid temp_tr2ID = ddl.Items[1].Value.ToGuid();
        //TestResult r = bll.GetByID(temp_tr2ID);
        //if (r == null || r.ParentID == null) return;

        //Guid tr1ID = r.ParentID.Value;
        //Guid tr2ID = ddl.SelectedValue.ToGuid();

        //Guid packID = txtPackID.Text.ToGuid();
        //if (packID == null) return;

        //packTestResultBLL.UpdateTestResult(packID, tr1ID, tr2ID);
    }

}
