using BeInEvent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeInEvent.Controllers
{
    public class CalenderController : Controller
    {
        // GET: Calender
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            ApplicationDbContext dc = new ApplicationDbContext();
            
                var events = dc.Events.Where(n=>n.EventCanBePublished == 1&&n.PublisherUser.userIsBlocked==0).Select(e=>new {e.EventID, e.EventName ,e.Description,e.StartDate,e.EndDate}).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            
        }
    }
}