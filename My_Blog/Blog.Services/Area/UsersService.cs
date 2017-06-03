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
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Common;

    public class UsersService : BaseService
    {
        public User GetUserById(string id)
        {
            var user = this.Context.Users.Find(id);

            return user;
        }

        public void RemoveUserFromDb(User user)
        {
            IEnumerable<Comment> userComments =
                this.Context.Users.Find(user.Id).Comments.ToList();

            foreach (var comment in userComments)
            {
                this.Context.Comments.Remove(comment);
            }

            this.Context.Users.Remove(user);
            this.Context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> dbUsers =
                this.Context
                .Users
                .Where(x => x.UserName != GlobalConstants.AdminRole)
                .ToList();

            return dbUsers;
        }

        public void MakeUserEditor(string id)
        {
            var user = this.Context.Users.Find(id);
            this.UserManager.AddToRole(user.Id, GlobalConstants.EditorRole);
        }

        public bool CheckIfUserIsEditor(string id)
        {
            var userManager = this.UserManager;
            if (!userManager.IsInRole(id, GlobalConstants.EditorRole))
            {
                return false;
            }

            return true;
        }

        public IEnumerable<User> GetNotEditorUsers()
        {
            IEnumerable<User> dbUsers = this.Context.Users.ToList();
            IList<User> notEditorUsers = new List<User>();
            foreach (var user in dbUsers)
            {
                if (!this.CheckIfUserIsEditor(user.Id))
                {
                    if (user.UserName != GlobalConstants.AdminRole)
                    {
                        notEditorUsers.Add(user);
                    }
                }
            }

            return notEditorUsers;
        }
    }
}