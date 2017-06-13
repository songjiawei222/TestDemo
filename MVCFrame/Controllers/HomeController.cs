using MVC.DAL;
using MVC.Model;
using MVCFrame.Models;
using MVCFrame.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCFrame.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
      
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetMeunInfoByParentId( int parentid)
        {
            MenuInfoDAL menuInfoDAL = new MenuInfoDAL();
            List<MenuInfo> list = menuInfoDAL.GetByParentId(parentid);
           
            return Json(list);
        }

        [HttpPost]
        public JsonResult EditPassWord(string password)
        {
            
            if (SessionUtil.Session.Contains("UserInfo"))
            {
              UserUtil userUtil = (UserUtil)SessionUtil.Session["UserInfo"];
              UserInfoDAL userInfoDAL = new UserInfoDAL();
              if (userInfoDAL.EditPassWord(userUtil.Id.ToString(), password))
              {
                  return Json(true);
              }
              else return Json(false);
            }
            else {
                RedirectToAction("Shared", "Error");
            }

            return null;
        
        }


        [HttpPost]
        public JsonResult GetMenuList(int parentid)
        {

            MenuInfoDAL menuInfoDAL = new MenuInfoDAL();
            List<MenuInfo> list = menuInfoDAL.GetByParentId(parentid);
            StringBuilder sb = new StringBuilder();
              foreach( MenuInfo mi  in list)
              {

                  sb.Append("<li class=\"active \" ><a href=\"#\">" + mi.Title + "</a></li> ");


                  List<MenuInfo> childlist = menuInfoDAL.GetByParentId(mi.Id);
                  if (childlist.Count > 0)
                  {
                     
                      foreach (MenuInfo child in childlist)
                      {
                          sb.Append("<li><a href=\"#\" onclick=addTab('" + child.Id + "','" + child.Url + "','" + child.Title + "') >" + child.Title + "</a></li>");
                      
                      }
                  }
              }
              return Json(sb.ToString());
             
        }
    }
}
