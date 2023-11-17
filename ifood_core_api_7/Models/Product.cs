using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ifood_core_api_7.Models
{
    [Table("Product")]
    public class Product
    {
        [Required]

        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]

        public string Description { get; set; }
        [Required]
        public double Price { get; set; }= 0;
        [StringLength(100)]

        public string Producer { get; set;}
        public DateTime ExpDate { get; set; }=DateTime.Now;


    }
}
