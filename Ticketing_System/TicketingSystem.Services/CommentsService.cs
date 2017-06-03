namespace TicketingSystem.Services
{
    using AutoMapper;
    using System.Linq;
    using System.Web;
    using Models.EntityModels;
    using Models.ViewModels.Comments;

    public class CommentsService : BaseService
    {
        public Comment GetDbComment(PostCommentViewModel comment)
        {
            Comment dbComment = Mapper.Map<Comment>(comment);

            var ticket = this.Context.Tickets.Find(comment.TicketId);
            if (ticket == null)
            {
                throw new HttpException(404, "Ticket not found");
            }

            ticket.Comments.Add(dbComment);
            this.Context.SaveChanges();

            return dbComment;
        }

        public User GetAuthorComment(Comment dbComment)
        {
            dbComment.Author = this.Context.Users
                .FirstOrDefault(
                u => u.UserName == HttpContext.Current.User.Identity.Name);

            return dbComment.Author;
        }
    }
}