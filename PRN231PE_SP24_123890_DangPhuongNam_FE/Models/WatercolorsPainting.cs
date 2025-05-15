using PRN231PE_SP24_123890_DangPhuongNam_FE.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PRN231PE_SP24_123890_DangPhuongNam_FE.Models
{
    public class WatercolorsPainting
    {
        [Required]
        public string PaintingId { get; set; } = null!;
        [Required]
        [FullnameValidation]
        public string PaintingName { get; set; } = null!;
        [Required]
        public string? PaintingDescription { get; set; }
        [Required]
        public string? PaintingAuthor { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }
        [Required]
        [Range(1000, int.MaxValue)]
        public int? PublishYear { get; set; }
        [Required]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public string? StyleId { get; set; }
        public virtual Style? Style { get; set; }

    }
}
