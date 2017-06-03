namespace Blog.Services
{
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Posts;
    using System.Linq;

    public class PostsService : BaseService
    {
        public PostsViewModel GetPostsById(int id)
        {
            Post dbPost = this.Context.Posts.Find(id);
            dbPost.Views++;
            this.Context.SaveChanges();
            PostsViewModel vmPost = Mapper.Map<PostsViewModel>(dbPost);

            return vmPost;
        }

        public PostsByTagViewModel GetTagInfoByTagName(string tagName)
        {
            Tag dbTag = this.Context.Tags.First(x => x.Name == tagName);
            PostsByTagViewModel vmTag = Mapper.Map<PostsByTagViewModel>(dbTag);

            return vmTag;
        }

        public Image GetImageById(int id)
        {
            var image = this.Context.Images.Find(id);

            return image;
        }
    }
}