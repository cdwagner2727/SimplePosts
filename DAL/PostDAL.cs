using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SimplePosts.Models;

namespace SimplePosts.DAL
{
    public class PostDAL : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>().ToTable("tblPosts");
            modelBuilder.Entity<User>().ToTable("tblUsers");
            modelBuilder.Entity<UserPost>().ToTable("tblUserPosts");
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }

        //returns true if username is unique; false otherwise
        public bool IsUnique(User u)
        {
            if (this.Users.Any(x => x.Username == u.Username))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public User GetUserByUsername(string username)
        {
            return this.Users.First(n => n.Username == username);
        }

        //login validation: finds a User that matches the username and verifies the provided password is valid.  Returns true/false accordingly.
        public bool IsValid(string username, string password)
        {
            bool isValid = false;
            if (this.Users.Any(x => x.Username == username))
            {
                User x = this.Users.First(n => n.Username == username);
                if (x.Password == password)
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        //returns a List of Posts by a particular User.
        //Ran into a case where the UserPost table had not 
        public List<Post> GetPostsByUser(User u)
        {
            List<Post> posts = new List<Post>();
            List<int> uPosts = this.UserPosts.Where(p => p.UserId == u.Id).Select(p => p.PostId).ToList();
            uPosts.ForEach(delegate(int id)
            {
                Post post = this.Posts.FirstOrDefault(p => p.Id == id);
                if (post == null){
                    this.UserPosts.Remove(this.UserPosts.FirstOrDefault(p => p.PostId == id));
                }
                else
                {
                    posts.Add(post);
                }
            });
            this.SaveChanges();
            return posts;
        }

        //issue here when editing newly created items too quickly.
        public Post GetPostById(int id)
        {
            return this.Posts.FirstOrDefault(p => p.Id == id);
        }

        //need to add post, save changes, then get post again in order to get dynamically created Post Id.
        public int AddPost(Post p, User u)
        {
            p.Author = u.Username;
            this.Posts.Add(p);
            this.SaveChanges();
            UserPost uPost = new UserPost(u, p);
            this.AddUserPost(uPost);
            return p.Id;
        }

        private void AddUserPost(UserPost uPost)
        {
            this.UserPosts.Add(uPost);
            this.SaveChanges();
        }

        internal List<Post> GetAllPosts()
        {
            return this.Posts.ToList<Post>();
        }
    }
}