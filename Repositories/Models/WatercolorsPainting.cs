using Repositories.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Repositories.Models;

public partial class WatercolorsPainting
{
    [Key]
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
    [Range(0,double.MaxValue)]
    public decimal? Price { get; set; }
    [Required]
    [Range(1000, int.MaxValue)]
    public int? PublishYear { get; set; }
    [Required]
    public DateTime? CreatedDate { get; set; } = DateTime.Now;
    [Required]
    public string? StyleId { get; set; }
    [JsonIgnore]
    public virtual Style? Style { get; set; }
}
