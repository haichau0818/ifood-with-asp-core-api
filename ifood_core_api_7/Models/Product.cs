using System.ComponentModel.DataAnnotations;

namespace ifood_core_api_7.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]

        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }= 0;
        [StringLength(100)]

        public string Producer { get; set;}
        public DateTime ExpDate { get; set; }=DateTime.Now;


    }
}
