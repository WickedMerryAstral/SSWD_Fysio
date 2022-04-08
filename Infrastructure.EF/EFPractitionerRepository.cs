using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure.EF
{
    public class EFPractitionerRepository : IPractitionerRepository
    {
        private FysioDBContext context;
        public EFPractitionerRepository(FysioDBContext db)
        {
            this.context = db;
        }

        public int AddPractitioner(Practitioner practitioner)
        {
            context.practitioners.Add(practitioner);
            context.SaveChanges();
            return practitioner.practitionerId;
        }

        public void DeletePractitionerById(int id)
        {
            context.Remove(GetPractitionerById(id));
        }

        public List<Practitioner> getAllPractitioners()
        {
            return context.practitioners.ToList();
        }

        public List<Practitioner> getAllSupervisors()
        {
            return context.practitioners.Where(p => p.type == PractitionerType.TEACHER).ToList();
        }

        public Practitioner GetPractitionerById(int id)
        {
            return context.practitioners.Where(p => p.practitionerId == id).FirstOrDefault();
        }

        public Practitioner GetPractitionerByEmail(string mail) {
            return context.practitioners.Where(p => p.mail == mail).First();
        }
    }
}
