using Core.Domain;
using System;
using System.Collections.Generic;

namespace Core.DomainServices
{
    public interface ITreatmentRepository
    {
        public int AddTreatment(Treatment treatment);
        public Treatment FindTreatmentById(int id);
        public IEnumerable<Treatment> FindTreatmentsByTreatmentPlan(int planId);
        public IEnumerable<Treatment> FindTreatmentsByPractitioner(int practitionerId);
        public int DeleteTreatmentById(int id);
    }
}
