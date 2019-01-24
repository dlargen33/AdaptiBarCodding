using System;
using System.ComponentModel.DataAnnotations;

namespace AdaptiBarCoding.DataModel
{
    public class BlogPost
    {
        [Display(Name = "Created date/time")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [Display(Name = "Id")]
        public long Id { get; set; }

        [Display(Name = "Blog text")]
        public string BlogText { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; } = true;

    }
}
