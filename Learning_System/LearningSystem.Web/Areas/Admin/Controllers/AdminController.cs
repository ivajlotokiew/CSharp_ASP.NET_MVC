using LearningSystem.Models.ViewModels;
using LearningSystem.Services;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class AdminController : Controller
    {
        private AdminService service;

        public AdminController()
        {
            this.service = new AdminService();
        }

        [Route("Index")]
        public ActionResult Index()
        {
            IEnumerable<CourseViewModel> courses = this.service.GetAllCourses();
            IEnumerable<UserViewModel> users = this.service.GetAllUsers();
            AdminAreaViewModel adminVM = new AdminAreaViewModel
            {
                Courses = courses,
                Users = users
            };

            return View(adminVM);
        }

        [HttpGet]
        [Route("CourseEdit/{id}")]
        public ActionResult CourseEdit(int? id)
        {
            EditCourseViewModel courseVM = this.service.GetCourseById(id);

            return this.View(courseVM);
        }

        [HttpPost]
        [Route("CourseEdit/{id}")]
        public ActionResult CourseEdit([Bind(Include = "Id, Name, Description")] EditCourseViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.ChangeGivenCourse(model);
                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        [HttpGet]
        [Route("UserSetRole")]
        public ActionResult UserSetRole()
        {
            return this.View();
        }

        [HttpPost]
        [Route("UserSetRole")]
        public ActionResult UserSetRole(SetUserRoleViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.SetUserRole(model);    
            }

            return this.View();
        }
    }
}