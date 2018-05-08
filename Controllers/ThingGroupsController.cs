using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using securityfilter;
using thingservice.Data;
using thingservice.Model;
using thingservice.Service.Interface;

namespace thingservice.Controllers {
    [Route ("api/[controller]")]
    public class ThingGroupsController : Controller {
        private readonly IThingGroupService _thingGroupService;

        public ThingGroupsController (IThingGroupService thingGroupService) {
            _thingGroupService = thingGroupService;
        }

        [HttpGet]
        [SecurityFilter ("thinggroups__allow_read")]
        [ResponseCache (CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get ([FromQuery] int startat, [FromQuery] int quantity) {

            if (quantity == 0)
                quantity = 50;
            var (groups, total) = await _thingGroupService.getThingGroups (startat, quantity);
            return Ok (new { values = groups, total = total });
        }

        [HttpGet ("{id}")]
        [SecurityFilter ("thinggroups__allow_read")]
        [ResponseCache (CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get (int id) {
            var group = await _thingGroupService.getThingGroup (id);
            if (group == null)
                return NotFound ();
            return Ok (group);
        }

        [HttpGet ("getgroups/{thingid}")]
        [SecurityFilter ("thinggroups__allow_read")]
        [ResponseCache (CacheProfileName = "thingscache")]
        public async Task<IActionResult> GetGroups (int thingid) {
            var group = await _thingGroupService.GetThingsFromThingGroup (thingid);
            if (group == null)
                return NotFound ();
            return Ok (group);
        }

        [HttpPost]
        [SecurityFilter ("thinggroups__allow_update")]
        public async Task<IActionResult> Post ([FromBody] ThingGroup thingGroup) {
            thingGroup.thingGroupId = 0;
            thingGroup.thingsIds = new int[0];
            if (ModelState.IsValid) {
                thingGroup = await _thingGroupService.createThingGroup (thingGroup);
                return Created ($"api/thinggroups/{thingGroup.thingGroupId}", thingGroup);
            }
            return BadRequest (ModelState);
        }

        [HttpPut ("{id}")]
        [SecurityFilter ("thinggroups__allow_update")]
        public async Task<IActionResult> Put (int id, [FromBody] ThingGroup thingGroup) {
            if (ModelState.IsValid) {
                var curThing = await _thingGroupService.updateThingGroup (id, thingGroup);
                if (id != thingGroup.thingGroupId) {
                    return NotFound ();
                }
                return NoContent ();

            }
            return BadRequest (ModelState);
        }

        [HttpDelete ("{id}")]
        [SecurityFilter ("thinggroups__allow_update")]
        public async Task<IActionResult> Delete (int id) {
            var curThing = await _thingGroupService.deleteThingGroup (id);
            if (curThing != null) {

                return NoContent ();
            }
            return NotFound ();
        }

        [HttpGet ("list/")]
        [SecurityFilter ("thinggroups__allow_update")]
        [ResponseCache (CacheProfileName = "thingscache")]
        public async Task<IActionResult> GetList ([FromQuery] int[] thingGroupId) {
            var things = await _thingGroupService.getThingGroupsById (thingGroupId);
            return Ok (things);
        }
    }

}