using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeInEvent.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {


        public string UserFirstName { get; set; }


        public string UserLastName { get; set; }
        [ForeignKey("User_City")]
        public int UserCity { get; set; }
        public int userIsBlocked { get; set; }

        // 1 - M User Book Ticket
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Event> PublishEvent { get; set; }
        // 1 - M User Have Category
       /// [Required]
        public virtual UserCategory UserCategory { get; set; }
        //Object From M - M Class 
        [InverseProperty("Users")]
        public virtual City User_City { get; set; }
        public virtual UserSubscribeEvent userSubscribEvent { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketType> TicketTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<UserCategory> UsersCategories { get; set; }
        public virtual DbSet<UserSubscribeEvent> UserSubscribeEvents { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}