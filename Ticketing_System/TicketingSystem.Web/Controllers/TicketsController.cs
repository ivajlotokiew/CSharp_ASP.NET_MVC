namespace TicketingSystem.Web.Controllers
{
    using Models.EntityModels;
    using Models.ViewModels.Comments;
    using Models.ViewModels.Tickets;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class TicketsController : Controller
    {
        private TicketsService service;

        public TicketsController()
        {
            this.service = new TicketsService();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            IEnumerable<CommentsViewModel> comments = this.service.GetAllCommentsByTicketId(id);
            TicketDetailsViewModel model = this.service.GetTicketById(id, comments);

            if (model == null)
            {
                throw new HttpException(404, "Ticket not found");
            }

            return this.View(model);
        }

        //public ActionResult GetFullDescriptionContent(int id)
        //{
        //    string descriptionContent = this.service.GetDescriptionContentByTicketId(id);

        //    return this.Content(descriptionContent);
        //}

        //public ActionResult GetLessDescriptionContent(int id)
        //{
        //    string descriptionContent = 
        //        this.service.GetDescriptionContentByTicketId(id).Substring(0, 100);

        //    return this.Content(descriptionContent);
        //}

        [Authorize]
        public ActionResult Add()
        {
            AddTicketViewModel addTicket = this.service.AddTicketVM();

            return this.View(addTicket);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddTicketViewModel ticket)
        {
            if (ticket != null && this.ModelState.IsValid)
            {
                this.service.AddTicketInDb(ticket);
                return this.RedirectToAction("All", "Tickets");
            }

            ticket.Categories = this.service.AddTicketVM().Categories;
            return this.View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult All(int? CategoryId, int id = 1)
        {
            IEnumerable<ListTicketViewModel> tickets;
            const int ItemsPerPage = 5;
            if (CategoryId != null)
            {
                tickets = this.service.GetTicketsByCategoryId(CategoryId);
                if (tickets.Count() / (ItemsPerPage * id) < 1)
                {
                    id = 1;
                }
            }
            else
            {
                tickets = this.service.GetAllTickets();
            }

            var page = id;
            var allItemsCount = tickets.Count();
            var totalPages = Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;
            var ticketsVm = tickets.Skip(itemsToSkip).Take(ItemsPerPage);

            var viewModel = new PageableListTicketViewModel
            {
                CurrentPage = page,
                TotalPages = (int)totalPages,
                Tickets = ticketsVm,
                Categories = this.service.GetAllCategories(),
                CategoryId = CategoryId
            };

            return View(viewModel);
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
    }
}