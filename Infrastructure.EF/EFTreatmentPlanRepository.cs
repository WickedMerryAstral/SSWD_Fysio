using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure.EF
{
    public class EFTreatmentPlanRepository : ITreatmentPlanRepository
    {
        private FysioDBContext context;
        public EFTreatmentPlanRepository(FysioDBContext db)
        {
            this.context = db;
        }
        public int AddTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            throw new NotImplementedException();
        }

        public int DeleteTreatmentPlanById(int id)
        {
            throw new NotImplementedException();
        }

        public TreatmentPlan FindTreatmentPlanById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
