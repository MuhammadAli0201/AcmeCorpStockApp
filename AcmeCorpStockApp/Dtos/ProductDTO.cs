using AcmeCorpStockApp.CustomElements;
using System.ComponentModel.DataAnnotations;

namespace AcmeCorpStockApp.Dtos
{
    public class ProductDTO
    {
        [Required, MaxLength(10, ErrorMessage = "maximum 10 letters only")]
        public string Id { get; set; }

        [Required, MaxLength(100, ErrorMessage = "maximum 100 letters only")]
        public string Name { get; set; }
        [Required]
        [MaxLength(5, ErrorMessage = "maximum 5 letters only")]
        public string Quantity { get; set; }
        [Required]
        [MaxFloatLength(MaxLength = 5, ErrorMessage = "maximum 5 letters only")]
        public float? Price { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "maximum 10 letters only")]
        public string Manufactured { get; set; }
    }
}

