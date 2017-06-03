namespace TicketingSystem.Services
{
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Comments;
    using Models.ViewModels.Tickets;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System;

    public class TicketsService : BaseService
    {
        public TicketDetailsViewModel GetTicketsById(int id)
        {
            Ticket ticket = this.Context.Tickets.Find(id);
            TicketDetailsViewModel ticketVm = Mapper.Map<Ticket, TicketDetailsViewModel>(ticket);

            return ticketVm;
        }

        public Image GetImageById(int id)
        {
            var image = this.Context.Images.Find(id);

            return image;
        }

        public IEnumerable<CommentsViewModel> GetAllCommentsByTicketId(int id)
        {
            IEnumerable<Comment> commentsDb = this.Context.Tickets
                .Find(id)
                .Comments
                .OrderByDescending(c => c.Id);

            IEnumerable<CommentsViewModel> commentsVm =
                Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentsViewModel>>(commentsDb);

            return commentsVm;
        }

        public string GetDescriptionContentByTicketId(int id)
        {
            string description = this.Context.Tickets.Find(id).Description;

            return description;
        }

        public AddTicketViewModel AddTicketVM()
        {
            var addTicketViewModel = new AddTicketViewModel
            {
                Categories = this.Context.Categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return addTicketViewModel;
        }

        public void AddTicketInDb(AddTicketViewModel ticket)
        {
            Ticket dbTicket =
                Mapper.Map<Ticket>(ticket);
            dbTicket.Author = this.Context.Users
                .First(x => x.UserName == HttpContext.Current.User.Identity.Name);
            if (ticket.UploadedImage != null)
            {
                using (var memory = new MemoryStream())
                {
                    ticket.UploadedImage.InputStream.CopyTo(memory);
                    var content = memory.GetBuffer();
                    dbTicket.Image = new Image
                    {
                        Content = content,
                        FileExtension = ticket.UploadedImage.FileName.Split('.').Last()
                    };
                }
            }

            this.Context.Tickets.Add(dbTicket);
            this.Context.SaveChanges();
        }

        public IEnumerable<ListTicketViewModel> GetTicketsByCategoryId(int? categoryId)
        {
            IEnumerable<Ticket> ticketsDb = this.Context.Tickets.Where(x => x.CategoryId == categoryId).ToList();
            IEnumerable<ListTicketViewModel> ticketsVm =
                Mapper.Map<IEnumerable<Ticket>, IEnumerable<ListTicketViewModel>>(ticketsDb);

            return ticketsVm;
        }

        public IEnumerable<ListTicketViewModel> GetAllTickets()
        {
            IEnumerable<Ticket> ticketsDb = this.Context.Tickets.ToList();
            IEnumerable<ListTicketViewModel> ticketsVm =
                Mapper.Map<IEnumerable<Ticket>, IEnumerable<ListTicketViewModel>>(ticketsDb);

            return ticketsVm;
        }

        public IEnumerable<SelectListItem> GetAllCategories()
        {
            var categories = this.Context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            return categories;
        }

        public TicketDetailsViewModel GetTicketById(int id, IEnumerable<CommentsViewModel> comments)
        {
            Ticket ticket = this.Context.Tickets.Find(id);
            TicketDetailsViewModel ticketVm = Mapper.Map<Ticket, TicketDetailsViewModel>(ticket);
            foreach (var comment in comments)
            {
                ticketVm.Comments.Add(comment);
            }

            return ticketVm;
        }
    }
}