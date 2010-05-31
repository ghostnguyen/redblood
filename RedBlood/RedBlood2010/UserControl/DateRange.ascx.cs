using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class UserControl_DateRange : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public DateTime? FromDate
    {
        get { return ucFromDate.Date; }
        set { ucFromDate.Date = value; }
    }
    public DateTime? ToDate
    {
        get { return ucToDate.Date; }
        set { ucToDate.Date = value; }
    }

    public bool Validated()
    {
        if (!FromDate.HasValue || !ToDate.HasValue || FromDate > ToDate)
        {
            throw new Exception("Khoảng thời gian không xác định.");
        }

        return true;
    }
}
