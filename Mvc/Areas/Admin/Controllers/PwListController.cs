using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using DataBase.DataHelper;
using DataBase.Handler;
using Newtonsoft.Json;

namespace Mvc.Areas.Admin.Controllers
{
    public class PwListController : Controller
    {
        //
        // GET: /Admin/PwList/
        [LoginHandler]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string GetList()
        {
            string userid =((AS_user) Session["loginuserkey"]).userid;
            List<AS_user_website> ListResult = new List<AS_user_website>();
            IRepositoryBase<AS_user_website> objMng = new RepositoryBase<AS_user_website>();
            if (userid == "admin")
                ListResult = objMng.FindEntities(U => 1 == 1);
            else
                ListResult = objMng.FindEntities(U => U.userid == userid);
            return JsonConvert.SerializeObject(ListResult);
        }
    }
}
