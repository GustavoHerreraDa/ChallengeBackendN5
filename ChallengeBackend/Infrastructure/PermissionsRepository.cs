using Challenge.Application.Common.Interfaces;
using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Infrastructure.Persistence;

namespace Challenge.Infrastructure
{
    public class PermissionsRepository : DbEntityBase, IPermissionsRepository
    {
        private readonly ILogger<PermissionsRepository> _logger;

        public PermissionsRepository(ApplicationDbContext context, ILogger<PermissionsRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            try
            {
                return await _context.Permission.ToListAsync();
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to get a record");
                throw;
            }
        }

        public async Task<Permission> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Permission.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to get by id a record");
                throw;
            }
        }

        public void Insert(Permission entity)
        {
            try
            {
                _context.Add(entity);
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to add a record");
                throw;
            }
        }

        public async Task SaveChangeAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to persist in the database");
                throw;
            }
        }

        public void Update(Permission entity)
        {
            try
            {
                _context.Attach(entity).State = EntityState.Modified;
                _context.Update(entity);
            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while trying to update a record");
                throw;
            }
        }
    }
}
