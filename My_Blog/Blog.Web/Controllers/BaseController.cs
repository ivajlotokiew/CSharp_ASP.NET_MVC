namespace Blog.Web.Controllers
{
    using Models.ViewModels.Posts;
    using Services;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        private BaseService service = new BaseService();

        public ActionResult MostViewedPosts()
        {
            IEnumerable<PostsSortedByViewModel> vmPosts =
                this.service.GetFirstFiveMostViewdPosts();

            return this.PartialView("_PostsSortedByViewsPartial", vmPosts);
        }

        public ActionResult MostCommentedPosts()
        {
            IEnumerable<PostsSortedByViewModel> vmPosts =
                this.service.GetFirstFiveMostCommentedPosts();

            return this.PartialView("_PostsSortedByCommentsPartial", vmPosts);
        }
    }
}