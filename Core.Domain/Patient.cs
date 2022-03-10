using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Patient
    {
        // Identifiers
        public int patientId { get; set; }
        public int patientFileId { get; set; }
        public PatientFile patientFile { get; set; }

        // Info
        public string studentNumber { get; set; }
        public string employeeNumber { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string photoURL { get; set; }
        public DateTime birthdate { get; set; }
        public Sex sex { get; set; }

        // Might not use this. Check again later.
        public PatientType type { get; set; }
    }

    // Patients can be either students or employees. Fill Student -or EmployeeNumber respectively.
    public enum PatientType {
        STUDENT,
        TEACHER
    } 

    public enum Sex {
        MALE,
        FEMALE,
        OTHER
    }
}
