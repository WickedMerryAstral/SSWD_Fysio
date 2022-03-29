using Core.Domain;
using System;
using System.Collections.Generic;

namespace Core.DomainServices
{
    public interface ITreatmentRepository
    {
        public int AddTreatment(Treatment treatment);
        public Treatment FindTreatmentById(int id);
        public List<Treatment> FindTreatmentsByTreatmentPlan(int planId);
        public List<Treatment> FindTreatmentsByPractitioner(int practitionerId);
        public List<Treatment> GetTreatmentsByPractitionerId(int practitionerId);
        public int GetTodaysTreatmentsCount(int practitionerId);
        public int DeleteTreatmentById(int id);
    }
}
