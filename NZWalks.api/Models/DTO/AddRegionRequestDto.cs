using System.ComponentModel.DataAnnotations;

namespace NZWalks.api.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 charecter")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 charecter")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 charecter")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
