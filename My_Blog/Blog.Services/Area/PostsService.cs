namespace Blog.Services.Area
{
    using Models.EntityModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Models.ViewModels.Area;
    using System.IO;
    using AutoMapper;
    using Models.ViewModels.Posts;

    public class PostsService : BaseService
    {
        public Post GetPostById(int? id)
        {
            var post = this.Context.Posts.Find(id);

            return post;
        }

        public void EditPost(Post post)
        {
            Post dbPost = this.Context.Posts.Find(post.Id);
            dbPost.Title = post.Title;
            dbPost.Content = post.Content;
            dbPost.UpdatedOn = DateTime.Now;
            this.Context.SaveChanges();
        }

        public void RemovePostFromDb(Post post)
        {
            IEnumerable<Comment> userComments =
                this.Context.Posts.Find(post.Id).Comments.ToList();

            foreach (var comment in userComments)
            {
                this.Context.Comments.Remove(comment);
            }

            this.Context.Posts.Remove(post);
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public IEnumerable<PostsViewModel> GetPostsByTitleContent(string titleContent)
        {
            IEnumerable<Post> dbPosts
                = this.Context.Posts
                .Where(x => x.Title.Contains(titleContent))
                .ToList();

            IEnumerable<PostsViewModel> vmPosts =
                Mapper.Map<IEnumerable<PostsViewModel>>(dbPosts);

            return vmPosts;
        }

        public AddPostViewModel AddPostVm()
        {
            var AddPostViewModel = new AddPostViewModel
            {
                Categories = this.Context.Categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return AddPostViewModel;
        }

        public void AddPostDb(AddPostViewModel post)
        {
            Post dbPost = Mapper.Map<Post>(post);
            dbPost.CreatedOn = DateTime.Now;

            if (post.UploadedImage != null)
            {
                using (var memory = new MemoryStream())
                {
                    post.UploadedImage.InputStream.CopyTo(memory);
                    var content = memory.GetBuffer();

                    dbPost.Image = new Image
                    {
                        Content = content,
                        FileExtension = post.UploadedImage.FileName.Split(new[] { '.' }).Last()
                    };
                }
            }

            this.Context.Posts.Add(dbPost);
            this.Context.SaveChanges();
        }

        public Image GetImageById(int id)
        {
            var image = this.Context.Images.Find(id);

            return image;
        }
    }
}