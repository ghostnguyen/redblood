using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AuthenticationHttpModule
/// </summary>
public class AuthenticationHttpModule : IHttpModule
{
    public AuthenticationHttpModule()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region IHttpModule Members

    public void Dispose()
    {
        //throw new NotImplementedException();
    }

    public void Init(HttpApplication context)
    {
        context.BeginRequest += new EventHandler(context_BeginRequest);
        context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
        context.PostAuthenticateRequest += new EventHandler(context_PostAuthenticateRequest);
    }

    void context_PostAuthenticateRequest(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
        HttpApplication app = sender as HttpApplication;

        if (app.Request.RawUrl.Contains("Login.aspx")) return;

        if (!app.Request.IsAuthenticated)
            app.Response.Redirect("~/Login.aspx", true);
    }

    void context_AuthenticateRequest(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
      
    }

    void context_BeginRequest(object sender, EventArgs e)
    {
        
    }

    

    #endregion
}
