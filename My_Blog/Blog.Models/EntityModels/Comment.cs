namespace Blog.Models.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1500, MinimumLength = 3)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int? GuestAuthorId { get; set; }

        public virtual Guest GuestAuthor { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}