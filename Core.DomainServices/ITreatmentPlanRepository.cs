using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface ITreatmentPlanRepository
    {
        public int AddTreatmentPlan(TreatmentPlan treatmentPlan);
        public TreatmentPlan FindTreatmentPlanById(int id);
        public int DeleteTreatmentPlanById(int id);
        public bool HasReachedWeeklyLimit(DateTime date, int treatmentPlanId);
    }
}
