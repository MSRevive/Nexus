using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MSNexus.DAL;
using MSNexus.Model;
using MSNexus.Repository;

namespace MSNexus.Controllers
{
    [Route("api/character")]
    public class CharController : ControllerBase
    {
        private readonly ILogger _logger;
        private Character _context;
        private readonly ICharactersAsyncRepository _contextChars;

        public CharController(ILogger<CharController> logger, Character context, ICharactersAsyncRepository contextChars)
        {
            _logger = logger;
            _context = context;
            _contextChars = contextChars;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Characters>> GetAllCharacters()
        {
            return await _context.Characters.ToListAsync();
        }

        // GET api/<controller>/{steamid}
        [HttpGet("{steamid}")]
        public async Task<IEnumerable<Characters>> GetCharacters(string steamid)
        {
            return await _context.Characters.Where(c => c.SteamID.Contains(steamid)).ToListAsync();
        }

        // GET api/<controller>/{steamid}/{slot}
        [HttpGet("{steamid}/{slot}")]
        public async Task<IEnumerable<Characters>> GetCharacter(string steamid, short slot)
        {
            return await _context.Characters.Where(c => c.SteamID.Contains(steamid) && c.Slot.Equals(slot)).ToListAsync();
        }

        // GET api/<controller>/id/{id}
        [HttpGet("id/{id}")]
        public async Task<Characters> GetCharacterByID(Guid id)
        {
            //return await _context.Characters.Where(c => c.ID == id).ToListAsync();
            return await _contextChars.FindAsync(id);
        }

        // POST api/<controller>
        // Create a new character in the record.
        [HttpPost]
        public async Task<ActionResult<Characters>> PostCharacter([FromBody] Characters charData)
        {
            _context.Characters.Add(charData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCharacterByID), new { ID = charData.ID }, charData);
        }

        // PUT api/<controller>/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Characters>> PutCharacter([FromBody] Characters charData, Guid id)
        {
            await _contextChars.Update(id, charData);
            return CreatedAtAction(nameof(GetCharacterByID), new { ID = id }, charData);
        }

        // DELETE api/<controller>/{id}
        [HttpDelete("{id}")]
        public async Task DeleteCharater(Guid id)
        {
            await _contextChars.Delete(id);
        }
    }
}
