using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DomainServices
{
    public interface IPatientFileRepository
    {
        public int addPatientFile(PatientFile patientFile);
        public PatientFile findPatientFileById(int id);
        public int deletePatientFile(int id);
    }
}
