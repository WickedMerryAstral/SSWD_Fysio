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
            context.patients.Add(patient);
            context.SaveChanges();
            return patient.patientId;
        }

        public int DeletePatientById(int id)
        {
            throw new NotImplementedException();
        }

        public Patient FindPatientById(int id)
        {
            return context.patients.Where(p => p.patientId == id).FirstOrDefault();
        }

        public Patient FindPatientByMail(string mail)
        {
            return context.patients.Where(p => p.mail == mail).FirstOrDefault();
        }

        public List<Patient> GetPatients()
        {
            throw new NotImplementedException();
        }
    }
}
