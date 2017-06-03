namespace Blog.Web.Controllers
{
    using Models.ViewModels.Posts;
    using Services;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}