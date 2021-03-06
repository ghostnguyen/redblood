﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

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
        //HttpApplication app = sender as HttpApplication;

        //if (app.Request.RawUrl.Contains("ResetPassword4Admin.aspx"))
        //{
        //    MembershipUser user = Membership.GetUser("admin");
        
        //    if (user.IsLockedOut)
        //    {
        //        user.UnlockUser();
        //    }

        //    app.Response.Write(user.ResetPassword());
        //}
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
