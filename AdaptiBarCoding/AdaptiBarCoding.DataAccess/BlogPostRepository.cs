using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using AdaptiBarCoding.ApplicationServices;
using AdaptiBarCoding.DataModel;

namespace AdaptiBarCoding.DataAccess
{
    public class BlogPostRepository : IBlogPostRepository
    {
        #region Private Readonly 

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<BlogPost> _dbSet;

        #endregion

        #region Constructors

        public BlogPostRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = applicationDbContext.Set<BlogPost>();
        }

        #endregion

        #region IBlogPostRepository Members

        public IQueryable<BlogPost> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IList<BlogPost>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<BlogPost> GetAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<BlogPost> SaveAsync(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                throw new ArgumentNullException(nameof(blogPost));
            }

            if (await GetAsync(blogPost.Id) == null)
            {
                return await InsertAsync(blogPost);
            }

            Update(blogPost);
            return blogPost;
        }

        #endregion

        #region Private Members

        private async Task<BlogPost> InsertAsync(BlogPost entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await _dbSet.AddAsync(entity);

            return result.Entity;
        }

        private void Update(BlogPost entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Attach(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
        }

        #endregion
    }
}
