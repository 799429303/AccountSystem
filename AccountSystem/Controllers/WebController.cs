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

        public ActionResult Web()
        {
            AS_website res = new AS_website();
            string uuid = Request.QueryString["uuid"];
            if (!string.IsNullOrEmpty(uuid))
            {
                IRepositoryBase<AS_website> service = new RepositoryBase<AS_website>();
                res = service.FindEntity(uuid);
            }
            return View(res);
        }


        [HttpPost]
        public string GetWebSite()
        {
            string userid = ((AS_user)Session["loginuserkey"]).userid;
            IRepositoryBase<AS_website> service = new RepositoryBase<AS_website>();
            return service.FindEntities(U => U.userid == userid).ListToJason<AS_website>();
        }
        [AjaxHandler]
        public JsonResult DeleteWeb()
        {
            string uuid = Request.QueryString["uuid"].Trim();
            IRepositoryBase<AS_website> service = new RepositoryBase<AS_website>();
            if (service.DeleteEntity(uuid) > 0)
            {
                return Json(new { code = "success", msg = "删除成功！" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = "fail", msg = "删除失败！" }, JsonRequestBehavior.AllowGet);
            }
        }
        [AjaxHandler]
        public JsonResult Update(string uuid, string web)
        {
            IRepositoryBase<AS_website> service = new RepositoryBase<AS_website>();
            if (string.IsNullOrEmpty(uuid))
            {
                AS_website website = new AS_website();
                website.uuid = Guid.NewGuid().ToString().ToUpper();
                website.website = web;
                website.userid = ((AS_user)Session["loginuserkey"]).userid;
                var res = service.FindEntity(p => p.userid == website.userid && p.website==web);
                 if (res != null)
                 {
                     return Json(new { code = "exsit", msg = "已存在！" }, JsonRequestBehavior.AllowGet);
                 }
                 else
                 {
                     if (service.AddEntity(website) > 0)
                     {
                         return Json(new { code = "success", msg = "添加成功！" }, JsonRequestBehavior.AllowGet);
                     }
                     else
                     {
                         return Json(new { code = "fail", msg = "添加失败！" }, JsonRequestBehavior.AllowGet);
 
                     }
                 }               
            }
            else
            {
                AS_website website = service.FindEntity(uuid);
                website.website = web;
                if (service.ModifyEntity(website) > 0)
                {
                    return Json(new { code = "success", msg = "保存成功！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = "fail", msg = "保存失败！" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
