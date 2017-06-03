namespace TicketingSystem.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}