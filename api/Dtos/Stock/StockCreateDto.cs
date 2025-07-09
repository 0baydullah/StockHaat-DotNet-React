using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class StockCreateDto
    {
        [Required]
        [StringLength(10, ErrorMessage = "Symbol cannot be longer than 10 characters.")]
        [MinLength(1, ErrorMessage = "Symbol must be at least 1 character long.")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Symbol can only contain uppercase letters and numbers.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [StringLength(30, ErrorMessage = "Company name cannot be longer than 30 characters.")]
        [MinLength(3, ErrorMessage = "Company name must be at least 3 character long.")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.01, 100, ErrorMessage = "Quantity must be in range 0.01 to 100.")]
        public decimal LastDiv { get; set; }
        [Required]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "Market Cap must be a non-negative number.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Market Cap must be a valid number.")]
        public long MarketCap { get; set; }
    }
}
