using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class TreatmentPlan
    {
        // Identifiers
        [Key]
        public int treatmentPlanId { get; set; }
        public int practitionerId { get; set; }
        public int patientFileId { get; set; }
        public List<Treatment> treatments { get; set; }

        // Info
        public string diagnosis { get; set; }
        public string complaint { get; set; }
        public int weeklySessions { get; set; }
        // Duration is measured in minutes.
        public int sessionDuration { get; set; }

        public TreatmentPlan(int practitionerId, string diagnosis, string complaint, int weeklySessions, int sessionDuration)
        {
            this.practitionerId = practitionerId;
            this.diagnosis = diagnosis;
            this.complaint = complaint;
            this.weeklySessions = weeklySessions;
            this.sessionDuration = sessionDuration;
        }

        public TreatmentPlan()
        {
            this.treatments = new List<Treatment>();
        }
    }
}
