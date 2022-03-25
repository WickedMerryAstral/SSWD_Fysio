using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface IPractitionerRepository
    {
        public int AddPractitioner(Practitioner practitioner);
        public List<Practitioner> getAllPractitioners();
        public List<Practitioner> getAllSupervisors();
        public Practitioner GetPractitionerById(int id);
        public Practitioner GetPractitionerByEmail(string email);
        public void DeletePractitionerById(int id);
    }
}
