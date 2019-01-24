using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdaptiBarCoding.ApplicationServices;
using AdaptiBarCoding.DataAccess;
using AdaptiBarCoding.DataModel;
using AdaptiBarCoding.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdaptiBarCoding.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BlogPostController : Controller
    {
        #region Private Readonly 
        private readonly ApplicationDbContext _dbContext;
        private readonly IBlogPostService _blogPostService;
        #endregion

        #region Constructors

        public BlogPostController(ApplicationDbContext context, IBlogPostService blogPostService)
        {
            _dbContext = context;
            _blogPostService = blogPostService;
        }

        #endregion

        #region Public Members
        // GET: api/<controller>
        [HttpGet("GetBlogPosts")]
        [Produces(typeof(IList<BlogPostInfo>))]
        public async Task<IActionResult> GetBlogPosts()
        {
           try
            {
                var blogPostInfos = new List<BlogPostInfo>();
                var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
                foreach(var blogPost in blogPosts)
                {
                    var blogPostInfo = new BlogPostInfo();
                    Map(blogPost, blogPostInfo);
                    blogPostInfos.Add(blogPostInfo);
                }
                return Ok(blogPostInfos);
            }
            catch (Exception ex)
            {
                return BadRequest(new WebApiError{ Message = ex.Message});
            }
        }

        [HttpPost("AddBlogPost")]
        [Produces(typeof(BlogPostInfo))]
        public async Task<IActionResult> AddBlogPost( [FromBody] BlogPostInfo blogPostInfo)
        {
            try
            {
                var savedBlogPostInfo = new BlogPostInfo();
                using (var ctx = _dbContext.Database.BeginTransaction())
                {
                    var blogPost = await _blogPostService.AddBlogPostAsync(blogPostInfo.BlogText);
                    await _dbContext.SaveChangesAsync();
                    ctx.Commit();
                    Map(blogPost, savedBlogPostInfo);
                }
            
                return Ok(savedBlogPostInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new WebApiError { Message = ex.Message });
            }
        }

        [HttpPost("DeleteBlogPost")]
        public async Task<IActionResult> DeleteBlogPost(long blogPostId)
        {
            try
            {
                using (var ctx = _dbContext.Database.BeginTransaction())
                {
                    await _blogPostService.DeleteBlogPostAsync(blogPostId);
                    await _dbContext.SaveChangesAsync();
                    ctx.Commit();
                }                 
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new WebApiError { Message = ex.Message });
            }
        }

        #endregion

        #region Private Members
        private BlogPostInfo Map(BlogPost source, BlogPostInfo target)
        {
            target.Id = source.Id;
            target.CreatedDateTime = source.CreatedDateTime;
            target.BlogText = source.BlogText;

            return target;
        }
        #endregion
    }
}
