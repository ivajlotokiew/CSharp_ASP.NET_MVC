namespace TicketingSystem.Models.ViewModels.Home
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string AuthorUserName { get; set; }

        public int NumberOfComments { get; set; }
    }
}