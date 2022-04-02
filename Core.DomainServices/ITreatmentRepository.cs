using Core.Domain;
using System;
using System.Collections.Generic;

namespace Core.DomainServices
{
    public interface ITreatmentRepository
    {
        public int AddTreatment(Treatment treatment);
        public Treatment FindTreatmentById(int id);
        public List<Treatment> GetTreatmentsByTreatmentPlanId(int planId);
        public List<Treatment> GetTreatmentsByPractitioner(int practitionerId);
        public List<Treatment> GetTreatmentsByPractitionerId(int practitionerId);
        public List<Treatment> GetTreatmentsByPatientId(int patientId);
        public int GetTodaysTreatmentsCount(int practitionerId);
        public void UpdateTreatment(Treatment treatment);
        public int DeleteTreatmentById(int id);
    }
}
