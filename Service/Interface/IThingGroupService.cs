using System.Collections.Generic;
using System.Threading.Tasks;
using thingservice.Model;

namespace thingservice.Service.Interface {
    public interface IThingGroupService {
        Task<(List<ThingGroup>, int)> getThingGroups (int startat, int quantity);
        Task<ThingGroup> getThingGroup (int thingGroupId);
        Task<List<ThingGroup>> getThingGroupsById (int[] thingGroupIds);
        Task<ThingGroup> createThingGroup (ThingGroup thingGroup);
        Task<ThingGroup> updateThingGroup (int thingGroupId, ThingGroup thingGroup);
        Task<ThingGroup> deleteThingGroup (int thingGroupId);

        Task<List<Thing>> GetThingsFromThingGroup (int thingGroupId);
        Task<List<ThingGroup>> GetGroupsFromThing (int thingId);

    }
}