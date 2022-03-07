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
        public string treatmentId;
        public string practitionerId;

        // Get type from Vektis treatment data
        public string type;

        // Info
        public DateTime treatmentDate;
        public string location;

    }
}
