using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public int HallId { get; set; }

        public Hall Hall { get; set; } = null!;

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}
