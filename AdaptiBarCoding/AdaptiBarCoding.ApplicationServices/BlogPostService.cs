using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AdaptiBarCoding.DataModel;

namespace AdaptiBarCoding.ApplicationServices
{
    public class BlogPostService : IBlogPostService
    {
        #region Private Readonly Fields

        private readonly IBlogPostRepository _blogPostRepository;

        #endregion

        #region Constructors

        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        #endregion

        #region IBlogPostService members
        public async Task<BlogPost> AddBlogPostAsync(string messageText)
        {
            var blogPost = new BlogPost
            {
                CreatedDateTime = DateTime.Now,
                BlogText = messageText,
                Active = true
            };
            return await _blogPostRepository.SaveAsync(blogPost);
        }

        public async Task DeleteBlogPostAsync(long id)
        {
            var blogPost = await _blogPostRepository.AsQueryable().SingleOrDefaultAsync (x => x.Id == id);         
           
            if (blogPost == null)
            {
                throw new Exception($"Specified Blog Post does not exist. Blog Post Id was [{id}].");
            }

            blogPost.Active = false;
            await _blogPostRepository.SaveAsync(blogPost);
            return;
        }

        public async Task<IList<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _blogPostRepository.AsQueryable()
                .Where(x => x.Active == true)
                .OrderBy(x => x.CreatedDateTime)
                .ToListAsync();
        }

        public async Task<BlogPost> GetBlogPostAsync(long id)
        {
            var blogPost = await _blogPostRepository.AsQueryable().SingleOrDefaultAsync(x => x.Id == id);

            if (blogPost == null)
            {
                throw new Exception($"Specified Blog Post does not exist. Blog Post Id was [{id}].");
            }

            return blogPost;
        }

        #endregion  
    }
}
