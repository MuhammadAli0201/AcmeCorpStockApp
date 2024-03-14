using System.ComponentModel.DataAnnotations;

namespace AcmeCorpStockApp.Dtos
{
    public class UserLoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
