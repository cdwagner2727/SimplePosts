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
    public class LoginController : Controller
    {
        PostDAL _dal = new PostDAL();

        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login(string username, string password)
        {
            User u;
            if (_dal.IsValid(username, password))
            {
                u = _dal.GetUserByUsername(username);
                Session["User"] = u;
                Session["Username"] = u.Username;
                return RedirectToAction("Index", "Home");
                //return View("AccountSummary", u);
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return View("Login");
        }

        public ActionResult AccountSummary()
        {
            if (Session["User"] == null)
            {
                return View("Login");
            }
            else
            {
                User u = Session["User"] as User;
                return View("AccountSummary", u);
            }
        }
        [HttpPost]
        public ActionResult UpdateUser(User u)
        {
            User temp = _dal.GetUserByUsername(u.Username);
            temp.FirstName = u.FirstName;
            temp.LastName = u.LastName;
            _dal.SaveChanges();
            Session["User"] = u;
            return View("AccountSummary", u);
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        //will call the database and check if username already exists, be upset with user if it does.
        public ActionResult RegisterUser(User u)
        {

            u.Joined = DateTime.Now.ToString();

            if (_dal.IsUnique(u))
            {
                _dal.Users.Add(u);
                _dal.SaveChanges();

                //change this to the home view with the current user.  But instead we should proably just add it to session data
                //so that we can stay consistant.  If no user in session data, redirect to login
                //return View("NewUserSummary", u);

                Session["User"] = u;
                Session["Username"] = u.Username;
                return RedirectToAction("Index", "Home", u);
            }
            else
            {
                //probably should change this... maybe?
                return View("RegisterError", u);
            }
        }
    }
}