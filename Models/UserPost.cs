using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SimplePosts.Models
{
    public class UserPost
    {
        public UserPost(){ }

        public UserPost(User u, Post p)
        {
            this.UserId = u.Id;
            this.PostId = p.Id;
        }
        public int UserId { get; set; }
        public int PostId { get; set; }
        [Key]
        public int Id { get; set; }
    }
}