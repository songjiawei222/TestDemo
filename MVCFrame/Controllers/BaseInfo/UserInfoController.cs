using MVC.DAL;
using MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFrame.Controllers.BaseInfo
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/

        public ActionResult Index()
        {

            UserInfoDAL userInfoDAL = new UserInfoDAL();

            List<UserInfo> list = userInfoDAL.GetAll();
            return View(list);
        }

    }
}
