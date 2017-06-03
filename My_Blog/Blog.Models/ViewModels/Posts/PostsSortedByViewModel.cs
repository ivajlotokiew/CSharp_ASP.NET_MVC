namespace Blog.Models.ViewModels.Posts
{
    public class PostsSortedByViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? Views { get; set; }

        public int? Comments { get; set; }
    }
}