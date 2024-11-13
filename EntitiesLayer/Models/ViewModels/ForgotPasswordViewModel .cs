using System.ComponentModel.DataAnnotations;

namespace NCRSPOTLIGHT.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


    }
}
