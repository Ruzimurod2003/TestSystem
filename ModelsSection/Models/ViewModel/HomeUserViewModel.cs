using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class HomeUserViewModel
    {
        public Guid UserId { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string? FullName { get; set; }
        [Required]
        [DisplayName("Birt date")]
        public DateTime Birth { get; set; }
        [Required]
        [DisplayName("Email")]
        public string? Email { get; set; }
        public string? RoleString { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
