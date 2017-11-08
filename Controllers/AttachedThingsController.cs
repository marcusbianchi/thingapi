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
    [Route("api/thinggroups/attachedthings/")]
    public class AttachedThingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttachedThingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{thingGroupId}")]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get(int thingGroupId)
        {
            var thingGroup = await _context.ThingGroups.Where(x => x.thingGroupId == thingGroupId).FirstOrDefaultAsync();
            if (thingGroup != null)
            {
                var things = await _context.Things.Where(x => thingGroup.thingsIds.Contains(x.thingId)).ToListAsync();
                return Ok(things);
            }
            return NotFound();
        }

        [HttpPost("{thingGroupId}")]
        public async Task<IActionResult> Post(int thingGroupId, [FromBody]Thing thing)
        {
            var thingGroup = await _context.ThingGroups.Where(x => x.thingGroupId == thingGroupId).FirstOrDefaultAsync();
            var childrenThing = await _context.Things.Where(x => x.thingId == thing.thingId).FirstOrDefaultAsync();

            if (thingGroup != null && childrenThing != null)
            {
                if (thingGroup.thingsIds == null)
                    thingGroup.thingsIds = new int[0];

                if (thingGroup.thingsIds.Contains(thing.thingId))
                    ModelState.AddModelError("ChildrenError", "This thing is already in this group");

                if (ModelState.IsValid)
                {
                    thingGroup.thingsIds = thingGroup.thingsIds.Append(thing.thingId).ToArray();

                    await _context.SaveChangesAsync();
                    return Created($"api/thinggroups/{thingGroup.thingGroupId}", thingGroup);
                }
                return BadRequest(ModelState);
            }
            return NotFound();
        }

        [HttpDelete("{thingGroupId}")]
        public async Task<IActionResult> Delete(int thingGroupId, [FromBody]Thing thing)
        {
            var thingGroup = await _context.ThingGroups.Where(x => x.thingGroupId == thingGroupId).FirstOrDefaultAsync();
            var childrenThing = await _context.Things.Where(x => x.thingId == thing.thingId).FirstOrDefaultAsync();

            if (thingGroup != null && childrenThing != null)
            {
                if (!thingGroup.thingsIds.Contains(thing.thingId))
                    ModelState.AddModelError("ChildrenError", "This thing is not in this group");

                if (ModelState.IsValid)
                {
                    thingGroup.thingsIds = thingGroup.thingsIds.Where(val => val != thing.thingId).ToArray();

                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                return BadRequest(ModelState);

            }
            return NotFound();
        }
    }

}