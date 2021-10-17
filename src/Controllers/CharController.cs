using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MSNexus.DAL;
using MSNexus.Model;

namespace MSNexus.Controllers
{
    [Route("api/character")]
    public class CharController : ControllerBase
    {
        private readonly ILogger _logger;
        private Character _context;

        public CharController(ILogger<CharController> logger, Character context)
        {
            _logger = logger;
            _context = context;
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

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Characters>> PostCharacter([FromBody] Characters charData)
        {
            _context.Characters.Add(charData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCharacters), new { SteamID = charData.SteamID }, charData);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
