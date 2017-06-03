namespace Blog.Web.Controllers
{
    using Models.ViewModels.Posts;
    using Services;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;
    using System;
    using Models.ViewModels.Paging;
    using Models.EntityModels;
    using System.Web;

    public class PostsController : BaseController
    {
        private PostsService service;

        public PostsController()
        {
            this.service = new PostsService();
        }

        public ActionResult View(int id)
        {
            PostsViewModel vmPost = this.service.GetPostsById(id);

            return this.View(vmPost);
        }

        public ActionResult Image(int id)
        {
            Image image = this.service.GetImageById(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.FileExtension);
        }

        public ActionResult All(int? CategoryId, int id = 1)
        {
            IEnumerable<PostsViewModel> posts;
            const int ItemsPerPage = 5;
            if (CategoryId != null)
            {
                posts = this.service.GetPostsByCategoryId(CategoryId);
                if (posts.Count() / (ItemsPerPage * id) < 1)
                {
                    id = 1;
                }
            }
            else
            {
                posts = this.service.GetAllPosts();
            }

            var page = id;
            var allItemsCount = posts.Count();
            var totalPages = Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;
            var postsVm = posts.Skip(itemsToSkip).Take(ItemsPerPage);

            var viewModel = new PaginationPostsByCategoryIdViewModel
            {
                CurrentPage = page,
                TotalPages = (int)totalPages,
                Posts = postsVm,
                Categories = this.service.GetAllCategories(),
                CategoryId = CategoryId
            };

            return View(viewModel);
        }

        public ActionResult Search(string tagName)
        {
            PostsByTagViewModel vmModel = 
                this.service.GetTagInfoByTagName(tagName);

            return this.View(vmModel);
        }
    }
}