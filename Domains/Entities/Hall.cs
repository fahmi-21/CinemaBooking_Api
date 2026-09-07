using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Hall : BaseEntity
    {
        public int BranchId { get; set; }

        public string Name { get; set; } = null!;

        public HallType Type { get; set; }

        public int Capacity { get; set; }

        public int CleaningBufferMinutes { get; set; }

        // Navigation Properties
        public Branch Branch { get; set; } = null!;

        public ICollection<Section> Sections { get; set; } = new List<Section>();

        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
    }
}
