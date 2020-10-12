using Fabrit.Heroes.Data.Business.Power;
using Fabrit.Heroes.Data.Entities.Badge;
using System.Collections.Generic;

namespace Fabrit.Heroes.Data.Business.Villain
{
    public class VillainDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<VillainDto> Allies { get; set; }
        public IEnumerable<PowerDto> Powers { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public PowerDto MainPower { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Email { get; set; }
        public string AvatarPath { get; set; }
        public string Birthday { get; set; }
        public int OverallStrength { get; set; }
        public ICollection<HeroBadge> Badges { get; set; }
        public bool IsGod { get; set; }
    }
}
