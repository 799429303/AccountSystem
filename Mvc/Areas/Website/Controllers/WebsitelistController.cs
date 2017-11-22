using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using DataBase.DataHelper;
using Newtonsoft.Json;
using DataBase.Handler;
namespace Mvc.Areas.Website.Controllers
{
    public class WebsitelistController : Controller
    {
        //
        // GET: /Website/Websitelist/
        [LoginHandler]
        public ActionResult Index()
        {

            return View();
        }
        [AjaxHandler]
        public string GetWebSiteList()
        {
            string userid = ((AS_user)Session["loginuserkey"]).userid;
            List<AS_website> lstResult = new List<AS_website>();
            IRepositoryBase<AS_website> service = new RepositoryBase<AS_website>();
            lstResult = service.FindEntities(U => U.userid == userid);
            //var data = new
            //{
            //    rows = JsonConvert.DeserializeObject(lstResult.ListToJason<AS_website>()),
            //    total = "",
            //    page = "",
            //    records = ""
            //};
            return lstResult.ListToJason<AS_website>();
        }
    }
}
