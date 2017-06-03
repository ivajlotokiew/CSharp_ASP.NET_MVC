using System.Collections.Generic;

namespace LearningSystem.Models.ViewModels
{
    public class AdminAreaViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }

        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
