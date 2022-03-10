using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PatientFile
    {
        // Identifiers
        public int patientFileId { get; set; }
        public int patientId { get; set; }
        public Patient patient { get; set; }
        public int treatmentPlanId { get; set; }
        public TreatmentPlan treatmentPlan { get; set; }
        public int intakeByPractitionerId { get; set; }
        public Practitioner intakeByPractitioner { get; set; }
        public int supervisedByPractitionerId { get; set; }
        public Practitioner supervisedByPractitioner { get; set; }
        public Comment[] comments { get; set; }

        // Info
        public int age { get; set; }
        public PatientFileType type { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime dischargeDate { get; set; }
    }

    public enum PatientFileType {
        STUDENT,
        EMPLOYEE
    }
}
