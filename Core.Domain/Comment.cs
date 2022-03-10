using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Comment
    {
        // Identifiers
        public int commentId { get; set; }
        // Author is always a practitioner.
        public int practitionerId { get; set; }
        public Practitioner practitioner { get; set; }
        public int patientFileId { get; set; }
        public PatientFile patientFile { get; set; }

        // Info
        public DateTime postDate { get; set; }
        public bool visible { get; set; }
        public string content { get; set; }
    }
}
