using System.Collections.Generic;

namespace Laboratory56.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string NamePost { get; set; }
        public string ImagePath { get; set; }
        public int Counter = 0;
        public string CommentPost { get; set; }
        public int CommentPostCounter = 0;
        public string LikePost { get; set; }
        public int LikePostCounter = 0;
    }
}