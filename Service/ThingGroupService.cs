using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using thingservice.Data;
using thingservice.Model;
using thingservice.Service.Interface;
namespace thingservice.Service {
    public class ThingGroupService : IThingGroupService {
        private readonly ApplicationDbContext _context;

        public ThingGroupService (ApplicationDbContext context) {
            _context = context;
        }

        public async Task<ThingGroup> createThingGroup (ThingGroup thingGroup) {
            await _context.AddAsync (thingGroup);
            await _context.SaveChangesAsync ();
            return thingGroup;
        }

        public async Task<ThingGroup> deleteThingGroup (int thingGroupId) {
            var curThing = await _context.ThingGroups
                .Where (x => x.thingGroupId == thingGroupId)
                .FirstOrDefaultAsync ();
            if (curThing != null) {
                curThing.enabled = false;
                _context.Entry (curThing).State = EntityState.Modified;
                await _context.SaveChangesAsync ();
                return curThing;
            }
            return null;
        }

        public async Task<List<ThingGroup>> GetGroupsFromThing (int thingId) {
            var group = await _context.ThingGroups
                .Include (x => x.Tags)
                .Include (x => x.toolGroupAssociates)
                .OrderBy (x => x.thingGroupId)
                .Where (x => x.thingsIds.Contains (thingId))
                .ToListAsync ();
            return group;
        }

        public async Task<ThingGroup> getThingGroup (int thingGroupId) {
            var group = await _context.ThingGroups
                .Include (x => x.Tags)
                .Include (x => x.toolGroupAssociates)
                .OrderBy (x => x.thingGroupId)
                .Where (x => x.thingGroupId == thingGroupId)
                .FirstOrDefaultAsync ();
            return group;
        }

        public async Task<(List<ThingGroup>, int)> getThingGroups (int startat, int quantity) {
            var groups = await _context.ThingGroups
                .Include (x => x.Tags)
                .Include (x => x.toolGroupAssociates)
                .Where (x => x.enabled == true)
                .OrderBy (x => x.thingGroupId)
                .Skip (startat).Take (quantity)
                .ToListAsync ();
            return (groups, groups.Count);
        }

        public async Task<List<ThingGroup>> getThingGroupsById (int[] thingGroupIds) {
            var things = await _context.ThingGroups
                .Where (x => thingGroupIds.Contains (x.thingGroupId))
                .ToListAsync ();
            return things;
        }

        public async Task<List<Thing>> GetThingsFromThingGroup (int thingGroupId) {
            var thingGroup = await _context.ThingGroups.Where (x => x.thingGroupId == thingGroupId).FirstOrDefaultAsync ();
            if (thingGroup != null) {
                var things = await _context.Things.Where (x => thingGroup.thingsIds.Contains (x.thingId)).ToListAsync ();
                return things;
            }
            return null;;
        }

        public async Task<ThingGroup> updateThingGroup (int thingGroupId, ThingGroup thingGroup) {
            var curThing = await _context.ThingGroups
                .AsNoTracking ()
                .Where (x => x.thingGroupId == thingGroupId)
                .FirstOrDefaultAsync ();
            thingGroup.thingsIds = curThing.thingsIds;
            if (thingGroupId != thingGroup.thingGroupId) {
                return null;
            }
            _context.ThingGroups.Update (thingGroup);
            await _context.SaveChangesAsync ();
            return await getThingGroup (thingGroupId);
        }
    }
}