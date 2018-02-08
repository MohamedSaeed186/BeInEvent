using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeInEvent.Models
{
    public class Ticket
    {
        [Column(Order = 1)]
        [Key]
        public int TicketID { get; set; }
        [Column(Order = 2)]

        [Key]
        [ForeignKey("Users")]
        public string UserID { get; set; }


        public int NuOfUserTicket { get; set; }

        // 1 - M User Book Ticket
        public virtual ApplicationUser Users { get; set; }
        public int TicketTypeId { get; set; }

        [ForeignKey("Event_Ticket")]
        public int EventID { get; set; }

        [InverseProperty("Ticket_TicketType")]
        public virtual TicketType TicketType { get; set; }

        [InverseProperty("Ticket")]
        public virtual Event Event_Ticket { get; set; }

    }
}