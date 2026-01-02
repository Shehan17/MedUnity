using System.ComponentModel.DataAnnotations;

namespace MedUnity.Models
{
    public class WellnessUpdate
    {
        [Key]
        public int WellnessUpdateId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        [MaxLength(500)]
        public string? SourceUrl { get; set; }

        [MaxLength(255)]
        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
