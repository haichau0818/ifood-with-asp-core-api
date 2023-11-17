using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ifood_core_api_7.Models
{
    [Table("User")]
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Fullname { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
    }
}
