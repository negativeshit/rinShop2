using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV19T1021242.BusinessLayers;
using SV19T1021242.DomainModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SV19T1021242.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: /<controller>/
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username = "", string password = "")
        {
            ViewBag.UserName = username;
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("Error", "Vui lòng nhập tên và mật khẩu!");
                return View();
            }
            var userAccount = UserAccountService.Authorize(username, password);
            if(userAccount == null)
            {
                ModelState.AddModelError("Error", "Đăng nhập thất bại!");

                return View();
            }


            ///Đăng nhập thành công
            var userData = new WebUserData()
            {
                UserId = userAccount.UserID,
                UserName = userAccount.UserName,
                DisplayName = userAccount.FullName,
                Email = userAccount.Email,
                Photo = userAccount.Photo,
                ClientIP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                SessionId = HttpContext.Session.Id,
                AdditionalData = "",
                Roles = userAccount.RoleNames.Split(',').ToList()

            };
            //Thiết lập phiên đăng nhập
            await HttpContext.SignInAsync(userData.CreatePrincipal());
  
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {

            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult ChangePassword(string username, string newpassword, string oldpassword)
        {

            var userAccount = UserAccountService.ChangePassword(username, oldpassword, newpassword);
            return View("Save",userAccount);
        }
        [HttpPost]
        public IActionResult Save(bool userAccount)
        {
           
            if (userAccount == false)
            {
                ModelState.AddModelError("Error", "Vui lòng nhập email và mật khẩu lại!");
            }
                return Json(userAccount);
           
        }
    }
}

