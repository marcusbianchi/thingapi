using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using thingservice.Model;
namespace thingservice.Service.Interface
{
    
    public interface ITagService
    {
         Task<(List<Tag>, int)> getTags(int startat, int quantity, TagFieldEnum fieldFilter, string fieldValue, TagFieldEnum orderField, OrderEnum order);
    }
}