using Fabrit.Heroes.Infrastructure.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Entities.Badge
{
    public enum BadgeType
    {
        TierCategory1 = 1,
        TierCategory2 = 2,
        TierCategory3 = 3,
        TierCategory4 = 4,
        TierCategory5 = 5
    }

    public class Badge : IDataEntity
    {
        public int Id { get; set; }
        public BadgeType Tier { get; set; }
        public ICollection<HeroBadge> Heroes { get; set; }
    }
}
