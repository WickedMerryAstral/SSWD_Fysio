using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DomainServices
{
    public interface IPatientRepository
    {
        public int AddPatient(Patient patient);
        public Patient FindPatientById(int id);
        public int DeletePatientById(int id);
        public IEnumerable<Patient> GetPatients();
    }
}
