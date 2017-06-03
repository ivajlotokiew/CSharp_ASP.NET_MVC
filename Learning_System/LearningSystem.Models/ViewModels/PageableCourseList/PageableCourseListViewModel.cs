using System.Collections.Generic;

namespace LearningSystem.Models.ViewModels.PageableCourseList
{
    public class PageableCourseListViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
