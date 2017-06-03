using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Models.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
