
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using BeInEvent.Models;
using System.Data.Entity.Validation;

namespace BeInEvent.Controllers
{
    public class UserController : Controller
    {

        ApplicationDbContext bd = new ApplicationDbContext();


        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {

            SelectList cat = new SelectList(bd.Categories.ToList(), "CategoryID", "CategoryName");
            SelectList city = new SelectList(bd.Cities.ToList(), "ID", "CityName");
            ViewBag.cat = cat;
            ViewBag.city = city;
            ViewBag.check = 0;
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Event even, HttpPostedFileBase myimg)
        {
            SelectList cat = new SelectList(bd.Categories.ToList(), "CategoryID", "CategoryName");
            SelectList city = new SelectList(bd.Cities.ToList(), "ID", "CityName");
            ViewBag.cat = cat;
            ViewBag.city = city;
            string id = User.Identity.GetUserId();
            //myimg.SaveAs(Server.MapPath("~/images/"+ myimg.FileName));

            if (ModelState.IsValid&&myimg!=null)
            {
                even.Image = even.EventID + myimg.FileName;
                even.EventCanBePublished = 0;
                myimg.SaveAs(Server.MapPath("~/images/" + even.Image));
                ViewBag.myphoto = even.Image;
                var user = bd.Users.First(n => n.Id == id);
                if (user != null&&user.userIsBlocked==0)
                {
                    user.PublishEvent.Add(even);
                   
                    try
                    {
                        bd.SaveChanges();
                        ViewBag.check = 1;

                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        

                    }
                }
                ViewBag.evid = even.EventID;

                ModelState.Clear();
                ViewBag.message = "success";

                
                TempData["evid"] = even.EventID;
                return RedirectToAction("TicketType");
            }
            else
            {
                ViewBag.message = "faild";
               

                
                return View(even);
            }
        }

        [HttpGet]
        public ActionResult TicketType()
        {
            TempData.Keep();
            var x = (int)TempData["evid"];
            int NoOfTickets = bd.Events.FirstOrDefault(n=>n.EventID== x).NumberOfTickets;
            Session["NoOfTickets"] = NoOfTickets;
            return View();
        }
        [HttpPost]
        public ActionResult TicketType(TicketType tikty)
        {
            var x = (int)TempData["evid"];

            TempData.Keep();
           
          //  int tickets = bd.Events.Where(n => n.EventID == x).Select(b => b.NumberOfTickets).FirstOrDefault();
            if (ModelState.IsValid )
            {
                int ticketsNumber= int.Parse(Session["NoOfTickets"].ToString());
                if (ticketsNumber >= tikty.NOTicketType)
                {
                   ticketsNumber -=int.Parse(tikty.NOTicketType.ToString());
                    Session["NoOfTickets"] = ticketsNumber;

                    bd.TicketTypes.Add(tikty);
                    bd.Events.First(n => n.EventID == x).TicketType.Add(tikty);
                    bd.SaveChanges();
                    ViewBag.error = 0;
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.error = 1;
                }
                return View();
            }
            else
            {
                return View(tikty);
            }

        }
        
    }
}