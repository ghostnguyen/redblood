using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemBLL.SOD();


        if (string.IsNullOrEmpty(CodabarBLL.RootUrl))
        {
            string[] split = Request.Url.AbsoluteUri.Split('/');
            CodabarBLL.RootUrl = split[0] + "/" + split[1] + "/" + split[2] + "/" + split[3] + "/Codabar/Image.aspx";
        }

        //GeoBLL bll = new GeoBLL();
        //bll.UpdateFullname();

        //GeoBLL bll = new GeoBLL();
        //StreamReader reader = new StreamReader(@"E:\\TinhThanh.txt");
        //string lastPro = "";
        //string currentProIDStr = "";
        //while (reader.Peek() > 0)
        //{
        //    string line = reader.ReadLine();
        //    string[] list = line.Split('\t');

        //    string currentPro = list[2].Trim();


        //    if (currentPro != lastPro)
        //    {
        //        lastPro = currentPro;
        //        currentProIDStr = bll.Insert(currentPro, 1, null);
        //    }

        //    string currentDist = (list[3].Trim()+ " " + list[1].Trim()).Trim();
        //    if (currentProIDStr != "" && currentDist != "")
        //        bll.Insert(currentDist, 2, currentProIDStr.ToGuid());
        //}
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //ListBox1.DataBind();
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        //string[] search = TextBox1.Text.Split(' ');
        //string search1 = TextBox1.Text;
        //string search2 = "%" + TextBox1.Text.Replace(" ", "%") + "%";
        //RedBloodDataContext db = new RedBloodDataContext();

        //var r = from rs in db.Geos
        //        // where search.Contains(rs.Fullname)
        //        //where rs.Fullname.Contains(search1)
        //        where SqlMethods.Like(rs.Fullname, search2) || SqlMethods.Like(rs.FullnameNoDiacritics, search2)
        //        select rs;
        //e.Result = r;
        
    }

    
}
