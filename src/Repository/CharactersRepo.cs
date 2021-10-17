using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Update(Guid? id, Characters data)
        {
            var result = await _contextDB.Characters.FindAsync(id);
            if (result != null)
            {
                result = data;
                await _contextDB.SaveChangesAsync();
            }
        }
    }
}
