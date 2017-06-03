namespace Blog.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using System.Data.Entity;

    public class BlogDbContext : IdentityDbContext<User>
    {
        public BlogDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Guest> Guests { get; set; }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }
    }
}