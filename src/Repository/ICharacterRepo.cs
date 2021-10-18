using System;
using System.Threading.Tasks;

namespace MSNexus.Repository
{
    public interface ICharactersAsyncRepository
    {
        Task<Model.Characters> AddAsync(Model.Characters data);
        Task<Model.Characters> FindAsync(Guid? id);
        Task Update(Guid id, Model.Characters data);
        Task Delete(Guid id);
    }
}
