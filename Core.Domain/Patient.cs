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
        public string patientId;
        public string patientFileId;
        public PatientFile patientFile;

        // Info
        public string studentNumber;
        public string employeeNumber;
        public string name;
        public string mail;
        public string phone;
        public string photoURL;
        public DateTime birthdate;
        public Sex sex;

        // Might not use this. Check again later.
        public PatientType type;
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
