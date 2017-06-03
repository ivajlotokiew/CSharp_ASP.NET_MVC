namespace TicketingSystem.Services
{
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Administration;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class AdministrationService : BaseService
    {
        public Comment GetCommentById(int? id)
        {
            var comment = this.Context.Comments.Find(id);

            return comment;
        }

        public void ModifiedState(Comment comment)
        {
            this.Context.Entry(comment).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public IEnumerable<CommentInfoViewModel> GetAllComments()
        {
            IEnumerable<Comment> dbComments = this.Context.Comments.ToList();
            IEnumerable<CommentInfoViewModel> vmComments =
                Mapper.Map<IEnumerable<CommentInfoViewModel>>(dbComments);

            return vmComments;
        }

        public void RemoveCommentFromDb(Comment comment)
        {
            this.Context.Comments.Remove(comment);
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}