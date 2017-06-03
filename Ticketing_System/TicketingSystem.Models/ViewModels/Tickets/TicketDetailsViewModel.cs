namespace TicketingSystem.Models.ViewModels.Tickets
{
    using System.Collections.Generic;
    using Comments;

    public class TicketDetailsViewModel
    {
        public int Id { get; set; }

        public PriorityType Priority { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public IList<CommentsViewModel> Comments { get; set; }

        public int? ImageId { get; set; }
    }
}