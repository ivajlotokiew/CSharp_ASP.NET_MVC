using System.Collections.Generic;
using LearningSystem.Models.ViewModels;
using LearningSystem.Models.EntityModels;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;

namespace LearningSystem.Services
{
    public class AdminService : Service
    {
        public AdminService()
        {
        }

        public IEnumerable<CourseViewModel> GetAllCourses()
        {
            IEnumerable<Course> coursesDB = this.Context.Courses;
            IEnumerable<CourseViewModel> coursesVM =
                Mapper.Map<IEnumerable<Course>, IEnumerable<CourseViewModel>>(coursesDB);

            return coursesVM;
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            IEnumerable<Student> usersDB = this.Context.Students;
            IEnumerable<UserViewModel> usersVM =
                Mapper.Map<IEnumerable<Student>, IEnumerable<UserViewModel>>(usersDB);

            return usersVM;
        }

        public EditCourseViewModel GetCourseById(int? id)
        {
            Course currentCourse = this.Context.Courses.Find(id);

            var courseVM = new EditCourseViewModel
            {
                Id = id,
                Description = currentCourse.Description,
                Name = currentCourse.Name
            };

            return courseVM;
        }

        public void ChangeGivenCourse(EditCourseViewModel model)
        {
            var course = this.Context.Courses.Find(model.Id);
            course.Name = model.Name;
            course.Description = model.Description;
            this.Context.SaveChanges();
        }

        public void SetUserRole(SetUserRoleViewModel model)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(this.Context);
            var manage = new UserManager<ApplicationUser>(store);
            try
            {
                manage.AddToRole(userId, model.Role);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.InnerException);
            }
        }
    }
}