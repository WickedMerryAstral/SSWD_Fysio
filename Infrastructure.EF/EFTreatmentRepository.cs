using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure.EF
{
    public class EFTreatmentRepository : ITreatmentRepository
    {
        private FysioDBContext context;
        public EFTreatmentRepository(FysioDBContext db)
        {
            this.context = db;
        }

        public int AddTreatment(Treatment treatment)
        {
            context.treatments.Add(treatment);
            context.SaveChanges();
            return treatment.treatmentId;
        }

        public int DeleteTreatmentById(int id)
        {
            this.context.Remove(FindTreatmentById(id));
            this.context.SaveChanges();
            return id;
        }

        public Treatment FindTreatmentById(int id)
        {
            return this.context.treatments.Where(t => t.treatmentId == id).FirstOrDefault();
            
        }

        public List<Treatment> GetTreatmentsByPractitioner(int practitionerId)
        {
            throw new NotImplementedException();
        }

        public List<Treatment> GetTreatmentsByTreatmentPlanId(int planId)
        {
            return this.context.treatments.Where(t => t.treatmentPlanId == planId).ToList();
        }

        public int GetTodaysTreatmentsCount(int practitionerId)
        {
            return context.treatments.Where(t =>
            t.practitionerId == practitionerId
            && t.treatmentDate == DateTime.Today).Count();
        }

        public List<Treatment> GetTreatmentsByPatientId(int patientId)
        {
            throw new NotImplementedException();
        }

        public List<Treatment> GetTreatmentsByPractitionerId(int practitionerId)
        {
            return context.treatments.Where(t => t.practitionerId == practitionerId).ToList();
        }

        public void UpdateTreatment(Treatment treatment)
        {
            Treatment t = FindTreatmentById(treatment.treatmentId);
            t.location = treatment.location;
            t.type = treatment.type; 
            t.description = treatment.description;
            t.practitionerId = treatment.practitionerId;
            t.hasMandatoryExplanation = treatment.hasMandatoryExplanation;
            context.SaveChanges();
        }
    }
}
