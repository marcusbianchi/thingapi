using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thingservice.Model
{
    public class Thing
    {
        public int thingId { get; set; }
        public int? parentThingId { get; set; }
        [Required]
        [MaxLength(50)]
        public string thingName { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string physicalConnection { get; set; }
        public bool enabled { get; set; }
        [MaxLength(50)]
        public string thingCode { get; set; }
        public int position { get; set; }
        [Column("childrenThingsIds", TypeName = "integer[]")]
        public int[] childrenThingsIds { get; set; }
    }
}