using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class TreatmentPlan
    {
        // Identifiers
        public int treatmentPlanId { get; set; }
        public int patientId { get; set; }
        public Patient patient { get; set; }
        public int practitionerId { get; set; }
        public Practitioner practitioner { get; set; }
        public int patientFileId { get; set; }
        public PatientFile pratientFile { get; set; }
        public Treatment[] treatments { get; set; }

        // Info
        public string diagnosis { get; set; }
        public string complaint { get; set; }
        public int weeklySessions { get; set; }
        // Duration is measured in minutes.
        public int sessionDuration { get; set; }
    }
}
