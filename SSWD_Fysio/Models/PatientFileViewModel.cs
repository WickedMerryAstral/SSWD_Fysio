using Core.Domain;
using System.Collections.Generic;

namespace SSWD_Fysio.Models
{
    public class PatientFileViewModel
    {
        public PatientFile patientFile { get; set; }
        public Patient patient { get; set; }
        public TreatmentPlan treatmentPlan { get; set; }
        public List<Treatment> treatments { get; set; }
        public List<Comment> comments { get; set; }
        public PractitionerBar practitionerBar { get; set; }

        public string type { get; set; }
        public string number { get; set; }
        public string sex { get; set; }

        public PatientFileViewModel(PatientFile patientFile) {
            this.patientFile = patientFile;
            this.patient = patientFile.patient;
            this.treatmentPlan = patientFile.treatmentPlan;
            this.treatments = patientFile.treatmentPlan.treatments;

            // Empty list error prevention
            if (patientFile.comments != null)
            {
                comments = patientFile.comments;
            }
            else {
                comments = new List<Comment>();
            }

            practitionerBar = new PractitionerBar();

            // Type
            switch (patient.type) {
                case PatientType.STUDENT:
                    type = "Student";
                    number = patient.studentNumber;
                    break;

                case PatientType.TEACHER:
                    type = "Teacher";
                    number = patient.employeeNumber;
                    break;
            }

            // Sex
            switch (patient.sex)
            {
                case Sex.MALE:
                    sex = "Male";
                    break;

                case Sex.FEMALE:
                    sex = "Female";
                    break;

                case Sex.OTHER:
                    sex = "Other";
                    break;
            }

        }
    }
}
