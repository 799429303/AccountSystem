using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using DataBase.DataHelper;
using Mvc.Areas.Admin.Models;
using DataBase.Handler;
namespace Mvc.Areas.Admin.Controllers
{
    public class WebController : Controller
    {
        //
        // GET: /Admin/AddUserWeb/
        [LoginHandler]
        public ActionResult Index()
        {
            return View();
        }
        [AjaxHandler]
        public JsonResult AddUserWebSite()
        {
            AS_user_website userWeb = new AS_user_website();
            userWeb.username = Request["username"];
            userWeb.website = Request["website"];
            userWeb.password = Request["password"];
            userWeb.userid = ((AS_user)Session["loginuserkey"]).userid;
            userWeb.uuid = Guid.NewGuid().ToString().ToUpper();
            IRepositoryBase<AS_user_website> service = new RepositoryBase<AS_user_website>();
            AS_user_website objtemp= service.FindEntities(p => p.userid == userWeb.userid&&p.website==userWeb.website).FirstOrDefault<AS_user_website>();
            if (objtemp != null)
            {
                return new JsonResult { Data = "FAIL",JsonRequestBehavior=JsonRequestBehavior.AllowGet };
            }
            else
            {
                service.BeginTrans();
                service.AddEntity(userWeb);
                service.Commit();
                return new JsonResult { Data = "SUCCESS", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            
        }
        [HttpPost]
        public string GetWebSite()
        {
            string userid=((AS_user)Session["loginuserkey"]).userid;
            IRepositoryBase<AS_website> service = new RepositoryBase<AS_website>();
            return service.FindEntities(U => U.userid == userid).ListToJason<AS_website>();
        }

    }
}
