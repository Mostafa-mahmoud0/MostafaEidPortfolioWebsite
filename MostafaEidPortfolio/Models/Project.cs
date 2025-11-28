// In Models/Project.cs
namespace MostafaEidPortfolio.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } // Path to the project's image
        public string TechnologiesUsed { get; set; } // e.g., "C#, .NET, SQL"
        public string ProjectUrl { get; set; } // Optional: Link to the live project or GitHub repo
        public DateTime CreatedDate { get; set; }
    }
}