using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace thingservice.Model
{
    public class Tag
    {
        public int tagId { get; set; }
        [Required]
        [MaxLength(50)]
        public string tagName { get; set; }
        [MaxLength(100)]
        public string tagDescription { get; set; }
        [MaxLength(100)]
        public string physicalTag { get; set; }
        [MaxLength(50)]
        public string tagGroup{get;set;}
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public TagTypeEnum tagType{get;set;}
        public int thingGroupId { get; set; }
        public ThingGroup thingGroup { get; set; }
    }
}