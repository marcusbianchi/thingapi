using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thingservice.Model
{
    public class ThingGroup
    {
        public int thingGroupId { get; set; }
        [Required]
        [MaxLength(50)]
        public string groupName { get; set; }
        [MaxLength(100)]
        public string groupDescription { get; set; }
        [MaxLength(100)]
        public string groupPrefix { get; set; }
        public bool enabled { get; set; }
        [MaxLength(50)]
        public string groupCode { get; set; }
        [Column("thingsIds", TypeName = "integer[]")]
        public int[] thingsIds { get; set; }
        public ICollection<ToolGroupAssociated> toolGroupAssociates {get;set;}
        public ICollection<Tag> Tags { get; set; }
    }
}