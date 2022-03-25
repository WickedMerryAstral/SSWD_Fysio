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
        private FysioDBContext context;
        public EFPatientRepository(FysioDBContext db)
        {
            this.context = db;
        }

        public int AddPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public int DeletePatientById(int id)
        {
            throw new NotImplementedException();
        }

        public Patient FindPatientById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Patient> GetPatients()
        {
            throw new NotImplementedException();
        }
    }
}
