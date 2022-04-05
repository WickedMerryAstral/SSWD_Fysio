using Core.Domain;
using System;
using System.Collections.Generic;

namespace SSWD_Fysio.Models
{
    public class PractitionerDashboardViewModel
    {
        public List<PatientFile> practitionerFiles { get; set; }
        public List<PatientFile> allFiles { get; set; }
        public PractitionerBar practitionerBar { get; set; }
        public List<Treatment> treatments { get; set; }
        public List<PatientTreatment> patientTreatments { get; set; }

        public PractitionerDashboardViewModel() {
            practitionerFiles = new List<PatientFile>();
            allFiles = new List<PatientFile>();
            practitionerBar = new PractitionerBar();
            treatments = new List<Treatment>();

            patientTreatments = new List<PatientTreatment>();
        }

        public class PatientTreatment
        {
            public int treatmentId { get; set; }
            public int patientId { get; set; }
            public string patientName { get; set; }
            public DateTime treatmentDate { get; set; }
            public string treatmentCode { get; set; }
        }
    }
}
