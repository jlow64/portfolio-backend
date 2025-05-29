using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_backend.Context;
using portfolio_backend.Models;

namespace portfolio_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
            // Public facing
            return Ok(await _context.Projects.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            // Public facing
            var project = await _context.Projects.FindAsync(id);
            if (project is null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> AddProject(Project newProject)
        {
            // TODO: Admin only
            // TODO: Need to make DTO
            if (newProject == null)
                return BadRequest();

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectById), new { id = newProject.Id }, newProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project updatedProject)
        {
            // TODO: Admin only
            // TODO: Need to make DTO
            var project = await _context.Projects.FindAsync(id);
            if (project is null)
                return NotFound();

            project.Title = updatedProject.Title;
            project.Description = updatedProject.Description;
            project.TechStack = updatedProject.TechStack;
            project.ImageUrl = updatedProject.ImageUrl;
            project.GithubUrl = updatedProject.GithubUrl;
            project.LiveUrl = updatedProject.LiveUrl;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            // TODO: Admin only
            // TODO: Need to make DTO
            var project = await _context.Projects.FindAsync(id);
            if (project is null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
