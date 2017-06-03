namespace TicketingSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Services;

    public class HomeController : Controller
    {
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService();
        }

        public ActionResult Index()
        {
            var ticketsVm = this.service.GetIndexViewModel();

            return View(ticketsVm);
        }

        public ActionResult Error()
        {
            return View();
        }
                    
        [ChildActionOnly]
        [OutputCache(Duration = 60 * 60)]
        public ActionResult MostCommentedTickets()
        {
            return PartialView("_MostCommentedTicketsPartial", this.service.GetIndexViewModel());
        }
    }
}