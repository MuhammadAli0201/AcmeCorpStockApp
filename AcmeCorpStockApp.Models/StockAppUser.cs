using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeCorpStockApp.Models
{
    [Table("StockAppUser")]
    public class StockAppUser
    {
        [Key]
        [Column(TypeName = "nvarchar(30)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Type { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string RegistrarEmail { get; set; }

        //Deep copy constructor
        public StockAppUser(StockAppUser oldUser)
        {
            this.Email = oldUser.Email;
            this.Name = oldUser.Name;
            this.Password = oldUser.Password;
            this.Type = oldUser.Type;
            this.RegistrarEmail = oldUser.RegistrarEmail;
        }

        public StockAppUser()
        {
        }
    }
}
