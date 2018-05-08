using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using securityfilter;
using thingservice.Data;
using thingservice.Model;

namespace thingservice.Controllers {
    [Route ("api/things/childrenthings/")]
    public class ChildrenThingsController : Controller {
        private readonly ApplicationDbContext _context;

        public ChildrenThingsController (ApplicationDbContext context) {
            _context = context;
        }

        // GET api/values
        [HttpGet ("{parentId}")]
        [SecurityFilter ("things__allow_read")]
        [ResponseCache (CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get (int parentId) {
            var parentThing = await _context.Things.Where (x => x.thingId == parentId).FirstOrDefaultAsync ();
            if (parentThing != null) {
                var things = await _context.Things.Where (x => parentThing.childrenThingsIds.Contains (x.thingId)).ToListAsync ();
                return Ok (things);
            }
            return NotFound ();
        }

        // POST api/values
        [HttpPost ("{parentId}")]
        [SecurityFilter ("things__allow_update")]
        public async Task<IActionResult> Post (int parentId, [FromBody] Thing thing) {

            if (thing.thingId == parentId)
                ModelState.AddModelError ("ChildrenError", "The Children can´t be parent of itself.");
            if (ModelState.IsValid) {
                var parentThing = await _context.Things.Where (x => x.thingId == parentId).FirstOrDefaultAsync ();
                var childrenThing = await _context.Things.Where (x => x.thingId == thing.thingId).FirstOrDefaultAsync ();

                if (parentThing != null && childrenThing != null) {
                    if (parentThing.childrenThingsIds == null)
                        parentThing.childrenThingsIds = new int[0];

                    if (parentThing.childrenThingsIds.Contains (thing.thingId))
                        ModelState.AddModelError ("ChildrenError", "This thing is already in this parent");

                    if (childrenThing.parentThingId != null && childrenThing.parentThingId != 0) {
                        var oldParentThing = await _context.Things.Where (x => x.thingId == parentId).FirstOrDefaultAsync ();
                        if (oldParentThing != null) {
                            oldParentThing.childrenThingsIds = oldParentThing.childrenThingsIds.Where (val => val != thing.thingId).ToArray ();
                            _context.Things.Update (oldParentThing);
                        }

                    }
                    parentThing.childrenThingsIds = parentThing.childrenThingsIds.Append (thing.thingId).ToArray ();
                    childrenThing.parentThingId = parentId;
                    _context.Things.Update (childrenThing);
                    _context.Things.Update (parentThing);
                    await _context.SaveChangesAsync ();
                    return Created ($"api/things/{parentThing.thingId}", parentThing);
                }
                return BadRequest (ModelState);
            }
            return NotFound ();
        }

        // DELETE api/values/5
        [HttpDelete ("{parentId}")]
        [SecurityFilter ("things__allow_update")]
        public async Task<IActionResult> Delete (int parentId, [FromBody] Thing thing) {
            var parentThing = await _context.Things.Where (x => x.thingId == parentId).FirstOrDefaultAsync ();
            var childrenThing = await _context.Things.Where (x => x.thingId == thing.thingId).FirstOrDefaultAsync ();

            if (parentThing != null && childrenThing != null) {
                if (!parentThing.childrenThingsIds.Contains (thing.thingId) || childrenThing.parentThingId != parentId)
                    ModelState.AddModelError ("ChildrenError", "This thing is not in this parent");

                if (ModelState.IsValid) {
                    parentThing.childrenThingsIds = parentThing.childrenThingsIds.Where (val => val != thing.thingId).ToArray ();
                    childrenThing.parentThingId = null;
                    await _context.SaveChangesAsync ();
                    return Created ($"api/things/{parentThing.thingId}", parentThing);
                }
                return BadRequest (ModelState);

            }
            return NotFound ();
        }
    }

}