namespace Blog.Models.ViewModels.Posts
{
    using System.Collections.Generic;

    public class PostsByTagViewModel
    {
        public int Id { get; set; }

        public IEnumerable<PostsViewModel> Posts { get; set; }
    }
}