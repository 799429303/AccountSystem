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
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult _Header() 
        {
            return PartialView();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckLogin(string username, string password)
        {
            RepositoryBase<AS_user> database = new RepositoryBase<AS_user>();
            AS_user LoginEntity = database.FindEntity(u => u.userid == username);
            Encrypt Md5 = new Encrypt(password);
            if (LoginEntity != null)
            {
                if (LoginEntity.password == Md5.EncryptResult)
                {
                    if (!string.IsNullOrEmpty(Request["check"]))
                    {
                        WebHelper.WriteCookie("loginuserkey", LoginEntity.uuid, 60);

                    }
                    Session["loginuserkey"] = LoginEntity;
                    return Content("登录成功！");
                }
                else
                {
                    return Content("密码错误！");
                }
            }
            else
            {
                return Content("账号不存在！");
            }
        }


        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            AccountSysEntitity dbcontext = new AccountSysEntitity();
            var UserList = dbcontext.AS_user.Select(u => u);
            return View(UserList);
        }

        /// <summary>
        /// 检查用户名是否已有
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string CheckUser()
        {
            string username = Request["username"];
            IRepositoryBase<AS_user> service = new RepositoryBase<AS_user>();
            AS_user objUser = service.FindEntity(U => U.userid == username);
            if (objUser != null)
                return "false";
            else
                return "true";
        }

        public ActionResult Register()
        {
            return View();
        }
        [AjaxHandler]
        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        
        public ActionResult RegisterUser()
        {
            Encrypt md5 = new Encrypt(Request["password"]);
            IRepositoryBase<AS_user> service = new RepositoryBase<AS_user>();
            AS_user objUser = new AS_user();
            objUser.uuid = Guid.NewGuid().ToString().ToUpper();
            objUser.userid = Request["username"];
            objUser.password = md5.EncryptResult;
            service.BeginTrans();
            service.AddEntity(objUser);
            service.Commit();
            return Content("SUCCESS");
        }


        public ActionResult UserPasswordList()
        {
           
            return View();
        }

        public JsonResult GetUserPasswordList()
        {
            IRepositoryBase<AS_user_website> service = new RepositoryBase<AS_user_website>();
            string userid = ((AS_user)Session["loginuserkey"]).userid;
            List<AS_user_website> res = new List<AS_user_website>();
            string web = Request.QueryString["web"].Trim();
            string username = Request.QueryString["username"].Trim();
            if (!string.IsNullOrEmpty(username))
            {
                 res = service.FindEntities(p => p.userid == userid&&p.username ==username);
            }
            else if(!string.IsNullOrEmpty(web))
            {
                 res = service.FindEntities(p => p.userid == userid && p.website == web);
            }
            else if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(web))
            {
                 res = service.FindEntities(p => p.userid == userid && p.website == web && p.username == username);
            }
            else 
            {
                 res = service.FindEntities(p => p.userid == userid);
            }
            
            return Json(new { total = res.Count,rows=res }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddUserPassWord(string Id)
        {
            ViewBag.Modal = null;
            if (!string.IsNullOrEmpty(Id))
            {
                IRepositoryBase<AS_user_website> service = new RepositoryBase<AS_user_website>();
                var res=  service.FindEntity((object)Id.Trim());
                if (res != null)
                {
                    ViewBag.Modal = res;
                }
            }
            return View();
        }

        [AjaxHandler]
        public JsonResult AddUserWebSite()
        {
            AS_user_website userWeb = new AS_user_website();
            userWeb.username = Request["username"].Trim();
            userWeb.website = Request["website"].Trim();
            userWeb.password = Request["password"].Trim();
            userWeb.userid = ((AS_user)Session["loginuserkey"]).userid;
            userWeb.uuid = Request["uuid"].Trim();
            if (string.IsNullOrEmpty(userWeb.uuid))
            {              
                userWeb.uuid = Guid.NewGuid().ToString().ToUpper();
                IRepositoryBase<AS_user_website> service = new RepositoryBase<AS_user_website>();
                AS_user_website objtemp = service.FindEntities(p => p.userid == userWeb.userid && p.website == userWeb.website).FirstOrDefault<AS_user_website>();
                if (objtemp != null)
                {
                    return new JsonResult { Data = "EXSIT", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    service.BeginTrans();
                    service.AddEntity(userWeb);
                    service.Commit();
                    return new JsonResult { Data = "SUCCESS", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            else
            {
              
                IRepositoryBase<AS_user_website> service = new RepositoryBase<AS_user_website>();
                service.BeginTrans();
                var res = service.ModifyEntity(userWeb);
                service.Commit();

                if (res > 0)
                {
                    return new JsonResult { Data = "SUCCESS", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return new JsonResult { Data = "FAIL", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
           

        }
         [AjaxHandler]
        public JsonResult Delele(string id)
        {
            try
            {
                IRepositoryBase<AS_user_website> service = new RepositoryBase<AS_user_website>();

                if (service.DeleteEntity(id) > 0)
                {
                    return Json(new { code = "success", msg = "删除成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = "success", msg = "删除失败" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = "success", msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
