namespace Blog.Models.ViewModels.Comment
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CommentViewModel
    {
        public int PostId { get; set; }

        public int? ComentsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 3)]
        public string AuthorName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "GuestName")]
        [StringLength(50, MinimumLength = 3)]
        public string GuestName { get; set; }

        [EmailAddress]
        public string GuestEmail { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [StringLength(1500, MinimumLength = 3)]
        public string Content { get; set; }
    }
}