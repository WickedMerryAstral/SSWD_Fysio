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

        public PatientFileViewModel(PatientFile patientFile) {
            patient = patientFile.patient;
            treatmentPlan = patientFile.treatmentPlan;
            treatments = patientFile.treatmentPlan.treatments;
        }
    }
}
