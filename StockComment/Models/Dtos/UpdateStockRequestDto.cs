using System.ComponentModel.DataAnnotations;

namespace StockComment.Models.Dtos
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(5, ErrorMessage = "Symbol cannot over 5 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot over 10 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry name cannot over 10 characters")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 50000000)]
        public long MarketCap { get; set; }
    }
}
