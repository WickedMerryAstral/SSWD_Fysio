using Core.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSWD_Fysio.Models
{
    public class RegisterPractitionerViewModel
    {
        // Account
        [Required]
        public string mail { get; set; }

        [Required]
        public string password { get; set; }

        // Practitioner
        public string name { get; set; }
        public string number { get; set; }


        // Teacher only
        // Add conditional validation later
        public string phone { get; set; }
        public string BIGnumber { get; set; }


        // List
        public List<string> typeOptions { get; set; }
        public string chosenType { get; set; }

        public RegisterPractitionerViewModel() {
            typeOptions = new List<string>();
            typeOptions.Add("Student");
            typeOptions.Add("Teacher");
        }
    }
}
