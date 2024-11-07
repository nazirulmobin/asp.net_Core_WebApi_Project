using System.ComponentModel.DataAnnotations;

namespace NZWalks.api.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be a maximum of 3 charecter")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be a maximum of 3 charecter")]
        public string Description { get; set; }
        [Required]
        [Range(0,50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
    }
}
