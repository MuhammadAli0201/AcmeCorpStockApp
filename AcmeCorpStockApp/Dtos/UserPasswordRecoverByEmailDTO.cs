using System.ComponentModel.DataAnnotations;

namespace AcmeCorpStockApp.Dtos
{
    public class UserPasswordRecoverByEmailDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
