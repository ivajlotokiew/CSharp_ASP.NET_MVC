namespace Blog.Services
{
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Models.EntityModels;
    using Models.ViewModels.Comment;
    using System;
    using System.Web;

    public class CommentsService : BaseService
    {
        public void AddCommentInDb(CommentViewModel comment)
        {
            Comment dbComment = Mapper.Map<Comment>(comment);
            Post dbPost = this.Context.Posts.Find(comment.PostId);
            dbComment.CreatedOn = DateTime.Now;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                this.Context.Users.Find(userId).Comments.Add(dbComment);
            }
            else
            {
                var guest = this.Context.Guests.Add(new Guest
                {
                    Email = comment.GuestEmail,
                    Name = comment.GuestName,
                });

                guest.Comments.Add(dbComment);
            }

            dbPost.Comments.Add(dbComment);
            this.Context.SaveChanges();
        }

        public CommentViewModel AddPostIdToCommentVm(int id)
        {
            CommentViewModel vmPost = new CommentViewModel
            {
                PostId = id
            };

            return vmPost;
        }
    }
}