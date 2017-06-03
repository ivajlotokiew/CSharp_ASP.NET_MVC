namespace Blog.Models.ViewModels.Area
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    public class AddPostViewModel
    {
        public string AuthorId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(1500, MinimumLength = 30)]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }
    }
}