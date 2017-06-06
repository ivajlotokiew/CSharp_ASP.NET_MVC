namespace Blog.Models.ViewModels.Paging
{
    using Interfaces;
    using Posts;

    public class PaginationPostsByCategoryIdViewModel<T> : IPageable<T> where T : PostsByCategoryViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public T Attributes { get; set; }

    }
}
