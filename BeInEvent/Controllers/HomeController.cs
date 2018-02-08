using BeInEvent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeInEvent.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //User.Identity.GetUserId();
            List<Category> currentCategory = db.Categories.ToList();
            ViewBag.currentCategory = currentCategory.ToList();
            int yeardate = DateTime.Now.Year;
            List<BeInEvent.Models.Event> EventByYear = db.Events.Where(e => e.StartDate.Year == yeardate && e.EventCanBePublished == 1 && e.PublisherUser.userIsBlocked == 0).ToList();
            List<Models.Event> EventByMonth = new List<Models.Event>();
            foreach (var item in EventByYear)
            {
                if (item.StartDate.Month == DateTime.Now.Month)
                {
                    EventByMonth.Add(item);
                }
            }
            ViewBag.every = db.Events.Where(n=> n.EventCanBePublished == 1 && n.PublisherUser.userIsBlocked == 0).ToList();
            List<Models.Event> EventByDay = EventByMonth.OrderBy(e => e.StartDate.Day).ToList();

            //int date = DateTime.Today.Month;
            //List<Event> Events = db.Events.Where(e => e.StartDate.Month == date).Select(e1=>e1.).ToList();
            return View(EventByDay);
        }
        public ActionResult Details()
        {
            return RedirectToAction("Detail", "Events");
        }

        [HttpPost]
        public ActionResult Submit(string email, string message)
        {
            db.Messages.Add(new Message()
            {
                index = 3,
                Email = email,
                ReportMessage = message
            });
            //db.Messages.Add(message).ReportMessage.ToString();
            //db.Messages.Add(message).Email.ToString();
            try
            {
                db.SaveChanges();

            }
            catch (Exception e)
            {
                Response.Write(e);
            }
            return View();
        }

        public ActionResult FilterByCategory(int id)
        {
            List<Models.Event> currentEvent = db.Events.Where(n=>n.CategoryID==id&& n.EventCanBePublished==1&&n.PublisherUser.userIsBlocked==0).ToList();
            ViewBag.every = currentEvent.ToList();

            Category c = db.Categories.ToList().FirstOrDefault(c1 => c1.CategoryID == id);
            return PartialView(c);
        }

        public ActionResult Buy()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}