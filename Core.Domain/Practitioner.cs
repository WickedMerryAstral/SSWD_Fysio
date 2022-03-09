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
        public string practitionerId;
        public string[] treatmentPlanIds;
        public TreatmentPlan[] treatmentPlans;
        public string[] treatmentIds;
        public Treatment[] treatments;

        // Info
        public PractitionerType type;
        public string name;
        public string mail;
        public string studentNumber;
        public string employeeNumber;

        // Only teachers have a BIG number, and a phone number registered. No need to validate BIG numbers.
        public string phone;
        public string BIGNumber;
    }

    // Practitioners can be either students or teachers. Fill Student -or EmployeeNumber respectively.
    public enum PractitionerType {
        STUDENT,
        TEACHER
    }
}
