using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Treatment
    {
        // Identifiers
        public int treatmentId { get; set; }
        public int practitionerId { get; set; }
        public Practitioner practitioner { get; set; }
        public int treatmentPlanId { get; set; }
        public TreatmentPlan treatmentPlan { get; set; }

        // Get type from Vektis treatment data
        public string type { get; set; }

        // Info
        public DateTime treatmentDate { get; set; }
        public string location { get; set; }

    }
}
