﻿namespace Blog.Models.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PostsByCategoryViewModel
    {
        [Display(Name = "Categories")]
        public int? CategoryId { get; set; }

        public IEnumerable<PostsViewModel> Posts { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}