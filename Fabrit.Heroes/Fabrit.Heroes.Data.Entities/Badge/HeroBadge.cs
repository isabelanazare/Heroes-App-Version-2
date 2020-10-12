using Fabrit.Heroes.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabrit.Heroes.Data.Entities.Badge
{
    public class HeroBadge : IDataEntity
    {
        public int Id { get; set; }
        public int BadgeId { get; set; }
        public virtual Badge Badge { get; set; }
        public int HeroId { get; set; }
        public virtual Fabrit.Heroes.Data.Entities.Hero.Hero Hero { get; set; }
    }

    public class HeroBadgeConfiguration : IEntityTypeConfiguration<HeroBadge>
    {
        public void Configure(EntityTypeBuilder<HeroBadge> builder)
        {
            builder
                .HasKey(hb => hb.Id);

            builder
                .HasOne(hb => hb.Hero)
                .WithMany(hb => hb.Badges)
                .HasForeignKey(hb => hb.HeroId);

            builder
                .HasOne(hb => hb.Badge)
                .WithMany(hb => hb.Heroes)
                .HasForeignKey(hb => hb.BadgeId);
        }
    }
}
