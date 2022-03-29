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
        public int AddPatientFile(PatientFile patientFile);
        public PatientFile FindPatientFileById(int id);
        public List<PatientFile> FindPatientFilesByPractitionerId(int practitionerId);
        public Patient GetPatientFromFile(PatientFile patientFile);
        public Patient GetPatientFromFileId(int id);
        public void DeletePatientFile(int id);
        public List<PatientFile> GetPatientFiles();

    }
}
