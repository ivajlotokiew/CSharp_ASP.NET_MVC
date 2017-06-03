namespace TicketingSystem.Web.Controllers
{
    using AutoMapper;
    using Models.ViewModels.Comments;
    using Services;
    using System.Web;
    using System.Web.Mvc;

    public class CommentsController : Controller
    {
        private CommentsService service;

        public CommentsController()
        {
            this.service = new CommentsService();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(PostCommentViewModel comment)
        {
            if (comment != null && this.ModelState.IsValid)
            {
                var dbComment = this.service.GetDbComment(comment);
                dbComment.Author = this.service.GetAuthorComment(dbComment);
                var viewModel = Mapper.Map<CommentsViewModel>(dbComment);

                return PartialView("_CommentPartial", viewModel);
            }

            throw new HttpException(400, "Invalid comment");
        }
    }
}