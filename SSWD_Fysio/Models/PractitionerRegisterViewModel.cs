using Core.Domain;
using System.Collections.Generic;

namespace SSWD_Fysio.Models
{
    public class PractitionerRegisterViewModel
    {
        // Account
        public string mail { get; set; }
        public string password { get; set; }

        // Practitioner
        public string name { get; set; }
        public string number { get; set; }


        // Teacher only
        public string phone { get; set; }
        public string BIGnumber { get; set; }


        // List
        public List<string> typeOptions { get; set; }
        public string chosenType { get; set; }


        public PractitionerRegisterViewModel() {
            typeOptions = new List<string>();
            typeOptions.Add("Student");
            typeOptions.Add("Teacher");
        }
    }
}
