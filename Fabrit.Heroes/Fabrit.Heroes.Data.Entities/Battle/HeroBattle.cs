using Fabrit.Heroes.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabrit.Heroes.Data.Entities.Battle
{
    public class HeroBattle : IDataEntity
    {
        public int Id { get; set; }
        public int HeroId { get; set; }
        public Fabrit.Heroes.Data.Entities.Hero.Hero Hero { get; set; }
        public int BattleId { get; set; }
        public Battle Battle { get; set; }
        public bool HasWon { get; set; }
    }

    public class HeroBattleConfiguration : IEntityTypeConfiguration<HeroBattle>
    {
        public void Configure(EntityTypeBuilder<HeroBattle> builder)
        {
            builder
                .HasKey(hb => hb.Id);

            builder
                .HasOne(hb => hb.Battle)
                .WithMany(b => b.Heroes)
                .HasForeignKey(hb => hb.BattleId);

            builder
                .HasOne(hb => hb.Hero)
                .WithMany(h => h.Battles)
                .HasForeignKey(hb => hb.HeroId);
        }
    }
}
