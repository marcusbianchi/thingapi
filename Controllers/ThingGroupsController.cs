using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thingservice.Data;
using thingservice.Model;

namespace thingservice.Controllers
{
    [Route("api/[controller]")]
    public class ThingGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThingGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get([FromQuery]int startat, [FromQuery]int quantity)
        {

            if (quantity == 0)
                quantity = 50;
            var groups = await _context.ThingGroups
            .Include(x => x.Tags)
            .Include(x => x.toolGroupAssociates)
            .Where(x => x.enabled == true)
            .OrderBy(x => x.thingGroupId)
            .Skip(startat).Take(quantity)
            .ToListAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get(int id)
        {
            var group = await _context.ThingGroups
            .Include(x => x.Tags)
            .Include(x => x.toolGroupAssociates)
            .OrderBy(x => x.thingGroupId)
            .Where(x => x.thingGroupId == id)
            .FirstOrDefaultAsync();
            if (group == null)
                return NotFound();
            return Ok(group);
        }

          [HttpGet("getgroups/{thingid}")]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> GetGroups(int thingid)
        {
            var group = await _context.ThingGroups
            .Include(x => x.Tags)
            .Include(x => x.toolGroupAssociates)
            .OrderBy(x => x.thingGroupId)
            .Where(x => x.thingsIds.Contains(thingid))
            .ToListAsync();
            if (group == null)
                return NotFound();
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ThingGroup thingGroup)
        {
            thingGroup.thingGroupId = 0;
            thingGroup.thingsIds = new int[0];
            if (ModelState.IsValid)
            {
                await _context.AddAsync(thingGroup);
                await _context.SaveChangesAsync();

                return Created($"api/thinggroups/{thingGroup.thingGroupId}", thingGroup);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ThingGroup thingGroup)
        {
            if (ModelState.IsValid)
            {
                var curThing = await _context.ThingGroups
                .AsNoTracking()
                .Where(x => x.thingGroupId == id)
                .FirstOrDefaultAsync();
                thingGroup.thingsIds = curThing.thingsIds;
                if (id != thingGroup.thingGroupId)
                {
                    return NotFound();
                }
                _context.ThingGroups.Update(thingGroup);
                await _context.SaveChangesAsync();
                return NoContent();

            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var curThing = await _context.ThingGroups
            .Where(x => x.thingGroupId == id)
            .FirstOrDefaultAsync();
            if (curThing != null)
            {
                curThing.enabled = false;
                _context.Entry(curThing).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("list/")]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> GetList([FromQuery]int[] thingGroupId)
        {
            var things = await _context.ThingGroups
            .Where(x => thingGroupId.Contains(x.thingGroupId))
            .ToListAsync();
            return Ok(things);
        }
    }

}