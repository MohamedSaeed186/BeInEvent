using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeInEvent.Models
{
    public class UserSubscribeEvent
    {
        [Key]
        [ForeignKey("SubscriperUser")]
        [Column(Order = 1)]
        public string UserID { get; set; }

        [Key]
        [ForeignKey("SubscribeEvent")]
        [Column(Order = 2)]
        public int EventID { get; set; }

        public string FeedBack { get; set; }

        // Supscrip M - M Event
        [InverseProperty("userEventSubscribEvent")]
        public virtual ICollection<Event> SubscribeEvent { get; set; }

        // Supscrip M - M User
        [InverseProperty("userSubscribEvent")]
        public virtual ICollection<ApplicationUser> SubscriperUser { get; set; }

    }
}