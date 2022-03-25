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
        private FysioDBContext context;
        public EFPatientFileRepository(FysioDBContext db)
        {
            this.context = db;
        }

        public int AddPatientFile(PatientFile patientFile)
        {
            // Adding the new objects to the context.
            //context.treatmentPlans.Add(patientFile.treatmentPlan);
            //context.patients.Add(patientFile.patient);

            context.patientFiles.Add(patientFile);
            context.SaveChanges();

            return patientFile.patientFileId;
        }

        public void DeletePatientFile(int id)
        {
            context.patientFiles.Remove(FindPatientFileById(id));
        }

        public PatientFile FindPatientFileById(int id)
        {
            return context.patientFiles.Where(p => p.patientFileId == id).FirstOrDefault();
        }

        public List<PatientFile> GetPatientFiles()
        {
            return this.context.patientFiles.ToList();
        }
        public Patient GetPatientFromFile(PatientFile patientFile)
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientFromFileId(int id)
        {
            PatientFile p = FindPatientFileById(id);
            return p.patient;
        }
    }
}
