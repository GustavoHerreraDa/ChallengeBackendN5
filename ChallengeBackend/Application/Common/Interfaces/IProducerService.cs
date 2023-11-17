using Challenge.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Interfaces
{
    public interface IProducerService
    {
        Task SendEventAsync(PermisionEvent PermissionEvent);
    }
}
