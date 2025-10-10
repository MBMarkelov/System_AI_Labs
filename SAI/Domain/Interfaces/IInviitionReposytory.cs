using SAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.Interfaces
{
    internal interface IInviitionReposytory
    {
        Task<Invitation?> GetByIdAsync(Guid id);
        Task AddAsync(Invitation invitation);
        void Update(Invitation invitation);
    }
}
