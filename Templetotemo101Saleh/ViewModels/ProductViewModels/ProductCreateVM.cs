using System.ComponentModel.DataAnnotations;

namespace Templetotemo101Saleh.ViewModels.ProductViewModels;

public class ProductCreateVM
{
    [Required,MaxLength(256),MinLength(3)]
    public string Name { get; set; } = string.Empty;
    [Required,MaxLength(1024),MinLength(3)]
    public string Description { get; set; } = string.Empty;
    [Range(0,double.MaxValue)]
    public decimal Price { get; set; }
    [Range (0,5)]
    public int Rating { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public IFormFile Image { get; set; } = null!;
}