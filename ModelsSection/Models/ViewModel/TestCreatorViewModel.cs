using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class TestCreatorViewModel
    {
        [Required]
        public Guid ProblemId { get; set; }
        [Required]
        [DisplayName("Problem Description")]
        public string? ProblemDescription { get; set; }
        [Required]
        public string? ProblemAnsver { get; set; }
        [Required]
        public string? FirstSolution { get; set; }
        [Required]
        public string? SecondSolution { get; set; }
        [Required]
        public string? ThirdSolution { get; set; }
        [Required]
        public string? FourthSolution { get; set; }
        [Required]
        public string? Solution { get; set; }
        [Required]
        public string? DepartmentName { get; set; }
        [Required]
        public List<string>? DepartmentNames { get; set; }
    }
}
