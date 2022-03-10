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
        public int treatmentId;
        public int practitionerId;
        public Practitioner practitioner;
        public int treatmentPlanId;
        public TreatmentPlan treatmentPlan;

        // Get type from Vektis treatment data
        public string type;

        // Info
        public DateTime treatmentDate;
        public string location;

    }
}
