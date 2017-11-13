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
    public class ParametersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParametersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get([FromQuery]int startat, [FromQuery]int quantity)
        {

            if (quantity == 0)
                quantity = 50;
            var paremeters = await _context.Parameters
            .OrderBy(x => x.thingGroupId)
             .Select(item => new
             {
                 item.ParameterCode,
                 item.parameterDescription,
                 item.parameterId,
                 item.parameterName,
                 item.physicalTag,
                 item.thingGroupId,
                 thingGroup = new
                 {
                     item.thingGroupId,
                     item.thingGroup.groupCode,
                     item.thingGroup.groupDescription,
                     item.thingGroup.groupName,
                     item.thingGroup.thingsIds
                 }
             })

            .Skip(startat).Take(quantity)
            .ToListAsync();
            return Ok(paremeters);
        }

        [HttpGet("list/")]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> GetList([FromQuery]int[] parameterId)
        {
            var things = await _context.Parameters
            .Where(x => parameterId.Contains(x.parameterId))
            .Select(item => new
            {
                item.ParameterCode,
                item.parameterDescription,
                item.parameterId,
                item.parameterName,
                item.physicalTag,
                item.thingGroupId,
                thingGroup = new
                {
                    item.thingGroupId,
                    item.thingGroup.groupCode,
                    item.thingGroup.groupDescription,
                    item.thingGroup.groupName,
                    item.thingGroup.thingsIds
                }
            })
            .ToListAsync();
            return Ok(things);
        }

        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get(int id)
        {
            var parameter = await _context.Parameters
            .Where(x => x.parameterId == id).Select(item => new
            {
                item.ParameterCode,
                item.parameterDescription,
                item.parameterId,
                item.parameterName,
                item.physicalTag,
                item.thingGroupId,
                thingGroup = new
                {
                    item.thingGroupId,
                    item.thingGroup.groupCode,
                    item.thingGroup.groupDescription,
                    item.thingGroup.groupName,
                    item.thingGroup.thingsIds
                }
            })
            .FirstOrDefaultAsync();

            if (parameter == null)
                return NotFound();

            return Ok(parameter);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Parameter parameter)
        {
            parameter.parameterId = 0;
            parameter.physicalTag = parameter.physicalTag != null ? parameter.physicalTag.ToLower() : null;
            if (ModelState.IsValid)
            {
                await _context.AddAsync(parameter);
                await _context.SaveChangesAsync();

                return Created($"api/parameters/{parameter.parameterId}", parameter);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Parameter parameter)
        {
            if (ModelState.IsValid)
            {
                var curThing = await _context.Parameters
                .AsNoTracking()
                .Where(x => x.parameterId == id)
                .FirstOrDefaultAsync();

                parameter.physicalTag = parameter.physicalTag != null ? parameter.physicalTag.ToLower() : null;

                if (id != parameter.parameterId)
                {
                    return NotFound();
                }
                _context.Parameters.Update(parameter);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var parameter = await _context.Parameters.Where(x => x.parameterId == id).FirstOrDefaultAsync();
            if (parameter != null)
            {
                _context.Entry(parameter).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
    }

}