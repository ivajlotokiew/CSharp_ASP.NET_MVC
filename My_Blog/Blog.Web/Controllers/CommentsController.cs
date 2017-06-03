namespace Blog.Web.Controllers
{
    using Models.ViewModels.Comment;
    using Services;
    using System.Web.Mvc;

    public class CommentsController : BaseController
    {
        private CommentsService service;

        public CommentsController()
        {
            this.service = new CommentsService();
        }

        [HttpGet]
        public ActionResult Add(int id)
        {
            var vmComment = this.service.AddPostIdToCommentVm(id);

            return this.View(vmComment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CommentViewModel comment)
        {
            if (comment != null && this.ModelState.IsValid)
            {
                this.service.AddCommentInDb(comment);
                return this.RedirectToAction("View", "Posts", new { id=comment.PostId});
            }

            return this.View();
        }
    }
}