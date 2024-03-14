using System.ComponentModel.DataAnnotations;

namespace AcmeCorpStockApp.Dtos
{
    public class UserChangePasswordDTO
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[@!$%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain atleast 1 capital letter and 1 number and one special character @!$%*?&")]
        [MinLength(8, ErrorMessage = "Length must be 8")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
