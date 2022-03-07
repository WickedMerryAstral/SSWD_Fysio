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
        public string treatmentPlanId;
        public string patientId;
        public string practitionerId;
        public string patientFileId;
        public string[] treatmentIds;

        // Info
        public string diagnosis;
        public string complaint;
        public int weeklySessions;
        // Duration is measured in minutes.
        public int sessionDuration;
    }
}
