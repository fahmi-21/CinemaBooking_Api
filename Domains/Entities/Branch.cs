using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Branch : BaseEntity
    {

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string GoogleMapsUrl { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<Hall> Halls { get; set; } = new List<Hall>();
    }
}
