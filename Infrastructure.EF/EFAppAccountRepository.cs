using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure.EF
{
    public class EFAppAccountRepository : IAppAccountRepository
    {
        private FysioDBContext context;
        public EFAppAccountRepository(FysioDBContext db)
        {
            this.context = db;
        }

        public int AddAppAccount(AppAccount account)
        {
            context.accounts.Add(account);
            context.SaveChanges();
            return account.accountId;
        }

        // System temp data types
        // Practitioners : 1
        // Patients : 2
        // None found: 400
        public int GetAccountType(string mail) {
            if (context.practitioners.Where(p => p.mail == mail).Any()){
                return 1;
            }
            if (context.patients.Where(p => p.mail == mail).Any()) {
                return 2;
            }
            return 400;
        }

        public bool isEmailAvailable(string mail) {
            if (context.accounts.Where(a => a.mail == mail).Any())
            {
                return false;
            }
            else {
                return true;
            }
        }
    }
}
