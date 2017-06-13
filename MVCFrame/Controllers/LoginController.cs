using MVC.DAL;
using MVC.Model;
using MVCFrame.Models;
using MVCFrame.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MVCFrame.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            if (Request.Cookies["MVCLoginInfo"] != null)
            {
                string cookieString = Request.Cookies["MVCLoginInfo"].Value;
                if (!string.IsNullOrEmpty(cookieString))
                {
                    LoginModel model = LitJson.JsonMapper.ToObject<LoginModel>(cookieString);
                    if (model != null && model.IsRememberMe)
                    {
                        return View(model);
                    }
                }
            
            }
         
            return View();
        }


        /// <summary>
        /// 登录验证
        /// </summary>
        [HttpPost]

        public ActionResult Index(LoginModel model)
        {


            if (model.UserName == null || model.Password == null)
            {

                @ViewBag.Mes = "用户名或密码不能为空";
            }
            else
            {
                UserInfoDAL userInfoDAL = new UserInfoDAL();

                UserInfo userInfo = userInfoDAL.GetUserInfoByNameAndPwd(model.UserName.Trim(), model.Password.Trim());


                if (userInfo != null && userInfo.UserName != null)
                {
                    UserUtil userUtil = new UserUtil();
                    userUtil.Id = userInfo.Id;
                    userUtil.UserName = userInfo.UserName;
                    userUtil.UserPwd = userInfo.UserPwd;
                    userUtil.EmployeeName = userInfo.EmployeeName;
                    userUtil.RoleId = userInfo.RoleId;
                    userUtil.IsSys = userInfo.IsSys;
                    userUtil.IsRememberMe = model.IsRememberMe;
                    userUtil.Comment = userUtil.Comment;
                    SessionUtil.Session.Add("UserInfo", userUtil);

                    //创建两个Cookie对象
                    HttpCookie myCookie = new HttpCookie("MVCLoginInfo", LitJson.JsonMapper.ToJson(model));
                    myCookie.Expires.AddDays(10);
                    Response.Cookies.Add(myCookie);
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    @ViewBag.Mes = "用户名或密码不正确";
                }
            }
            return View(model);
        }



        [HttpPost]
        public JsonResult GetLoginInfo()
        {
            if (SessionUtil.Session.Contains("UserInfo"))
            {
                UserUtil userUtil = (UserUtil)SessionUtil.Session["UserInfo"];
                return Json(userUtil);
            }
            else {

                 RedirectToAction("Shared", "Error");
                 return null;
            }
        }


    }
}