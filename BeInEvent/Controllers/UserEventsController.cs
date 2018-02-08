using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeInEvent.Models;
using Microsoft.AspNet.Identity;

namespace BeInEvent.Controllers
{
    public class UserEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        List<Event> s = new List<Event>();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getUserPublishedEvents()
        {
            string id = User.Identity.GetUserId();
            List<Models.Event> PublishedEven = db.Events.Where(n => n.PublisherID == id && n.EventCanBePublished == 1).ToList();//.Select(s => new { s.EventName, s.EventID,s.Description}).ToList();
            List<BeInEvent.Models.Event> PublishedEvents = new List<Models.Event>();
            foreach(var item in PublishedEven)
            {
                if (item != null)
                { 
                if(item.EventCanBePublished == 1)
                {
                    PublishedEvents.Add(item);
                }
                }
            }
           
            return View(PublishedEvents);
        }

        public ActionResult getUserSubscribedEvents( )
        {
            string id = User.Identity.GetUserId();

            List<Models.Event> s = new List<Models.Event>();
            List<UserSubscribeEvent> SubscribedEvents = db.UserSubscribeEvents.Where(e => e.UserID == id).ToList();
            foreach (var item in SubscribedEvents)
            {
                var any = db.Events.Where(v => v.EventID == item.EventID && v.PublisherUser.userIsBlocked == 0 && v.EventCanBePublished == 1).FirstOrDefault();
                if (any != null)
                {
                    s.Add(any);
                }
            }
            return View(s);
        }

        public ActionResult GetUserTickets()
        {
            string id = User.Identity.GetUserId();

            List<Ticket> OrderedTickets = db.Tickets.Where(t => t.UserID == id).ToList();
            return View(OrderedTickets);
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