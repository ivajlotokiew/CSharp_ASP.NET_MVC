namespace Blog.Models.ViewModels.Paging
{
    using Posts;
    using System.Collections.Generic;

    public class PaginationPostsByTagIdViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<PostsViewModel> Posts { get; set; }

        public int? TagId { get; set; }
    }
}