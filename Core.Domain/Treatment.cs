using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Treatment
    {
        // Identifiers
        [Key]
        public int treatmentId { get; set; }
        [Required]
        public int practitionerId { get; set; }
        [Required]
        public int treatmentPlanId { get; set; }

        // Get type from Vektis treatment data
        public string type { get; set; }

        // Info
        public DateTime treatmentDate { get; set; }
        public string location { get; set; }

        public Treatment(int practitionerId, int treatmentPlanId, string type, DateTime treatmentDate, string location, int patientId, string patientName)
        {
            this.practitionerId = practitionerId;
            this.treatmentPlanId = treatmentPlanId;

            this.type = type;
            this.treatmentDate = treatmentDate;
            this.location = location;
        }

        public Treatment()
        {
        }
    }
}
