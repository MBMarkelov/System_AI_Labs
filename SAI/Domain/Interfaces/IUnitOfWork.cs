using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<Guid> SaveChangesAsync();
    }
}
