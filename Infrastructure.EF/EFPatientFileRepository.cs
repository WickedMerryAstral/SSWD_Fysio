using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure.EF
{
    public class EFPatientFileRepository : IPatientFileRepository
    {
        public int addPatientFile(PatientFile patientFile)
        {
            throw new NotImplementedException();
        }

        public int deletePatientFile(int id)
        {
            throw new NotImplementedException();
        }

        public PatientFile findPatientFileById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
