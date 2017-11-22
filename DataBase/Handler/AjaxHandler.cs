using System;
using System.Web.Mvc;

namespace DataBase.Handler
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxHandler : ActionMethodSelectorAttribute
    {
       public bool ignore{get;set;}
       public AjaxHandler(bool ignore=false)
       {
           this.ignore = ignore;
       }

       public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
       {
           if (this.ignore == true)
           {
               return true;
           }
           return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
       }
    }
}
