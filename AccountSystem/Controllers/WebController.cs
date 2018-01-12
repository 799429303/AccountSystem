using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using DataBase.DataHelper;
using DataBase.Handler;
using DataBase.WebHelper;

namespace Mvc.Controllers
{
    public class WebController : Controller
    {
        //
        // GET: /Web/

        public ActionResult WebList()
        {
            return View();
        }

        public JsonResult GetWebList()
        {
            string userid = ((AS_user)Session["loginuserkey"]).userid;
            IRepositoryBase<AS_website> service = new RepositoryBase<AS_website>();
            string web = Request.QueryString["web"].Trim();
            List<AS_website> res = new List<AS_website>();
            if (!string.IsNullOrEmpty(web))
            {
                res = service.FindEntities(p => p.userid == userid && p.website == web);
            }
            else
            {
                res = service.FindEntities(p => p.userid == userid);
            }
            return Json(new { total = res.Count, rows = res }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddUserWebSite()
        {
            return View();
        }


        [HttpPost]
        public string GetWebSite()
        {
            string userid = ((AS_user)Session["loginuserkey"]).userid;
            IRepositoryBase<AS_website> service = new RepositoryBase<AS_website>();
            return service.FindEntities(U => U.userid == userid).ListToJason<AS_website>();
        }
    }
}
