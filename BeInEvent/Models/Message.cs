using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeInEvent.Models
{
    public class Message
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int index { get; set; }
        [RegularExpression(@"^\w+@[a-zA-Z]+?\.[a-zA-Z]{2,3}$")]
        public string Email { get; set; }
        [StringLength(200,MinimumLength = 50 , ErrorMessage ="Minmum Text Length = 50")]
        public string ReportMessage { get; set; }

    }
}