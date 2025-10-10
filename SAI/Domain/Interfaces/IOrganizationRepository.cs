using SAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.Interfaces
{
    internal interface IOrganizationRepository
    {
        Task<Organization> GetByIdAsync(Guid id);
        Task AddAsync(Organization organization);
        void Update(Organization organization);
        void Remove(Organization organization);
    }
}
