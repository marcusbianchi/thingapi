using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThingsAPI.Model
{
    public class Parameter
    {
        public int parameterId { get; set; }
        [Required]
        [MaxLength(50)]
        public string parameterName { get; set; }
        [MaxLength(100)]
        public string parameterDescription { get; set; }
        [MaxLength(100)]
        public string physicalTag { get; set; }
        [MaxLength(50)]
        public string ParameterCode { get; set; }
        public int thingGroupId { get; set; }
    }
}