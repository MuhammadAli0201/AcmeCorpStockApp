using System.ComponentModel.DataAnnotations;

namespace AcmeCorpStockApp.Dtos
{
    public class UserSignUpDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[@!$%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain atleast 1 capital letter and 1 number and one special character @!$%*?&")]
        [MinLength(8, ErrorMessage = "Password length must be 8")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password does not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserType { get; set; }

    }
}
