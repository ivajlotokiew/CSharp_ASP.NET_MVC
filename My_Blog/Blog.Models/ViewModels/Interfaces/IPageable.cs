namespace Blog.Models.ViewModels.Interfaces
{
    public interface IPageable<T> where T : class
    {
        int CurrentPage { get; set; }

        int TotalPages { get; set; }

        T Attributes { get; set; }
    }
}
