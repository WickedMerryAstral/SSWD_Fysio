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
            if (context.accounts.Where(ac => ac.mail == account.mail).Any()) {
                context.accounts.Add(account);
                context.SaveChanges();
                return account.accountId;
            }
            return -500;
        }

        public AppAccount FindAccountByMail(string email)
        {
            return context.accounts.Where(acc => acc.mail == email).FirstOrDefault();
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
