using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class StockUpdateDto
    {
        [Required]
        [StringLength(10, ErrorMessage = "Symbol cannot be longer than 10 characters.")]
        [MinLength(1, ErrorMessage = "Symbol must be at least 1 character long.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [StringLength(30, ErrorMessage = "Company name cannot be longer than 30 characters.")]
        [MinLength(3, ErrorMessage = "Company name must be at least 3 characters long.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Company name can only contain alphanumeric characters and spaces.")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.01, 100, ErrorMessage = "Last Dividend must be in range 0.01 to 100.")]
        public decimal LastDiv { get; set; }
        [Required]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "Market Cap must be a non-negative number.")]
        public long MarketCap { get; set; }
    }
}
