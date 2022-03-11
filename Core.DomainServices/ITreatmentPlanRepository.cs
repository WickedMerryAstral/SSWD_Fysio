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
        public int addTreatmentPlan(TreatmentPlan treatmentPlan);
        public TreatmentPlan findTreatmentPlanById(int id);
        public int deleteTreatmentPlanById(int id);
    }
}
