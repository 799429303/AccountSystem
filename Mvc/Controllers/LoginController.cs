using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using DataBase.DataHelper;
using Mvc.Models;
using DataBase.WebHelper;
namespace Mvc.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {

            return View();
        }
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

    }
}
