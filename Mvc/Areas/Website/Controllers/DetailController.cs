using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using DataBase.DataHelper;

namespace Mvc.Areas.Website.Controllers
{
    public class DetailController : Controller
    {
        //
        // GET: /Website/Detail/

        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Request["UUID"]))
            {
                IRepositoryBase<AS_website> service = new RepositoryBase<AS_website>();
                AS_website Entity = service.FindEntity(U => U.uuid == Request["UUID"]);
                return View(Entity);
            }
            return View();
        }

        public ActionResult AddWebSite()
        {
            IRepositoryBase<AS_website> service = new RepositoryBase<AS_website>();
            AS_website Entity = new AS_website();
            Entity.userid = ((AS_user)Session["loginuserkey"]).userid;;
            Entity.uuid = Guid.NewGuid().ToString().ToUpper();
            Entity.website = Request["website"].ToString();
            service.BeginTrans();
            service.AddEntity(Entity);
            return Content(service.Commit().ToString());
        }

    }
}
