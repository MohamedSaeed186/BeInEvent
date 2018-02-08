using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeInEvent.Models
{
    public class City
    {

        public int ID { get; set; }
        public string CityName { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

    }
}