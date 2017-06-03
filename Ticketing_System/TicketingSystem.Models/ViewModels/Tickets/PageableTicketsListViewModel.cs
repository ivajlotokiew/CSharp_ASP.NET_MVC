namespace TicketingSystem.Models.ViewModels.Tickets
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PageableListTicketViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<ListTicketViewModel> Tickets { get; set; }

        [Display(Name ="Search by category")]
        public int? CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}