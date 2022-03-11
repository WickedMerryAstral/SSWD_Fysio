using Core.Domain;
using System;

namespace Core.DomainServices
{
    public interface ITreatmentRepository
    {
        public int addTreatment(Treatment treatment);
        public Treatment findTreatmentById(int id);
        public Treatment[] findTreatmentsByTreatmentPlan(int planId);
        public Treatment[] findTreatmentsByPractitioner(int practitionerId);
        public int deleteTreatmentById(int id);
    }
}
