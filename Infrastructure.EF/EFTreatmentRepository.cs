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
        public int addTreatment(Treatment treatment)
        {
            throw new NotImplementedException();
        }

        public int deleteTreatmentById(int id)
        {
            throw new NotImplementedException();
        }

        public Treatment findTreatmentById(int id)
        {
            throw new NotImplementedException();
        }

        public Treatment[] findTreatmentsByPractitioner(int practitionerId)
        {
            throw new NotImplementedException();
        }

        public Treatment[] findTreatmentsByTreatmentPlan(int planId)
        {
            throw new NotImplementedException();
        }
    }
}
