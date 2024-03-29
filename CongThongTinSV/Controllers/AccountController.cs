﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using CongThongTinSV.Filters;
using CongThongTinSV.Models;
using CongThongTinSV.App_Lib;

namespace CongThongTinSV.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Entities db = new Entities();
            ViewBag.Service = new SelectList(db.MOD_DichVu, "Ten_rut_gon", "Ten_dv");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl, string Service)
        {
            //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            //{
            //    return RedirectToLocal(returnUrl);
            //}
            string portalUserGroup = "0";
            string portalUserID = "0";
            bool portalOK = false;
            string portalFullname = "";

            if (ModelState.IsValid)
            {
                Entities db = new Entities();
                ViewBag.Service = new SelectList(db.MOD_DichVu, "Ten_rut_gon", "Ten_dv");

                if (true)
                {
                    var q = from sv in db.STU_HoSoSinhVien
                            join ds in db.STU_DanhSach on sv.ID_sv equals ds.ID_sv
                            where sv.Ma_sv == model.UserName
                            select new
                            {
                                ID = ds.ID_sv,
                                Ma_sv = sv.Ma_sv,
                                Mat_khau = ds.Mat_khau,
                                Ho_ten = sv.Ho_ten
                            };

                    if (q.Count() > 0)
                    {
                        var hssv = q.First();

                        if (hssv.Mat_khau == model.Password)
                        {
                            portalOK = true;
                            portalFullname = hssv.Ho_ten;
                            portalUserGroup = "3";
                            portalUserID = hssv.ID.ToString();
                        }
                    }

                    if (!portalOK)
                    {
                        var q1 = from gv in db.PLAN_GiaoVien
                                 where gv.Ma_cb == model.UserName
                                 select gv;

                        if (q1.Count() > 0)
                        {
                            var gv = q1.First();
                            string matkhau = ((DateTime)gv.Ngay_sinh).ToString("ddMMyyyy");

                            var giaovien = (from gv1 in q1
                                            join gv2 in db.POR_GiaoVien
                                            on gv1.ID_cb equals gv2.ID_cb
                                            select new
                                            {
                                                ID_cb = gv2.ID_cb,
                                                Mat_khau = gv2.Mat_khau,
                                                Ngay_sinh = gv1.Ngay_sinh
                                            }).ToList();

                            if (giaovien.Count() == 0)
                            {
                                if (model.Password == matkhau)
                                {
                                    db.POR_GiaoVien.Add(new POR_GiaoVien()
                                    {
                                        ID_cb = gv.ID_cb,
                                        Mat_khau = matkhau
                                    });

                                    db.SaveChanges();
                                    portalOK = true;
                                    portalFullname = gv.Ho_ten;
                                    portalUserGroup = "2";
                                    portalUserID = gv.ID_cb.ToString();
                                }
                            }
                            else
                            {
                                if (giaovien.First().Mat_khau == model.Password)
                                {
                                    portalOK = true;
                                    portalFullname = gv.Ho_ten;
                                    portalUserGroup = "2";
                                    portalUserID = gv.ID_cb.ToString();
                                }
                            }
                        }
                    }
                }

                string token = MoodleLib.GetToken(model.UserName, model.Password, Service);

                if (token == "exception" && !portalOK)
                {
                }
                else
                {
                    // save authentication
                    MoodleEntities mdb = new MoodleEntities();
                    string cookieString;
                    HttpCookie cookie;
                    string[] userData = new string[7];
                    // save portal user group name
                    userData[0] = portalUserGroup;
                    // save portal user id
                    userData[1] = portalUserID;
                    // save portal user fullname
                    userData[2] = portalFullname;
                    // save moodle user token
                    userData[3] = token;
                    // save moodle user group name
                    userData[4] = Service;

                    //try
                    //{
                        var moodleUser = mdb.fit_user.Single(t => t.username == model.UserName);
                        // save moodle user id
                        userData[5] = moodleUser.id.ToString();
                        // save moodle user fullname
                        userData[6] = moodleUser.lastname + " " + moodleUser.firstname;
                    //}
                    //catch(Exception ex)
                    //{
                    //    string mess = ex.ToString();
                    //    userData[5] = "0";
                    //    userData[6] = "";
                    //}

                    // create a Forms Auth ticket with the username and the user data. 
                    FormsAuthenticationTicket formsTicket = new FormsAuthenticationTicket(
                        1,
                        model.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(60),
                        model.RememberMe,
                        string.Join("|", userData)
                        );
                    // encrypt the ticket
                    cookieString = FormsAuthentication.Encrypt(formsTicket);
                    cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieString);
                    if (model.RememberMe)
                        cookie.Expires = formsTicket.Expiration;
                    cookie.Path = FormsAuthentication.FormsCookiePath;
                    Response.Cookies.Add(cookie);
                    return RedirectToLocal(returnUrl);
                }
            }
            // If we got this far, something failed, redisplay form
            //ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ.");
            ModelState.AddModelError("", "Thông tin đăng nhập không hợp lệ.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //WebSecurity.Logout();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            var userData = GlobalLib.GetCurrentUserData();
            ViewBag.HasLocalPassword = userData!=null;//OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            /*bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        Entities db = new Entities();
                        
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);

                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }*/

            Entities db = new Entities();
            var userData = GlobalLib.GetCurrentUserData();
            if (userData !=null)
            {
                if (ModelState.IsValid)
                {
                    //var sv = db.STU_HoSoSinhVien.First(t => t.Ma_sv == userData.);
                    var ds = db.STU_DanhSach.First(t => t.ID_sv == userData.PortalUserID);
                    if (ds.Mat_khau == model.OldPassword)
                    {
                        ds.Mat_khau = model.NewPassword;
                        db.SaveChanges();
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {

            }
            

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
