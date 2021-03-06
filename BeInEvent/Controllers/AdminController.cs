﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeInEvent.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;
using System.Net;
using System.Data.Entity.Validation;

namespace BeInEvent.Controllers
{


    [Authorize(Roles = "Admin")]


    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext db = new ApplicationDbContext();

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //category update
        public ActionResult update()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult update(Category model)
        {
            if (ModelState.IsValid)
            {
                Category cat = db.Categories.Where(n => n.CategoryID == model.CategoryID).FirstOrDefault();
                if (cat != null)
                {
                    cat.CategoryName = model.CategoryName;
                    db.SaveChanges();
                }

            }
            return View();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<ApplicationUser> users = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("1")).ToList();
            SelectList alluser = new SelectList(users, "Id", "UserName");
            //List<ApplicationUser>users = db.Users.Where(n => n.userIsBlocked == 0).ToList();
            ViewBag.users = alluser;
            TempData.Keep();
            //ViewBag.users = db.Users.Where(n => n.userIsBlocked == 0).Select(n => new { n.UserName }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(ApplicationUser model)
        {
            ApplicationUser user = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("1") && x.Id == model.Id).FirstOrDefault();
            // ApplicationUser user= db.Users.FirstOrDefault(m => m.Id == model.Id);

            if (user != null)
            {
                UserManager.RemoveFromRole(model.Id, "User");
                UserManager.AddToRole(model.Id, "Admin");
                db.SaveChanges();
                TempData["result"] = "Member Is Added to Be Admin Now";
            }
            else
            {
                TempData["result"] = "Member Already Is  Admin";

            }
            List<ApplicationUser> users = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("1")).ToList();
            SelectList alluser = new SelectList(users, "Id", "UserName");
            //List<ApplicationUser>users = db.Users.Where(n => n.userIsBlocked == 0).ToList();
            ViewBag.users = alluser;
            return View();

        }
        [HttpGet]
        public ActionResult delete()
        {
            String ID = User.Identity.GetUserId();
            List<ApplicationUser> users = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("2") && x.Id != ID).ToList();
            SelectList alluser = new SelectList(users, "Id", "UserName");
            //List<ApplicationUser>users = db.Users.Where(n => n.userIsBlocked == 0).ToList();
            ViewBag.users = alluser;
            TempData.Keep();
            //ViewBag.users = db.Users.Where(n => n.userIsBlocked == 0).Select(n => new { n.UserName }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult delete(ApplicationUser model)
        {
            ApplicationUser user = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("2") && x.Id == model.Id).FirstOrDefault();
            // ApplicationUser user= db.Users.FirstOrDefault(m => m.Id == model.Id);

            if (user != null)
            {
                UserManager.RemoveFromRole(model.Id, "Admin");
                UserManager.AddToRole(model.Id, "User");
                db.SaveChanges();
                TempData["result"] = "Member Is Removed from Being Admin";
            }
            else
            {
                TempData["result"] = "Member Already Is Not Admin";

            }
            List<ApplicationUser> users = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("2")).ToList();
            SelectList alluser = new SelectList(users, "Id", "UserName");
            //List<ApplicationUser>users = db.Users.Where(n => n.userIsBlocked == 0).ToList();
            ViewBag.users = alluser;
            return View();

        }
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
        [HttpGet]
        public ActionResult Createcat()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Createcat(Category cat)
        {
            db.Categories.Add(cat);
            db.SaveChanges();
            return View("Index");
        }
        [HttpGet]
        public ActionResult Deletecat()
        {
            SelectList cat = new SelectList(db.Categories.ToList(), "CategoryID", "CategoryName");

            ViewBag.cat = cat;

            return View();
        }

        [HttpPost]
        public ActionResult Deletecat(Category cate)
        {
            //SelectList cat = new SelectList(db.Categories.ToList(), "CategoryID", "CategoryName");

            Category cat = db.Categories.FirstOrDefault(a => a.CategoryID == cate.CategoryID);
            ViewBag.cat = cat;
            db.Categories.Remove(cat);
            db.SaveChanges();
            return View("Index");
        }
        [HttpGet]
        public ActionResult Createcity()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Createcity(City newcity)
        {
            db.Cities.Add(newcity);
            db.SaveChanges();
            return View("Index");
        }


        [HttpGet]
        public ActionResult Deletecity()
        {
            SelectList usercities = new SelectList(db.Cities.ToList(), "ID", "CityName");

            ViewBag.cit = usercities;

            return View();
        }

        [HttpPost]
        public ActionResult Deletecity(City usercity)
        {

            City cit = db.Cities.FirstOrDefault(a => a.ID == usercity.ID);
            ViewBag.cit = cit;
            db.Cities.Remove(cit);
            db.SaveChanges();
            return View("Index");
        }

        [HttpGet]
        public ActionResult Message()
        {
            var messages = db.Messages.ToList();
            return View(messages);
        }
        [HttpGet]
        public ActionResult Replay(int id)
        {
            var i = db.Messages.FirstOrDefault(a => a.index == id);
            if (i != null)
            {

                return View(i);
            }
            else
            {

                return View();
            }
        }
        [HttpPost]
        public ActionResult Replay(Message message)
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("sararagab51194@gmail.com", "23242992324134"),

            };
            MailMessage m = new MailMessage("sararagab51194@gmail.com", message.Email)
            {
                Body = message.ReportMessage,
                Subject = "BeInEvent admin"
            };
            client.Send(m);

            return PartialView("Done");
        }

        public ActionResult BlockUsers()
        {
            List<ApplicationUser> users = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("1")).ToList();
            SelectList alluser = new SelectList(users, "Id", "UserName");
            //List<ApplicationUser>users = db.Users.Where(n => n.userIsBlocked == 0).ToList();
            ViewBag.users = alluser;
            TempData.Keep();
            //ViewBag.users = db.Users.Where(n => n.userIsBlocked == 0).Select(n => new { n.UserName }).ToList();
            return View();
        }


        public ActionResult Blocked(string id)
        {
            ApplicationUser user = db.Users.SingleOrDefault(e => e.Id == id);
            if (user.userIsBlocked == 0)
            {
                try
                {
                    user.userIsBlocked = 1;
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
                TempData["result"] = "User is Blocked";
            }
            else
            {
                TempData["result"] = "User is already Blocked";

            }
            return RedirectToAction("BlockUsers");

        }

        //public ActionResult UnblockUsers()
        //{
        //    //List<ApplicationUser>users = db.Users.Where(n => n.userIsBlocked == 1).ToList();
        //    List<ApplicationUser> users = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("1")).Where(n => n.userIsBlocked == 1).ToList();


        //    ViewBag.users = users;
        //    return View();
        //}
        [HttpPost]
        public ActionResult UnBlocked(string id)
        {
            ApplicationUser user = db.Users.SingleOrDefault(e => e.Id == id);
            if (user.userIsBlocked == 1)
            {
                try
                {
                    user.userIsBlocked = 0;
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
                TempData["result"] = "User is unBlocked";
            }
            else
            {
                TempData["result"] = "User is already unBlocked";

            }
            return RedirectToAction("BlockUsers");


        }
        public ActionResult PublishEvents()
        {
            List<Event> EventsTobBePublished = db.Events.Where(m => m.EventCanBePublished == 0 && m.PublisherUser.userIsBlocked == 0).ToList();

            return View(EventsTobBePublished);
        }

        public ActionResult Publish(int id)
        {

            db.Events.Single(e => e.EventID == id).EventCanBePublished = 1;
            //db.Events.Single(e => e.PublisherID == "111");
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

            //Models.Event e = db.Events.Where(m=>m.EventID==id).FirstOrDefault();
            //e.EventCanBePublished = 1;

            //if (e != null) {
            //db.Entry(e).State = EntityState.Modified;
            //db.SaveChanges();
            //              }
            return RedirectToAction("PublishEvents");
        }
    }
}