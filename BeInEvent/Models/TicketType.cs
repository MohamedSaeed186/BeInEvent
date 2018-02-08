using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeInEvent.Models
{
    public class TicketType
    {

        public int TicketTypeId { get; set; }

        [Required]
        public string TypeOfTicket { get; set; }

        public double? TicketPrice { get; set; }

        public int? NOTicketType { get; set; }

        // 1 - M Ticket Has TicketType
        public virtual ICollection<Ticket> Ticket_TicketType { get; set; }


    }
}