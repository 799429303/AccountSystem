using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using DataBase.DataHelper;
using DataBase.Handler;

namespace Mvc.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        public ActionResult Index()
        {
            return View();
        }
        [AjaxHandler]
        public ActionResult Register()
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

    }
}
