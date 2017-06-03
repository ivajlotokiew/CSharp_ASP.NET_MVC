namespace TicketingSystem.Models.ViewModels.Tickets
{
    public class ListTicketViewModel
    {
        public string Title { get; set; }

        public string Category { get; set; }

        public string AuthorName { get; set; }

        public PriorityType Priority { get; set; }
    }
}
