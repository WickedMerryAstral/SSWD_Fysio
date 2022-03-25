using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Practitioner
    {
        // Identifiers
        [Key]
        public int practitionerId { get; set; }
        public List<Treatment> treatments { get; set; }

        // Info
        public PractitionerType type { get; set; }

        [Display]
        public string name { get; set; }

        [Required]
        public string mail { get; set; }
        public string studentNumber { get; set; }
        public string employeeNumber { get; set; }

        // Only teachers have a BIG number, and a phone number registered. No need to validate BIG numbers.
        public string phone { get; set; }
        public string BIGNumber { get; set; }

        public Practitioner(PractitionerType type, string name, string mail, string studentNumber, string employeeNumber, string phone, string BIGNumber)
        {
            // Collections
            this.treatments = new List<Treatment>();

            this.type = type;
            this.name = name;
            this.mail = mail;
            this.studentNumber = studentNumber;
            this.employeeNumber = employeeNumber;
            this.phone = phone;
            this.BIGNumber = BIGNumber;
        }

        public Practitioner()
        {
        }
    }

    // Practitioners can be either students or teachers. Fill Student -or EmployeeNumber respectively.
    public enum PractitionerType {
        STUDENT,
        TEACHER
    }
}
