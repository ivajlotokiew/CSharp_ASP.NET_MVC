using AutoMapper;
using LearningSystem.Models.EntityModels;
using LearningSystem.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace LearningSystem.Services
{
    public class HomeService : Service
    {
        public IEnumerable<CourseViewModel> GetAllCourses()
        {
            IEnumerable<Course> coursesDB = this.Context.Courses;
            IEnumerable<CourseViewModel> coursesVM =
                Mapper.Map<IEnumerable<Course>, IEnumerable<CourseViewModel>>(coursesDB);

            return coursesVM;
        }

        public DetailsCourseViewModel CourseDetailsById(int id)
        {
            Course courseDB = this.Context.Courses.Find(id);
            DetailsCourseViewModel courseVM = Mapper.Map<Course, DetailsCourseViewModel>(courseDB);

            return courseVM;
        }
    }
}
