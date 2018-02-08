using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeInEvent.Models
{
    public class UserCategory
    {
        [Key]
        [ForeignKey("GetUserCategory")]
        [Column(Order = 1)]
        public string UserID { get; set; }

        [Key]
        [ForeignKey("Get_User_Category")]
        [Column(Order = 2)]
        public int CategoryID { get; set; }


        [InverseProperty("UserCategory")]
        public virtual ICollection<ApplicationUser> GetUserCategory { get; set; }


        [InverseProperty("User_Category")]
        public virtual ICollection<Category> Get_User_Category { get; set; }
    }
}