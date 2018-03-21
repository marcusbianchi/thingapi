using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thingservice.Model {
    public class ThingGroup {
        public int thingGroupId { get; set; }

        [Required]
        [MaxLength (50)]
        public string groupName { get; set; }

        [MaxLength (100)]
        public string groupDescription { get; set; }

        [MaxLength (100)]
        public string groupPrefix { get; set; }
        public bool enabled { get; set; }

        [MaxLength (50)]
        public string groupCode { get; set; }

        [Column ("thingsIds", TypeName = "integer[]")]
        public int[] thingsIds { get; set; }

        [Column ("possibleTagGroups", TypeName = "string[]")]
        private string[] _possibleTagGroups;
        public string[] possibleTagGroups {
            get {
                if (this._possibleTagGroups == null)
                    return new string[0];
                else
                    return this._possibleTagGroups;
            }
            set { this._possibleTagGroups = value; }
        }
        public ICollection<ToolGroupAssociated> toolGroupAssociates { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}