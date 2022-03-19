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
    }
}
