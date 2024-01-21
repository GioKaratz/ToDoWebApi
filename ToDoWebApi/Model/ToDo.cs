using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoWebApi.Model
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Completed { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        IdentityUser User { get; set; }
    }
}
