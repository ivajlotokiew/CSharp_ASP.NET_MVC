namespace Blog.Models.ViewModels.Posts
{
    using Comment;
    using EntityModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PostsViewModel
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(1500, MinimumLength = 30)]
        public string Content { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        [Display(Name = "Viewed: ")]
        public int Views { get; set; }

        [Display(Name = "Posted on: ")]
        public DateTime PostedOn { get; set; }

        [Display(Name = "Updated on: ")]
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Author: ")]
        public virtual string AuthorName { get; set; }

        public int? ImageId { get; set; }
    }
}