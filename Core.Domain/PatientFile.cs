using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PatientFile
    {
        // Identifiers
        [Key]
        public int patientFileId { get; set; }
        // public int patientId { get; set; }
        public Patient patient { get; set; }
        // public int treatmentPlanId { get; set; }
        public TreatmentPlan treatmentPlan { get; set; }
        [Required]
        public int intakeByPractitionerId { get; set; }
        public int supervisedBypractitionerId { get; set; }
        public List<Comment> comments { get; set; }

        // Info
        [Required]
        public DateTime birthDate { get; set; }
        public PatientFileType type { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime dischargeDate { get; set; }

        public PatientFile(int intakeByPractitioner, int supervisedByPractitioner, DateTime birthDate, PatientFileType type, DateTime registerDate, DateTime dischargeDate)
        {
            // Initializing a new patient and treatmentplan, as they are created during intake.
            // TreatmentPlan internally create new treatment lists when calling the default constructor.
            this.patient = new Patient();
            this.treatmentPlan = new TreatmentPlan();

            // Collection
            this.comments = new List<Comment>();

            this.intakeByPractitionerId = intakeByPractitioner;
            this.supervisedBypractitionerId = supervisedByPractitioner;
            this.birthDate = birthDate;
            this.type = type;
            this.registerDate = registerDate;
            this.dischargeDate = dischargeDate;
        }

        public PatientFile()
        {
            // Initializing
            treatmentPlan = new TreatmentPlan();
            patient = new Patient();
        }
    }

    public enum PatientFileType {
        STUDENT,
        EMPLOYEE
    }
}
