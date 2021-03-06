using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface IAppAccountRepository
    {
        public int AddAppAccount(AppAccount appaccount);
        public AppAccount FindAccountByMail(string email);
    }
}
