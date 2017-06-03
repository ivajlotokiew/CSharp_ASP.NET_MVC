using LearningSystem.Models.EntityModels;
using AutoMapper;

namespace LearningSystem.Services
{
    public class AccountService : Service
    {
        public void CreateStudent(ApplicationUser user)
        {
            Student student = new Student();
            ApplicationUser currentUser = this.Context.Users.Find(user.Id);
            student.User = currentUser;
            this.Context.Students.Add(student);
            this.Context.SaveChanges();
        }
    }
}