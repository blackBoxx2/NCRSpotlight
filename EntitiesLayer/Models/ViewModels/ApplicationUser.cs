using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesLayer.Models.ViewModels
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string?LastName { get; set; }
        [NotMapped]
        public string? RoleId { get; set; }
        [NotMapped]
        public string? Role { get; set; }
        [NotMapped]
        public string? UseClaim { get; set; }

        public DateTime? LastViewedNCRS { get; set; } = DateTime.MinValue;
    }
}
