using System.ComponentModel.DataAnnotations;

namespace LearningSystem.Models.ViewModels
{
    public class EditCourseViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}