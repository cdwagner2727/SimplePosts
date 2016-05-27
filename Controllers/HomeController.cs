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
            _vm.UserObj = u;
            if (u == null)
            {
<<<<<<< HEAD
                _vm.PostList = _dal.GetAllPosts(true);
                //pass it "special" Guest user object
                _vm.UserObj = _dal.GetUserByUsername("Guest");
                return View("Home", _vm);
                //return RedirectToAction("Login", "Login");
=======
                //_vm.PostList = _dal.GetAllPosts();
                //return View("Home", _vm);
                return RedirectToAction("Login", "Login");
>>>>>>> origin/master
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
            if (ModelState.IsValid)
            {
                var temp = _dal.AddPost(p, u);
                _vm.PostList = _dal.GetPostsByUser(u);
                return Content(temp.ToString());
            }
            else
            {
                _vm.PostList = _dal.GetPostsByUser(u);
                return Content("false");
                //return View("Home", _vm);
            }
        }

        //change this to also delete the corresponding UserPost... or maybe just the corresponding UserPost
        public ActionResult RemovePost(Post p)
        {
            _dal.Posts.Attach(p);
            _dal.Posts.Remove(p);
            _dal.SaveChanges();
<<<<<<< HEAD
            _vm.UserObj = _dal.GetUserByPost(p);
            //_vm.UserObj = _dal.GetUserByUsername(p.Author);
=======
            _vm.UserObj = _dal.GetUserByUsername(p.Author);
>>>>>>> origin/master
            _vm.PostList = _dal.GetPostsByUser(_vm.UserObj);
            return View("Home", _vm);
        }

        //Looks like theres an issue here if you create a new post and submit an edit to it too quickly it hasn't yet been added to the db.
        public ActionResult UpdatePost(Post p)
        {
            Post oldPost = _dal.GetPostById(p.Id);
            //oldPost.Author = p.Author;
            oldPost.Title = p.Title;
            oldPost.Content = p.Content;
            oldPost.Public = p.Public;
            _dal.SaveChanges();
<<<<<<< HEAD
            _vm.UserObj = _dal.GetUserByPost(p);
//            _vm.UserObj = _dal.GetUserByUsername(p.Author);
=======
            _vm.UserObj = _dal.GetUserByUsername(p.Author);
>>>>>>> origin/master
            _vm.PostList = _dal.GetPostsByUser(_vm.UserObj);
            return View("Home", _vm);
        }

        public ActionResult GetPostsByUser(User u)
        {
            _vm.PostList = _dal.GetPostsByUser(u);
            return View("Home", _vm);
        }


    }
}
