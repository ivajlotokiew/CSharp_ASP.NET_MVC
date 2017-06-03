namespace Blog.Services
{
    using AutoMapper;
    using Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using Models.ViewModels.Posts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class BaseService
    {
        private BlogDbContext context;
        private UserManager<User> userManager;
        private UserStore<User> userStore;

        public BaseService()
        {
            this.context = new BlogDbContext();
            this.userStore = new UserStore<User>(context);
            this.userManager = new UserManager<User>(userStore);
        }

        public UserManager<User> UserManager
        {
            get { return this.userManager; }
        }

        public BlogDbContext Context
        {
            get { return this.context; }
        }

        public IEnumerable<PostsSortedByViewModel> GetFirstFiveMostViewdPosts()
        {
            IEnumerable<Post> dbPosts = this.Context.Posts
                .OrderByDescending(x => x.Views)
                .Take(5)
                .ToList();

            IEnumerable<PostsSortedByViewModel> vmPosts =
                Mapper.Map<IEnumerable<PostsSortedByViewModel>>(dbPosts);

            return vmPosts;
        }

        public IEnumerable<PostsSortedByViewModel> GetFirstFiveMostCommentedPosts()
        {
            IEnumerable<Post> dbPosts = this.Context.Posts
                .OrderByDescending(x => x.Comments.Count)
                .Take(5)
                .ToList();

            IEnumerable<PostsSortedByViewModel> vmPosts =
                Mapper.Map<IEnumerable<PostsSortedByViewModel>>(dbPosts);

            return vmPosts;
        }

        public IEnumerable<PostsViewModel> GetAllPosts()
        {
            IEnumerable<Post> dbPosts = this.Context.Posts.ToList();
            IEnumerable<PostsViewModel> vmPosts =
                Mapper.Map<IEnumerable<Post>, IEnumerable<PostsViewModel>>(dbPosts);

            return vmPosts;
        }

        public IEnumerable<SelectListItem> GetAllCategories()
        {
            List<SelectListItem> categories = new List<SelectListItem>() { new SelectListItem
            {
                Value = "-1",
                Text = "Choose category"
            }};

            categories.AddRange(this.Context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }));

            return categories;
        }

        public IEnumerable<PostsViewModel> GetPostsByCategoryId(int? id)
        {
            if (id == -1)
            {
                return this.GetAllPosts();
            }

            IEnumerable<Post> dbPosts = this.Context.Posts.Where(x => x.CategoryId == id).ToList();
            IEnumerable<PostsViewModel> vmPosts =
                Mapper.Map<IEnumerable<PostsViewModel>>(dbPosts);

            return vmPosts;
        }
    }
}