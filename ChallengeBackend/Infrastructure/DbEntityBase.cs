using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Infrastructure.Persistence;

namespace Challenge.Infrastructure
{
    public class DbEntityBase : IDbEntityBase
    {
        protected ApplicationDbContext _context { get; set; }

        public DbEntityBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}