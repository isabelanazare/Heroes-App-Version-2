using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fabrit.Heroes.Data.Entities.Power;
using Fabrit.Heroes.Infrastructure.Common.Data;
using System;

namespace Fabrit.Heroes.Data.Entities.Hero
{
    public class HeroPower : IDataEntity
    {
        public int Id { get; set; }
        public int HeroId { get; set; }
        public Hero Hero { get; set; }
        public int PowerId { get; set; }
        public Power.Power Power { get; set; }
        public DateTime? LastTrainingTime { get; set; }
        public DateTime? LastChangeTime { get; set; }

        public int Strength { get; set; }
    }

    public class HeroPowerConfiguration : IEntityTypeConfiguration<HeroPower>
    {
        public void Configure(EntityTypeBuilder<HeroPower> builder)
        {
            builder
                .HasKey(hp => hp.Id);

            builder
                .HasOne(hp => hp.Hero)
                .WithMany(hero => hero.Powers)
                .HasForeignKey(hp => hp.HeroId);

            builder
                .HasOne(hp => hp.Power)
                .WithMany(power => power.Heroes)
                .HasForeignKey(hp => hp.PowerId);
        }
    }
}