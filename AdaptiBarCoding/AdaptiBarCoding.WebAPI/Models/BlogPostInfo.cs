using System;

namespace AdaptiBarCoding.WebAPI.Models
{
    public class BlogPostInfo
    {
        public long Id { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string BlogText { get; set; }
    }
}
