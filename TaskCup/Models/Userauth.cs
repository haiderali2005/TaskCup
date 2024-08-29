using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaskCup.Models
{
    public class Userauth
    {
        [Key]
        public int U_Id { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Enter a valid name")]
        [DisplayName("Name")]
        public string U_Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string U_Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string U_Password { get; set; }
        public ICollection<Tasks> Tasks { get; set;}
    }
}
