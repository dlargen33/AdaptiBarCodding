using System.Collections.Generic;
using System.Linq;
using AdaptiBarCoding.DataModel;
using System.Threading.Tasks;

namespace AdaptiBarCoding.ApplicationServices
{
    public interface IBlogPostRepository
    {
        IQueryable<BlogPost> AsQueryable();

        Task<BlogPost> GetAsync(long id);

        Task<IList<BlogPost>> GetAllAsync();

        Task<BlogPost> SaveAsync(BlogPost blogPost);
    }
}
