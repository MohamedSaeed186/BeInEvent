using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeInEvent.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required(ErrorMessage = "You Must Enter Name Event")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Please Number Of Characters At Most 20 And At Least 5")]
        public string EventName { get; set; }

        // [RegularExpression(@"^.*\(.jpeg|.JPEG|.gif|.GIF|.png|.PNG)$",ErrorMessage = "you must enter image .jpeg|.JPEG|.gif|.GIF|.png|.PNG")]

       // [Required(ErrorMessage = "You Must Enter Image Of Event")]
        //[RegularExpression(@"^[a-zA-Z0-9]*/.(gif|jpg|jpeg|png)$")]
        public string Image { get; set; }

        [Required(ErrorMessage = "You Must Enter Description")]
        [StringLength(150, MinimumLength = 50, ErrorMessage = "Number Of Characters 150 and at least 50 ")]
        public string Description { get; set; }
        [Range (minimum:10 ,maximum:1000000,ErrorMessage = "You Must Enter Number Of Ticket")]
        public int NumberOfTickets { get; set; }


        [Required(ErrorMessage = "Please Enter YYYY/MM/DD HH:MM:SS")]
        public DateTime StartDate { get; set; }


        [Required(ErrorMessage = "Please Enter YYYY/MM/DD HH:MM:SS")]
        public DateTime EndDate { get; set; }
        
        [Required(ErrorMessage = "You Must Choose City")]
        public int City { get; set; }
        
        [Required(ErrorMessage = "You Must Enter Address")]
        public string Address { get; set; }

        [ForeignKey("PublisherUser")]
        public string PublisherID { get; set; }


        [ForeignKey("newCategory")]
        public int CategoryID { get; set; }

        public int EventCanBePublished { get; set; }

        [InverseProperty("EventsCategory")]
        public virtual Category newCategory { get; set; }

        [InverseProperty("PublishEvent")]
        public virtual ApplicationUser PublisherUser { get; set; }


        // [InverseProperty("EventTicketType")]
        public virtual ICollection<TicketType> TicketType { get; set; }

        //Object From M - M Class
        public virtual UserSubscribeEvent userEventSubscribEvent { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }


    }
}