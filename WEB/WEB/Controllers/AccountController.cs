using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using BLL.Services;
using System.Diagnostics;
using Model.Models.AuthorizationViewModel;
using System.Data.SqlClient;

namespace WEB.Controllers
{
    public class AccountController : Controller
    {
        private AuthorizationService _authorizationService = new AuthorizationService();
        // GET: Account
        public ActionResult Register()
        {
            //await _authorizationService.SendVerificationMessage("1");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            IdentityResult result = await _authorizationService.CreateUser(model);
            if (!result.Succeeded)
            {
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Referer = HttpContext.Request.UrlReferrer;
            return View(new LoginViewModel()
            {
                ReferrerfUrl = HttpContext.Request.UrlReferrer
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authorizationService.Login(model);

            switch (result)
            {
                case SignInStatus.Success:
                    return Redirect(model.ReferrerfUrl.ToString());
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode");
                case SignInStatus.Failure:
                default:
                    {
                        ModelState.AddModelError("", "Invalid login or password");
                        return View(model);
                    }

            }           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _authorizationService.SignOut();
            return RedirectToAction("Index", "Base");
        }
        public async Task<ActionResult> SendCode(bool rememberMe)
        {

            var userId = await _authorizationService.CreateUser(new RegisterViewModel());
            return View();
        }

    }
}