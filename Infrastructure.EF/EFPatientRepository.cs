using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure.EF
{
    public class EFPatientRepository : IPatientRepository
    {
        public int addPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public int deletePatientById(int id)
        {
            throw new NotImplementedException();
        }

        public Patient findPatientById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
