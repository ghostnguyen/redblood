﻿<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        SystemBLL.SOD();
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
        if (string.IsNullOrEmpty(CodabarBLL.CodabarImgPage))
        {
            string[] split = Request.Url.AbsoluteUri.Split('/');
            CodabarBLL.CodabarImgPage = split[0] + "/" + split[1] + "/" + split[2] + "/" + split[3] + "/Codabar/Image.aspx";
            CodabarBLL.CodabarImgPage = CodabarBLL.CodabarImgPage.Replace("0.0.0.0","localhost");
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

