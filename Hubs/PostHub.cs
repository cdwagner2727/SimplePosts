using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using SimplePosts.DAL;
using SimplePosts.Models;

namespace SimplePosts.Hubs
{
    [HubName("hitCounter")]
    public class PostHub : Hub
    {
        PostDAL _dal = new PostDAL();
        private static int _hitCount = 0;

        public void RecordHit()
        {
            _hitCount += 1;
            this.Clients.All.onHitRecorded(_dal.GetUserByUsername("asdf").FirstName);
        }

        public void GetPosts()
        {

        }

        public static void SendPost(Post p)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<PostHub>();
            var json = JsonConvert.SerializeObject(p);
            hubContext.Clients.All.newPost(p);
        }
    }
}