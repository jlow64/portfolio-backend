using Microsoft.EntityFrameworkCore;
using portfolio_backend.Models;

namespace portfolio_backend.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<BlogPost> BlogPosts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().HasData(
                new()
                {
                    Id = 1,
                    Title = "Portfolio Site",
                    TechStack = ["React", "TypeScript", "ASP.NET", "Next.js"],
                    ImageUrl = "",
                    GithubUrl = ""
                },
                new()
                {
                    Id = 2,
                    Title = "Gym app",
                    TechStack = ["React", "TypeScript", "Next.js"],
                    ImageUrl = "",
                    GithubUrl = ""
                },
                new()
                {
                    Id = 3,
                    Title = "Some three.js practice",
                    TechStack = ["React", "TypeScript", "Three.js", "C"],
                    ImageUrl = "",
                    GithubUrl = ""
                }
            );

            modelBuilder.Entity<BlogPost>().HasData(
                new()
                {
                    Id = 1,
                    Title = "About me",
                    Content = ""
                },
                new()
                {
                    Id = 2,
                    Title = "What I'm doing now",
                    Content = ""
                }
            );
        }
    }
}
