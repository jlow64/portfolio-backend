using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_backend.Context;
using portfolio_backend.Models;

namespace portfolio_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<BlogPost>>> GetBlogPosts()
        {
            // Public facing

            return Ok(await _context.BlogPosts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPostById(int id)
        {
            // Public facing
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost is null)
                return NotFound();

            return Ok(blogPost);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<BlogPost>> AddBlogPost(BlogPost newBlogPost)
        {
            // TODO: Admin only
            // TODO: Need to make DTO
            if (newBlogPost == null)
                return BadRequest();

            _context.BlogPosts.Add(newBlogPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlogPostById), new { id = newBlogPost.Id }, newBlogPost);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, BlogPost updatedBlogPost)
        {
            // TODO: Admin only
            // TODO: Need to make DTO
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost is null)
                return NotFound();

            blogPost.Title = updatedBlogPost.Title;
            blogPost.Content = updatedBlogPost.Content;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            // Admin only
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost is null)
                return NotFound();

            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
