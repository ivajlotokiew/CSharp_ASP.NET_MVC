namespace Blog.Web.Areas.Administration.Controllers
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Models.EntityModels;
    using Models.ViewModels.Area;
    using Models.ViewModels.Posts;
    using Services.Area;
    using System.Collections.Generic;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Web.Controllers;

    [Authorize(Roles = "Admin, Editor")]
    public class PostsController : BaseController
    {
        private PostsService service;

        public PostsController()
        {
            this.service = new PostsService();
        }

        // GET: Administration/Comments
        public ActionResult Index(int? id = 1)
        {
            IEnumerable<PostsViewModel> vmPosts = this.service.GetAllPosts();

            return View(vmPosts);
        }

        public ActionResult SearchByTitleContent(string titleContent)
        {
            IEnumerable<PostsViewModel> vmModel =
               this.service.GetPostsByTitleContent(titleContent);

            return this.PartialView("_SearchByTitleContentPartial", vmModel);
        }

        // GET: MyArea/Posts/Create
        public ActionResult Create()
        {
            AddPostViewModel addPost = this.service.AddPostVm();
            addPost.AuthorId = User.Identity.GetUserId();

            return View(addPost);
        }

        // POST: MyArea/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddPostViewModel post)
        {
            if (ModelState.IsValid)
            {
                this.service.AddPostDb(post);

                return RedirectToAction("Index");
            }

            return View(post);
        }

        public ActionResult Image(int id)
        {
            Image image = this.service.GetImageById(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.FileExtension);
        }

        // GET: Administration/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = this.service.GetPostById(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // GET: Administration/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = this.service.GetPostById(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: Administration/Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Content, Title")] Post post)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditPost(post);

                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Administration/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = this.service.GetPostById(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: Administration/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = this.service.GetPostById(id);
            this.service.RemovePostFromDb(post);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.service.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}