using BeInEvent.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeInEvent.Controllers
{
    
    public class EventsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Events
        public ActionResult events()
        {   
            List<Event> events = db.Events.ToList();

            return View(events);
        }

        public ActionResult subscribe(int id)
        {
            string userid = User.Identity.GetUserId();
            db.UserSubscribeEvents.Add(new UserSubscribeEvent() { UserID = User.Identity.GetUserId(), EventID = id });
            
            db.SaveChanges();
            ViewBag.sub = 1;
            Event eventdets = db.Events.Find(id);
            return View("eachevent",eventdets);
        }
        public ActionResult unsubscribe(int id)
        {
            Event eventdets = db.Events.Find(id);
            string userid = User.Identity.GetUserId();
            UserSubscribeEvent user = db.UserSubscribeEvents.Where(n => n.UserID == userid && n.EventID == id).First();

            db.UserSubscribeEvents.Remove(user);
            db.SaveChanges();
            ViewBag.sub = 0;
            return View("eachevent",eventdets);
        }
        public ActionResult eachevent(int id)
        {
            Event eventdets = db.Events.Find(id);
            List<TicketType> tickettypes = db.Events.Single(v => v.EventID == id).TicketType.ToList();
            Session["tickettypes"] = tickettypes;
            Session["eventid"] = id;

            string userid = User.Identity.GetUserId();
            UserSubscribeEvent user = db.UserSubscribeEvents.Where(n => n.UserID == userid && n.EventID == id).FirstOrDefault();

            if (user == null)
            {
                ViewBag.sub = 0;
            }
            else
            { ViewBag.sub = 1; }
            
            return View(eventdets);
        }

        [HttpGet]
        public ActionResult buyticket()
        {
            List<TicketType> tickettypes = (List<TicketType>)Session["tickettypes"];
            var id = (int)Session["eventid"];
            ViewBag.tickettypes = tickettypes;
            ViewBag.id = id;
            return PartialView();
        }

        [HttpPost]
        public ActionResult buyticket(string noofticket,string ttype)
        {
            List<TicketType> tickettypes = (List<TicketType>)Session["tickettypes"];
           

            var id = (int)Session["eventid"];
            var UserId = User.Identity.GetUserId();
            int x = 0;
            Event eventdets = db.Events.Find(id);
            ViewBag.id = id;
            ViewBag.tickettypes = tickettypes;
            //my
            foreach (TicketType item in tickettypes)
            {
                int sum = 0,a;
                var alltype = db.Events.FirstOrDefault(n => n.EventID == id).TicketType.FirstOrDefault(n => n.TicketTypeId == item.TicketTypeId).NOTicketType;
                var allticket =  db.Tickets.Where(n => n.TicketTypeId == item.TicketTypeId).Select(y => y.NuOfUserTicket);
                foreach (var items in allticket)
                {
                    sum += items;
                }
                if(Request.Form[x].ToString()=="")
                {
                    a = 0;
                   
                }
                else
                {
                    a = int.Parse(Request.Form[x].ToString());

                    if (alltype - sum >= a)
                    {
                        db.Tickets.Add(new Ticket() { TicketTypeId = item.TicketTypeId, NuOfUserTicket = a, UserID = UserId, TicketID = item.TicketTypeId + item.TicketTypeId * 2, EventID = id });
                        db.SaveChanges();
                        ViewBag.result = "sucess booked";
                    }
                    else
                    {
                        ViewBag.result = "tickt of " + item.TypeOfTicket + " is not avaliablr for this amount ,available amount is " + (alltype - sum);
                        return View("eachevent", eventdets);

                    }

                }
                x++;
            }


           
            return View("eachevent",eventdets);
        }
        public ActionResult feedback(Event eve)
        {
            string userid = User.Identity.GetUserId();
            UserSubscribeEvent user = db.UserSubscribeEvents.Where(n => n.UserID == userid && n.EventID == eve.EventID).FirstOrDefault();
            user.FeedBack = eve.userEventSubscribEvent.FeedBack;
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
            Event e = db.Events.FirstOrDefault(n => n.EventID == eve.EventID);

           
            return View("eachevent",e);
        }

        public ActionResult getFeedbacks(int id)
        {
            ViewBag.counter = 0;
            IEnumerable<UserSubscribeEvent> all = db.UserSubscribeEvents.Where(n => n.EventID == id).ToList();
            List<ApplicationUser> UserNames = new List<ApplicationUser>();
            foreach (var item in all)
            {
                if(item!=null && item.FeedBack!=null)
                {
                    ViewBag.counter += 1;
                       ApplicationUser user = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("1")).Where(n => n.Id==item.UserID).FirstOrDefault();
                       UserNames.Add(user);
                }
            }

            ViewBag.allusers = UserNames;
            ViewBag.Usersubscribe = all;
            return PartialView();
        }
    }
    
}