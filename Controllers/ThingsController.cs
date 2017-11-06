using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThingsAPI.Data;
using ThingsAPI.Model;

namespace ThingsAPI.Controllers
{
    [Route("api/[controller]")]
    public class ThingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get([FromQuery]int startat, [FromQuery]int quantity)
        {

            if (quantity == 0)
                quantity = 50;
            var things = await _context.Things.OrderBy(x => x.thingId).Skip(startat).Take(quantity).ToListAsync();
            return Ok(things);
        }

        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get(int id)
        {
            var thing = await _context.Things.OrderBy(x => x.thingId).Where(x => x.thingId == id).FirstOrDefaultAsync(); ;
            return Ok(thing);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Thing thing)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(thing);
                await _context.SaveChangesAsync();

                return Created($"api/things/{thing.thingId}", thing);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Thing thing)
        {
            if (ModelState.IsValid)
            {
                if (id != thing.thingId)
                {
                    return NotFound();
                }
                _context.Things.Update(thing);
                await _context.SaveChangesAsync();
                return NoContent();

            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var thing = await _context.Things.Where(x => x.thingId == id).FirstOrDefaultAsync();
            if (thing != null)
            {
                _context.Entry(thing).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
    }

}