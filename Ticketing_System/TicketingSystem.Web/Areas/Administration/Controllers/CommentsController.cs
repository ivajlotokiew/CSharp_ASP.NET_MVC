namespace TicketingSystem.Web.Areas.Administration.Controllers
{
    using Models.EntityModels;
    using Models.ViewModels.Administration;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class CommentsController : Controller
    {
        private AdministrationService service;

        public CommentsController()
        {
            this.service = new AdministrationService();
        }

        // GET: Administration/Comments
        public ActionResult Index(int id = 1)
        {
            IEnumerable<CommentInfoViewModel> comments = this.service.GetAllComments();
            const int ItemsPerPage = 5;
            var page = id;
            var allItemsCount = comments.Count();
            var totalPages = Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;
            var commentsVm = comments.Skip(itemsToSkip).Take(ItemsPerPage);

            var viewModel = new PageableListCommentsViewModel<CommentInfoViewModel>
            {
                CurrentPage = page,
                TotalPages = (int)totalPages,
                Comments = commentsVm
            };

            return View(viewModel);
        }

        // GET: Administration/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = this.service.GetCommentById(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // GET: Administration/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = this.service.GetCommentById(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // POST: Administration/Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Content, TicketId, AuthorId")] Comment comment)
        {
            if (this.ModelState.IsValid)
            {
                this.service.ModifiedState(comment);
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        // GET: Administration/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = this.service.GetCommentById(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // POST: Administration/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = this.service.GetCommentById(id);
            this.service.RemoveCommentFromDb(comment);

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