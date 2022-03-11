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
        public int addPractitioner(Practitioner practitioner);
        public Practitioner getPractitionerById(int id);
        public int deletePractitionerById(int id);
    }
}
