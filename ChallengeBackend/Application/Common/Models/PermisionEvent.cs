using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Common.Models
{
    public class PermisionEvent
    {
        public Guid Id { get; set; }
        public string OperationName { get; set; }
    }
}
