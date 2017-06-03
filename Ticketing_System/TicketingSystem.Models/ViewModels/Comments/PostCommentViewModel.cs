namespace TicketingSystem.Models.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class PostCommentViewModel
    {
        public PostCommentViewModel()
        {

        }

        public PostCommentViewModel(int ticketId)
        {
            this.TicketId = ticketId;
        }

        public int TicketId { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        [UIHint("MultiLineText")]
        public string Content { get; set; }
    }
}