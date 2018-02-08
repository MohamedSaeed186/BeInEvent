using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeInEvent.Models
{
    public class Category
    {
        
        public int CategoryID { set; get; }
        [Required]
        [StringLength(20,MinimumLength = 5)]
        public string CategoryName { set; get; }

        // 1 To M Event Has Category
        public virtual ICollection<Event> EventsCategory { set; get; }

        public virtual UserCategory User_Category { get; set; }


    }
}