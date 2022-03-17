using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF
{
    public class FysioContextFactory : IDesignTimeDbContextFactory<FysioDBContext>
    {
        public FysioDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FysioDBContext>();
            optionsBuilder.UseSqlServer("Data Source=MAYHEM;Initial Catalog=FysioDB;Integrated Security=True");

            return new FysioDBContext(optionsBuilder.Options);
        }
    }
}
