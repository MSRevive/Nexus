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
        public async Task<IEnumerable<Characters>> GetCharacters()
        {
            return await _context.Characters.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{steamid}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Characters>> PostCharacter([FromBody] Characters charData)
        {
            Console.WriteLine(charData);
            _context.Characters.Add(charData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCharacters), new { ID = Guid.NewGuid() }, charData);
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
