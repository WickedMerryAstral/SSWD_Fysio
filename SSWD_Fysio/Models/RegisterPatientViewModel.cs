using Core.Domain;
using Infrastructure.EF;
using System.ComponentModel.DataAnnotations;

namespace SSWD_Fysio.Models
{
    public class RegisterPatientViewModel
    { 
        [Required]
        [EmailAddress]
        public string mail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(3)]
        public string password { get; set; }

        public RegisterPatientViewModel() {

        }
    }
}
