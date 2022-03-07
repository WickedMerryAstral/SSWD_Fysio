using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PatientFile
    {
        // Identifiers
        public string patientFileId;
        public string patientId;
        public string planId;
        public string intakeByPractitionerId;
        public string supervisedByPractitionerId;
        public string[] commentIds;

        // Info
        public int age;
        public PatientFileType type;
        public DateTime registerDate;
        public DateTime dischargeDate;
    }

    public enum PatientFileType {
        STUDENT,
        EMPLOYEE
    }
}
