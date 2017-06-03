namespace Blog.Models.ViewModels.Paging
{
    using Posts;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PaginationPostsByCategoryIdViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<PostsViewModel> Posts { get; set; }

        [Display(Name = "Categories")]
        public int? CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}