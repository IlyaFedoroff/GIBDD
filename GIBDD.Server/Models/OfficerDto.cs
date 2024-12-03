using System.ComponentModel.DataAnnotations;

namespace GIBDD.Server.Models
{
    public class OfficerDto
    {
        public int OfficerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string Position { get; set; } = null!;
    }
}
