namespace portfolio_backend.Models
{
    public class ProjectDTO
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string[] TechStack { get; set; }
        public required string ImageUrl { get; set; }
        public required string GithubUrl { get; set; }
        public string? LiveUrl { get; set; }
    }
}
