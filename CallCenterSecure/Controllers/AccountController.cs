using CallCenter.BusinessLogic;
using CallCenter.CustomAuthentication;
using CallCenter.DataAccess;
using CallCenter.Models;
using CallCenterSecure.BusinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CallCenter.Controllers
{

    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.UserName, EncryptionProvider.Encrypt(loginView.Password)))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.UserName, false);
                    if (user != null)
                    {
                        CustomRoleProvider customRoleProvider = new CustomRoleProvider(); 
                        CustomSerializeModel userModel = new Models.CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserRoleName = customRoleProvider.GetRoleNameById(user.RoleId),
                            UserRoleId = user.RoleId
                            //RoleName = user.Roles.Select(r => r.RoleName).ToArray()
                        };
                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, loginView.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );
                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index","Home");
                    }
                }
                ModelState.AddModelError("", "Something Wrong : Username or Password invalid");
            }
            return View(loginView);
        }
       
        [HttpGet]
        public ActionResult ActivationAccount(string id)
        {
            bool statusAccount = false;
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var userAccount = dbContext.Users.Where(u => u.ActivationCode.ToString().Equals(id)).FirstOrDefault();
                if (userAccount != null)
                {
                    userAccount.IsActive = true;
                    dbContext.SaveChanges();
                    statusAccount = true;
                }
                else
                {
                    ViewBag.Message = "Something Wrong !!";
                }
            }
            ViewBag.Status = statusAccount;
            return View();
        }
        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }
        [NonAction]
        public void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);
            var fromEmail = new MailAddress("mehdi.rami2012@gmail.com", "Activation Account - AKKA");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "******************";
            string subject = "Activation Account !";
            string body = "Please click on the following link in order to activate your account" +
                "<a href = \"\"> Activation Account! </ a > ";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
    }

}