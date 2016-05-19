using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplePosts.Models;

namespace SimplePosts.ViewModel
{
    public class PostVM
    {
        public Post PostObj { get; set; }
        public List<Post> PostList { get; set; }
        public User UserObj { get; set; }
        public List<User> UserList {get; set;}
    }
}