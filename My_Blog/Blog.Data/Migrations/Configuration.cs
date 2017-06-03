namespace Blog.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public sealed class Configuration : DbMigrationsConfiguration<BlogDbContext>
    {
        private IRandomGenerator random;
        private UserManager<User> userManager;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = new RandomGenerator();
        }

        protected override void Seed(BlogDbContext context)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(context));
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedCategoriesWithPostsWithComments(context);
        }

        private void SeedCategoriesWithPostsWithComments(BlogDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var image = this.GetSampleImage();
            var users = context.Users.Take(3).ToList();
            for (int i = 0; i < 5; i++)
            {
                var category = new Category
                {
                    Name = this.random.RandomString(5, 20)
                };

                for (int j = 0; j < 10; j++)
                {
                    var post = new Post
                    {
                        Author = context.Users.First(x => x.UserName == GlobalConstants.AdminRole),
                        Title = this.random.RandomString(5, 40),
                        Content = this.random.RandomString(50, 500),
                        Image = image,
                        CreatedOn = DateTime.Now
                    };

                    for (int z = 0; z < 5; z++)
                    {
                        var comment = new Comment
                        {
                            Author = users[random.RandomNumber(0, users.Count - 1)],
                            Content = this.random.RandomString(50, 200),
                            CreatedOn = DateTime.Now
                        };

                        var tag = new Tag
                        {
                            Name = this.random.RandomString(3, 10)
                        };

                        post.Comments.Add(comment);
                        post.Tags.Add(tag);
                    }

                    category.Posts.Add(post);
                }

                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        private Image GetSampleImage()
        {
            var directory = AssemblyHelpers.GetDirectoryForAssembly(Assembly.GetExecutingAssembly());
            var file = File.ReadAllBytes(directory + "/Migrations/Images/cat.jpg");
            var image = new Image
            {
                Content = file,
                FileExtension = "jpg"
            };

            return image;
        }

        private void SeedRoles(BlogDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole
            {
                Name = GlobalConstants.AdminRole,
            });

            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole
            {
                Name = GlobalConstants.EditorRole,
            });

            context.SaveChanges();
        }

        private void SeedUsers(BlogDbContext context)
        {
            if (!context.Users.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    var user = new User
                    {
                        Email = $"{this.random.RandomString(6, 16)}@{this.random.RandomString(6, 16)}.com",
                        UserName = this.random.RandomString(6, 16)
                    };

                    this.userManager.Create(user, "123456");
                }

                var adminUser = new User
                {
                    Email = "admin@admin.com",
                    UserName = GlobalConstants.AdminRole
                };

                this.userManager.Create(adminUser, "admin4o");
                this.userManager.AddToRole(adminUser.Id, GlobalConstants.AdminRole);
            }
        }
    }
}