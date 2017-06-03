namespace Blog.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Guest
    {
        private ICollection<Comment> comments;
 
        public Guest()
        {
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}