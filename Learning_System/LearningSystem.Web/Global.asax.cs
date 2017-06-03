using AutoMapper;
using LearningSystem.Models.EntityModels;
using LearningSystem.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LearningSystem.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
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
                expression.CreateMap<Course, CourseViewModel>();
                expression.CreateMap<Course, DetailsCourseViewModel>();
                expression.CreateMap<Course, UserCoursesViewModel>();
                expression.CreateMap<Student, UserViewModel>()
                .ForMember(vm => vm.Username, ce => ce.MapFrom(st => st.User.UserName));
            });
        }
    }
}
