using System;
using System.Threading.Tasks;
using MSNexus.Model;

namespace MSNexus.Repository
{
    public class CharactersRepository : ICharactersAsyncRepository
    {
        private readonly DAL.Character _contextDB;

        public CharactersRepository(DAL.Character contextDB)
        {
            _contextDB = contextDB;
        }

        public async Task<Characters> FindAsync(Guid? id)
        {
            return await _contextDB.Characters.FindAsync(id);
        }

        public async Task<Characters> AddAsync(Characters data)
        {
            await _contextDB.Characters.AddAsync(data);
            await _contextDB.SaveChangesAsync();
            return data;
        }

        public async Task Update(Guid id, Characters data)
        {
            data.ID = id;
            _contextDB.Update(data);
            await _contextDB.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Characters data = new Characters { ID = id };
            _contextDB.Characters.Attach(data);
            _contextDB.Characters.Remove(data);
            await _contextDB.SaveChangesAsync();
        }
    }
}
