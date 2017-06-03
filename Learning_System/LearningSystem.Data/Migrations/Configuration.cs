namespace LearningSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Models.EntityModels;
    using System;

    public sealed class Configuration : DbMigrationsConfiguration<LearningSystemContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LearningSystemContext context)
        {
            if (!context.Courses.Any())
            {
                for (int i = 0; i < 15; i++)
                {
                    context.Courses.Add(new Course
                    {
                        Name = $"Computer Science {i}",
                        Description = "Курсът цели да обогати и даде обща престава за това, какво са компютърните науки и какво приложение имат те в практиката.",
                        StartDate = DateTime.Now.AddDays(i),
                        EndDate = DateTime.Now.AddDays(30 + i)
                    });
                }
            }

            if (!context.Roles.Any(x => x.Name == "Student"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Student");
                manager.Create(role);
            }

            if (!context.Roles.Any(x => x.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Admin");
                manager.Create(role);
            }

            if (!context.Roles.Any(x => x.Name == "Trainer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Trainer");
                manager.Create(role);
            }

            if (!context.Roles.Any(x => x.Name == "BlogAuthor"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("BlogAuthor");
                manager.Create(role);
            }
        }
    }
}