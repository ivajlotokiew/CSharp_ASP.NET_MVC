using LearningSystem.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearningSystem.Models.ViewModels;

namespace LearningSystem.Web.Controllers
{
    [RoutePrefix("Users")]
    public class UsersController : Controller
    {
        private UsersService service;

        public UsersController()
        {
            this.service = new UsersService();
        }

        [HttpGet]
        [Route("Courses/{id}")]
        public ActionResult Courses(int id)
        {
            var userId = User.Identity.GetUserId();
            var userSignInCourse = this.service.CheckIfUserIsAlreadySignUp(id, userId);
            if (userSignInCourse)
            {
                this.service.SignOutOfCourse(id, userId);
            }
            else
            {
                this.service.SignInToCourse(id, userId);
            }

            IEnumerable<UserCoursesViewModel> courses = this.service.GetAllSutdentSignUpCourses(userId);

            return View(courses);
        }
    }
}