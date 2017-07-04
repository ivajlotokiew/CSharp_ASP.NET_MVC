namespace Blog.Web
{
    using AutoMapper;
    using Data;
    using Data.Migrations;
    using Models.EntityModels;
    using Models.ViewModels.Area;
    using Models.ViewModels.Comment;
    using Models.ViewModels.Posts;
    using Models.ViewModels.Tags;
    using System.Data.Entity;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<BlogDbContext, Configuration>());
            ConfigureMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Post, PostsViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(m => m.PostedOn, opt => opt.MapFrom(t => t.CreatedOn))
                .ReverseMap();
                expression.CreateMap<Post, PostsSortedByViewModel>()
                .ForMember(m => m.Comments, opt => opt.MapFrom(t => t.Comments.Count))
                .ReverseMap();
                expression.CreateMap<Comment, CommentViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(m => m.GuestName, opt => opt.MapFrom(t => t.GuestAuthor.Name))
                .ForMember(m => m.Email, opt => opt.MapFrom(t => t.Author.Email))
                .ForMember(m => m.GuestEmail, opt => opt.MapFrom(t => t.GuestAuthor.Email))
                .ReverseMap();
                expression.CreateMap<Tag, PostsByTagViewModel>()
                .ReverseMap();
                expression.CreateMap<Tag, TagsByPopularityViewModel>()
                .ForMember(m => m.Name, opt => opt.MapFrom(t => t.Name))
                .ReverseMap();
                expression.CreateMap<AddPostViewModel, Post>()
                .ReverseMap();

            });
        }
    }
}