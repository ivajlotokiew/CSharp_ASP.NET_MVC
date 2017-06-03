namespace TicketingSystem.Web
{
    using AutoMapper;
    using Data;
    using Data.Migrations;
    using Models.EntityModels;
    using Models.ViewModels.Administration;
    using Models.ViewModels.Comments;
    using Models.ViewModels.Home;
    using Models.ViewModels.Tickets;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<TicketSystemDbContext, Configuration>());
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
                expression.CreateMap<Ticket, TicketViewModel>()
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(t => t.Category.Name))
                .ForMember(m => m.AuthorUserName, opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(m => m.NumberOfComments, opt => opt.MapFrom(t => t.Comments.Count()))
                .ReverseMap();
                expression.CreateMap<Ticket, TicketDetailsViewModel>()
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(t => t.Category.Name))
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
                .ReverseMap();
                expression.CreateMap<Comment, CommentsViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
                .ReverseMap();
                expression.CreateMap<Ticket, AddTicketViewModel>().ReverseMap();
                expression.CreateMap<Ticket, ListTicketViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(m => m.Category, opt => opt.MapFrom(t => t.Category.Name))
                .ReverseMap();
                expression.CreateMap<Comment, PostCommentViewModel>().ReverseMap();
                expression.CreateMap<Comment, CommentInfoViewModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(m => m.Email, opt => opt.MapFrom(t => t.Author.Email))
                .ForMember(m => m.TicketDescription, opt => opt.MapFrom(t => t.Ticket.Description))
                .ForMember(m => m.CommentContent, opt => opt.MapFrom(t => t.Content))
                .ReverseMap();

            });
        }
    }
}