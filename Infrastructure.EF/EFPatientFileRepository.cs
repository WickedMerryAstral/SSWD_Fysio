using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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
            context.SaveChanges();
        }

        public PatientFile FindPatientFileById(int id)
        {
            return context.patientFiles
                .Include(pf => pf.patient)
                .Include(pf => pf.treatmentPlan)
                .Where(pf => pf.patientFileId == id)
                .FirstOrDefault();
        }

        public List<PatientFile> FindPatientFilesByPractitionerId(int practitionerId)
        {
            return context.patientFiles.Where(pf => pf.treatmentPlan.practitionerId == practitionerId).ToList();
        }

        public List<PatientFile> GetPatientFiles()
        {
            return this.context.patientFiles
                .Include(pf => pf.patient)
                .Include(pf => pf.treatmentPlan)
                .ToList();
        }
        public Patient GetPatientFromFile(PatientFile patientFile)
        {
            return context.patients.Where(p => p.patientFileId == patientFile.patientFileId).FirstOrDefault();
        }

        public Patient GetPatientFromFileId(int id)
        {
            PatientFile p = FindPatientFileById(id);
            return p.patient;
        }

        public void UpdatePatientFile(PatientFile newFile)
        {
            // Seting IDs first
            PatientFile file = FindPatientFileById(newFile.patientFileId);
            newFile.patient.patientId = file.patient.patientId;
            newFile.treatmentPlan.treatmentPlanId = file.treatmentPlan.treatmentPlanId;

            // Info
            file.patient = newFile.patient;
            file.treatmentPlan = newFile.treatmentPlan;
            file.type = newFile.type;
            file.dischargeDate = newFile.dischargeDate;
            file.intakeByPractitionerId = newFile.intakeByPractitionerId;
            file.registerDate = newFile.registerDate;
            context.SaveChanges();
        }
    }
}
