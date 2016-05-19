using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimplePosts.DAL;
using SimplePosts.Models;
using SimplePosts.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace SimplePosts.Controllers
{
    public class HomeController : Controller
    {

        PostDAL _dal = new PostDAL();
        PostVM _vm = new PostVM();

        public ActionResult Index()
        {
            var u = Session["User"] as User;
            if (u == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                _vm.PostList = _dal.GetPostsByUser(u);
                return View("Home", _vm);
            }
//            return View("Home", _vm);
        }
        
        [HttpPost]
        public ActionResult AddPost(Post p)
        {
            var u = Session["User"] as User;
            _dal.AddPost(p, u);
            _vm.PostList = _dal.GetPostsByUser(u);
            return View("Home", _vm);
        }

        //change this to also delete the corresponding UserPost... or maybe just the corresponding UserPost
        public ActionResult RemovePost(Post p)
        {
            _dal.Posts.Attach(p);
            _dal.Posts.Remove(p);
            _dal.SaveChanges();
            return View("Home");
        }

        //Looks like theres an issue here if you create a new post and submit an edit to it too quickly it hasn't yet been added to the db.
        public ActionResult UpdatePost(Post p)
        {
            Post oldPost = _dal.GetPostById(p.Id);
            oldPost.Author = p.Author;
            oldPost.Title = p.Title;
            oldPost.Content = p.Content;
            _dal.SaveChanges();
            return View("Home");
        }

        public ActionResult GetPostsByUser(User u)
        {
            _vm.PostList = _dal.GetPostsByUser(u);
            return View("Home", _vm);
        }


    }
}
