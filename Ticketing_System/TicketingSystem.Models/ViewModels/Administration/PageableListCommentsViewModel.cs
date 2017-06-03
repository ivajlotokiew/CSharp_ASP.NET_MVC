namespace TicketingSystem.Models.ViewModels.Administration
{
    using System.Collections.Generic;

    public class PageableListCommentsViewModel<T> where T : class
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<T> Comments { get; set; }
    }
}