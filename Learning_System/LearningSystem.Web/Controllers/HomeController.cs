using LearningSystem.Models.ViewModels;
using LearningSystem.Services;
using LearningSystem.Web.Attributes;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System;
using LearningSystem.Models.ViewModels.PageableCourseList;

namespace LearningSystem.Web.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService();
        }

        [Route("Index/{id=1}")]
        public ActionResult Index(int id = 1)
        {
            const int ItemsPerPage = 4;
            var page = id;
            IEnumerable<CourseViewModel> coursesVM = this.service.GetAllCourses();
            var allItemsCount = coursesVM.Count();
            var totalPages = Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;
            var courses = coursesVM.OrderBy(x => x.Id).Skip(itemsToSkip).Take(ItemsPerPage);

            var viewModel = new PageableCourseListViewModel
            {
                CurrentPage = page,
                TotalPages = (int)totalPages,
                Courses = courses
            };

            return View(viewModel);
        }

        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet, Route("Details/{id?}")]
        public ActionResult Details(int id)
        {
            var courseDetail = this.service.CourseDetailsById(id);
            return this.View(courseDetail);
        }
    }
}