using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class MobilesController : ControllerBase
    {
        private readonly WebapiContext _context;

        public MobilesController(WebapiContext context)
        {
            _context = context;
        }

        // GET: api/Mobiles
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mobiles>>> GetMobiles()
        {
            return await _context.Mobiles.ToListAsync();
        }

        // GET: api/Mobiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mobiles>> GetMobiles(int id)
        {
            var mobiles = await _context.Mobiles.FindAsync(id);

            if (mobiles == null)
            {
                return NotFound();
            }

            return mobiles;
        }

        // PUT: api/Mobiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMobiles(int id, Mobiles mobiles)
        {
            if (id != mobiles.ID)
            {
                return BadRequest();
            }

            _context.Entry(mobiles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MobilesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Mobiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mobiles>> PostMobiles(Mobiles mobiles)
        {
            _context.Mobiles.Add(mobiles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMobiles", new { id = mobiles.ID }, mobiles);
        }

        // DELETE: api/Mobiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMobiles(int id)
        {
            var mobiles = await _context.Mobiles.FindAsync(id);
            if (mobiles == null)
            {
                return NotFound();
            }

            _context.Mobiles.Remove(mobiles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MobilesExists(int id)
        {
            return _context.Mobiles.Any(e => e.ID == id);
        }

        [HttpPost("Id")]
        public async Task<ActionResult<IEnumerable<Mobiles>>> PostMethod(int Id , string Brand , string model)
        {
            var result = await _context.Database.ExecuteSqlRawAsync("EXEC uspInsertIntoMobiles @Id ={0} ,  @Brand={1} , @model = {2}", Id , Brand , model);

            return Ok(result);
           // string postmethod = "EXEC uspInsertIntoMobiles  " + "'" + Id + " ' , ' " + Brand + " ' , ' " + model + "'";
           // return await _context.Database.ExecuteSqlRawAsync(postmethod);

        }

        [HttpPut("Id")]
        public async Task<ActionResult<IEnumerable<Mobiles>>> PutMethod(int Id, string Brand, string model)
        {
            var result = await _context.Database.ExecuteSqlRawAsync("EXEC uspInsertIntoMobiles @Id ={0} ,  @Brand={1} , @model = {2}", Id, Brand, model);

            return Ok(result);
            // string postmethod = "EXEC uspInsertIntoMobiles  " + "'" + Id + " ' , ' " + Brand + " ' , ' " + model + "'";
            // return await _context.Database.ExecuteSqlRawAsync(postmethod);

        }
    }
}
