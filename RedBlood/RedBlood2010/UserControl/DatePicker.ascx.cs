using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class UserControl_DatePicker : System.Web.UI.UserControl
{
    public DateTime? Date
    {
        get
        {
            return txtDate.Text.Trim().ToDatetimeFromVNFormat();
        }
        set
        {
            txtDate.Text = value.HasValue ? value.Value.ToStringVN() : "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    

}
