using Core.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSWD_Fysio.Models
{
    public class RegisterPractitionerViewModel
    {
        // Account
        [Required]
        [EmailAddress]
        public string mail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        // Practitioner
        [Required]
        public string name { get; set; }

        [Required]
        public string number { get; set; }
        public string chosenType { get; set; }

        // Teacher only
        // Add conditional validation later
        public string phone { get; set; }
        public string BIGnumber { get; set; }

        public RegisterPractitionerViewModel() {

        }
    }
}
