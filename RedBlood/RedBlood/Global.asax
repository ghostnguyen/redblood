﻿<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        RedBloodSystemBLL.SOD();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
               

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        if (string.IsNullOrEmpty(BarcodeBLL.BarcodeImgPage))
        {
            ////System.Web.VirtualPathUtility.
            //string[] split = Request.Url.AbsoluteUri.Split('/');

            //RedBloodSystem.RootUrl = split[0] + "/" + split[1] + "/" + split[2] + "/" + split[3];
            //RedBloodSystem.RootUrl = RedBloodSystem.RootUrl.Replace("0.0.0.0", "localhost");
            
            //BarcodeBLL.BarcodeImgPage = RedBloodSystem.RootUrl + "/Barcode/Image.aspx";

            BarcodeBLL.BarcodeImgPage = System.Web.VirtualPathUtility.ToAbsolute("~/Barcode/Image.aspx");
        }
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>

