using Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Common.Interfaces
{
    public interface IPermissionsRepository
    {
        void Insert(Permission entity);

        void Update(Permission entity);

        Task SaveChangeAsync();

        Task<Permission> GetByIdAsync(int id);

        Task<IEnumerable<Permission>> GetAllAsync();
    }
}
