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

        // Availability checks
        public bool availableMON { get; set; }
        public bool availableTUE { get; set; }
        public bool availableWED { get; set; }
        public bool availableTHU { get; set; }
        public bool availableFRI { get; set; }
        public bool availableSAT { get; set; }
        public bool availableSUN { get; set; }

        public RegisterPractitionerViewModel() {

        }
    }
}
