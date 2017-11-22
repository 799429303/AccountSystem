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
    public class AddUserWebController : Controller
    {
        //
        // GET: /Admin/AddUserWeb/
        [LoginHandler]
        public ActionResult Index()
        {
            return View();
        }
        [AjaxHandler]
        public int AddUserWebSite()
        {
            AS_user_website userWeb = new AS_user_website();
            userWeb.username = Request["username"];
            userWeb.website = Request["website"];
            userWeb.password = Request["password"];
            userWeb.userid = ((AS_user)Session["loginuserkey"]).userid;
            userWeb.uuid = Guid.NewGuid().ToString().ToUpper();
            IRepositoryBase<AS_user_website> service = new RepositoryBase<AS_user_website>();
            service.BeginTrans();
            service.AddEntity(userWeb);
            return service.Commit();
            
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
