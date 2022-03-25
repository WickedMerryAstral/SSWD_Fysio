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
            throw new NotImplementedException();
        }

        public int DeleteTreatmentById(int id)
        {
            throw new NotImplementedException();
        }

        public Treatment FindTreatmentById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Treatment> FindTreatmentsByPractitioner(int practitionerId)
        {
            throw new NotImplementedException();
        }

        public List<Treatment> FindTreatmentsByTreatmentPlan(int planId)
        {
            throw new NotImplementedException();
        }
    }
}
