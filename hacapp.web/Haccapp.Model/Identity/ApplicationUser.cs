using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Haccapp.Model.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Email address")]
        //[Required(ErrorMessage = "The email address is required. This is where we will send you your notifications.")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string PreferredEmailAddress { get; set; }
    }
}