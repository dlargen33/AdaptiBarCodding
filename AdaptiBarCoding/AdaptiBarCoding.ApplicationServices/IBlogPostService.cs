using System.Collections.Generic;
using System.Threading.Tasks;
using AdaptiBarCoding.DataModel;

namespace AdaptiBarCoding.ApplicationServices
{
    public interface IBlogPostService
    {
        Task<IList<BlogPost>> GetAllBlogPostsAsync();

        Task<BlogPost> GetBlogPostAsync(long id);

        Task<BlogPost> AddBlogPostAsync(string messageText);

        Task DeleteBlogPostAsync(long id);
    }
}
