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
        public int patientFileId;
        public int patientId;
        public Patient patient;
        public int treatmentPlanId;
        public TreatmentPlan treatmentPlan;
        public int intakeByPractitionerId;
        public Practitioner intakeByPractitioner;
        public int supervisedByPractitionerId;
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
