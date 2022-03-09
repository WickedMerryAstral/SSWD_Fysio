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
        public string patientFileId;
        public string patientId;
        public Patient patient;
        public string treatmentPlanId;
        public TreatmentPlan treatmentPlan;
        public string intakeByPractitionerId;
        public Practitioner intakeByPractitioner;
        public string supervisedByPractitionerId;
        public Practitioner supervisedByPractitioner;
        public string[] commentIds;
        public Comment[] comments;

        // Info
        public int age;
        public PatientFileType type;
        public DateTime registerDate;
        public DateTime dischargeDate;
    }

    public enum PatientFileType {
        STUDENT,
        EMPLOYEE
    }
}
