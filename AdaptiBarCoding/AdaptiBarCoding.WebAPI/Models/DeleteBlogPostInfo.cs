using System.ComponentModel.DataAnnotations;

namespace AdaptiBarCoding.WebAPI.Models
{
    public class DeleteBlogPostInfo
    {
        [Display(Name = "Id")]
        public long BlogPostId { get; set; }
    }
}
