using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Practitioner
    {
        // Identifiers
        public int practitionerId { get; set; }
        public TreatmentPlan[] treatmentPlans { get; set; }
        public Treatment[] treatments { get; set; }
        public PatientFile[] intakeByPatientFiles { get; set; }
        public PatientFile[] supervisedByPatientFiles { get; set; }

        // Info
        public PractitionerType type { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string studentNumber { get; set; }
        public string employeeNumber { get; set; }

        // Only teachers have a BIG number, and a phone number registered. No need to validate BIG numbers.
        public string phone { get; set; }
        public string BIGNumber { get; set; }
    }

    // Practitioners can be either students or teachers. Fill Student -or EmployeeNumber respectively.
    public enum PractitionerType {
        STUDENT,
        TEACHER
    }
}
