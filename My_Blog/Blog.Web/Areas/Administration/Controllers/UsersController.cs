namespace Blog.Web.Areas.Administration.Controllers
{
    using Models.EntityModels;
    using Services.Area;
    using System.Collections.Generic;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Web.Controllers;

    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {
        private UsersService service;

        public UsersController()
        {
            this.service = new UsersService();
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = this.service.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        public ActionResult Index(string id)
        {
            IEnumerable<User> vmUsers = this.service.GetAllUsers();

            return View(vmUsers);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = this.service.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = this.service.GetUserById(id);
            this.service.RemoveUserFromDb(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editor()
        {
            var vmModel = this.service.GetNotEditorUsers();

            return this.View(vmModel);
        }

        [HttpPost]
        public ActionResult Editor(string id)
        {
            if (!this.ModelState.IsValid || id == null)
            {
                throw new HttpException(404, "User id is not found");
            }

            var UserIsEditor = this.service.CheckIfUserIsEditor(id);
            if (!UserIsEditor)
            {
                this.RedirectToAction("Index");
            }

            this.service.MakeUserEditor(id);
            return this.RedirectToAction("Index", "Users");
        }
    }
}