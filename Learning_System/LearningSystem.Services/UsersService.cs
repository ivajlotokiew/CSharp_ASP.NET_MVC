using LearningSystem.Models.EntityModels;
using System.Linq;
using System;
using LearningSystem.Models.ViewModels;
using System.Collections.Generic;
using AutoMapper;

namespace LearningSystem.Services
{
    public class UsersService : Service
    {
        public void SignInToCourse(int id, string userId)
        {
            Course currnetCourse = this.Context.Courses.Find(id);
            Student currentStudent = this.Context.Students.First(x => x.User.Id == userId);
            currentStudent.Courses.Add(currnetCourse);
            this.Context.SaveChanges();
        }

        public bool CheckIfUserIsAlreadySignUp(int id, string userId)
        {
            Student currentStudent = this.Context.Students.First(x => x.User.Id == userId);
            if (currentStudent.Courses.Any(x => x.Id == id))
            {
                return true;
            }

            return false;
        }

        public void SignOutOfCourse(int id, string userId)
        {
            Course currnetCourse = this.Context.Courses.Find(id);
            Student currentStudent = this.Context.Students.First(x => x.User.Id == userId);
            currentStudent.Courses.Remove(currnetCourse);
            this.Context.SaveChanges();
        }

        public IEnumerable<UserCoursesViewModel> GetAllSutdentSignUpCourses(string userId)
        {
            Student currentStudent = this.Context.Students.First(x => x.User.Id == userId);
            IEnumerable<Course> courses = this.Context.Students.First(x => x.User.Id == userId).Courses;
            IEnumerable<UserCoursesViewModel> userCourses =
                Mapper.Map<IEnumerable<Course>, IEnumerable<UserCoursesViewModel>>(courses);

            return userCourses;
        }
    }
}
