namespace TicketingSystem.Models.ViewModels.Tickets
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    public class AddTicketViewModel
    {
        public PriorityType Priority { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }
    }
}