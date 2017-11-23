using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class UserListController : Controller
    {
        //
        // GET: /UserList/

        public ActionResult Index()
        {
            AccountSysEntitity dbcontext = new AccountSysEntitity();
            var UserList =dbcontext.AS_user.Select(u => u);
            //var UserList = (from c in dbcontext.AS_user
            //                select c.id);
            return View(UserList);
        }

    }
}
