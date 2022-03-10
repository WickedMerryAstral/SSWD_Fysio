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
        public int commentId;
        // Author is always a practitioner.
        public int practitionerId;
        public Practitioner practitioner;
        public int patientFileId;
        public PatientFile patientFile;

        // Info
        public DateTime postDate;
        public bool visible;
        public string content;
    }
}
