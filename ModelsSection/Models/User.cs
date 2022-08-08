using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }
        [Required]
        [DisplayName("Birt date")]
        public DateTime Birth { get; set; }
        [Required]
        [DisplayName("Email")]
        public string? Email { get; set; }
        public string? HashPassword { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
