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
        public int addPatient(Patient patient);
        public Patient findPatientById(int id);
        public int deletePatientById(int id);
    }
}
