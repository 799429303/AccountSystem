using System;
using System.Web.Mvc;
using DataBase.WebHelper;
using System.Web;

namespace DataBase.Handler
{
    public  class LoginHandler: System.Web.Mvc.AuthorizeAttribute
    {
        public bool Ignore = true;
        public LoginHandler(bool ignore = true)
        {
            Ignore = ignore;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Ignore == false)
            {
                return;
            }
            if (string.IsNullOrEmpty(WebHelper.WebHelper.GetCookie("loginuserkey")) && HttpContext.Current.Session["loginuserkey"]==null)
            {
                DataBase.WebHelper.WebHelper.WriteCookie("nfine_login_error", "overdue");
                filterContext.HttpContext.Response.Write("<script>top.location.href = '/Login/Index';</script>");
                return;
            }
        }
   }
}
