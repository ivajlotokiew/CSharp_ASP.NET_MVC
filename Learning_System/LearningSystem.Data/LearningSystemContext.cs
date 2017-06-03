namespace LearningSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using System.Data.Entity;

    public class LearningSystemContext : IdentityDbContext<ApplicationUser>
    {
        public LearningSystemContext()
             : base(@"data source =.;
        initial catalog = LearningSystem;
        integrated security = True;
        MultipleActiveResultSets=True;
         App=EntityFramework"" providerName=""System.Data.SqlClient", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public static LearningSystemContext Create()
        {
            return new LearningSystemContext();
        }
    }
}