using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thingservice.Data;
using thingservice.Model;
using thingservice.Service.Interface;

namespace thingservice.Controllers
{
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITagService _tagService;

        public TagsController(ApplicationDbContext context,ITagService tagService)
        {
            _context = context;
            _tagService = tagService;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> Get([FromQuery]int startat, [FromQuery]int quantity,
        [FromQuery]string fieldFilter, [FromQuery]string fieldValue,
            [FromQuery]string orderField, [FromQuery]string order)
        {
            var fieldFilterEnum = TagFieldEnum.Default;
            Enum.TryParse(fieldFilter, true, out fieldFilterEnum);
            var orderFieldEnum = TagFieldEnum.Default;
            Enum.TryParse(orderField, true, out orderFieldEnum);
            var orderEnumValue = OrderEnum.Ascending;
            Enum.TryParse(order, true, out orderEnumValue);

            if (quantity == 0)
                quantity = 50;

            var (parameter, total)  = await _tagService.getTags(startat,quantity,fieldFilterEnum,fieldValue,orderFieldEnum,orderEnumValue);

            return Ok(new { values = parameter, total = total });
        }

        [HttpGet("list/")]
        [ResponseCache(CacheProfileName = "thingscache")]
        public async Task<IActionResult> GetList([FromQuery]int[] tagId)
        {
            var things = await _context.Tags
            .Where(x => tagId.Contains(x.tagId))
            .Select(item => new
            {
                item.tagId,
                item.tagDescription,
                item.tagName,
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
            var parameter = await _context.Tags
            .Where(x => x.tagId == id).Select(item => new Tag()
            {
                tagId = item.tagId,
                tagDescription = item.tagDescription,
                tagName = item.tagName,
                physicalTag = item.physicalTag,
                tagGroup = item.tagGroup,
                tagType = item.tagType,
                thingGroupId = item.thingGroupId,
                thingGroup = new ThingGroup()
                {
                    thingGroupId = item.thingGroupId,
                    groupCode = item.thingGroup.groupCode,
                    groupDescription = item.thingGroup.groupDescription,
                    groupName = item.thingGroup.groupName,
                    thingsIds = item.thingGroup.thingsIds
                }
            })
            .FirstOrDefaultAsync();

            if (parameter == null)
                return NotFound();

            return Ok(parameter);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Tag tag)
        {

            if (ModelState.IsValid)
            {
                tag.tagId = 0;
                tag.physicalTag = tag.physicalTag != null ? tag.physicalTag.ToLower() : null;
                await _context.AddAsync(tag);
                await _context.SaveChangesAsync();

                return Created($"api/tags/{tag.tagId}", tag);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Tag tag)
        {
            if (ModelState.IsValid)
            {
                var curThing = await _context.Tags
                .AsNoTracking()
                .Where(x => x.tagId == id)
                .FirstOrDefaultAsync();

                tag.physicalTag = tag.physicalTag != null ? tag.physicalTag.ToLower() : null;

                if (id != tag.tagId)
                {
                    return NotFound();
                }
                _context.Tags.Update(tag);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var parameter = await _context.Tags.Where(x => x.tagId == id).FirstOrDefaultAsync();
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