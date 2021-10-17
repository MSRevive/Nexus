using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSNexus.Repository
{
    public interface ICharactersAsyncRepository
    {
        Task<Model.Characters> AddAsync(Model.Characters data);
        Task<Model.Characters> FindAsync(Guid? id);
        Task Update(Guid? id, Model.Characters data);
    }
}
