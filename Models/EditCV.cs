using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CVProject.Models
{
    public class EditCV
    {
        [Required]
        [StringLength(30, ErrorMessage = "Maximum length = {1}")]
        [Display(Name = "Your first name")]
        public string fname { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Maximum length = {1}")]
        [Display(Name = "your last name")]
        public string lname { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Non valid date")]
        public string Bdate { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public bool[] programing { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email must be of the form 'something@else.com'")]
        public string email { get; set; } = " ";
        [Required]
        public string vemail { get; set; } = " ";
        [Required]
        [Range(1, 20)]
        public double x { get; set; }
        [Required]
        [Range(20, 50)]
        public double y { get; set; }
        [Required]
        public double sum { get; set; }
    }
}