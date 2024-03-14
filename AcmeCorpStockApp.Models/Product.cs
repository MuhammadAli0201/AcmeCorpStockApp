using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeCorpStockApp.Models
{
    [Table("Product")]
    public class Product
    {
        [Column(TypeName="nvarchar(10)")]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "int")]
        public int Quantity { get; set; }

        [Column(TypeName = "float(5,2)")]
        public float Price { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Manufactured { get; set; }

        public Product()
        {
        }
    }

}
