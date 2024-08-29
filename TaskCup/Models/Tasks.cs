using System.ComponentModel.DataAnnotations;

namespace TaskCup.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; } 

        public DateTime? Deadline { get; set; } 

        [Required]
        [StringLength(20)]
        public string Priority { get; set; } 

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "To Do"; 
        [Required]
        public  int  U_Id { get; set; }
        public Userauth userauth_ { get; set; }
    }
}
