using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int thingGroupId { get; set; }
        public ThingGroup thingGroup { get; set; }
    }
}