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
        public Practitioner GetPractitionerById(int id);
        public void DeletePractitionerById(int id);
    }
}
