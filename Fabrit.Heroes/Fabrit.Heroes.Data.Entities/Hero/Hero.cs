using Fabrit.Heroes.Data.Entities.Badge;
using Fabrit.Heroes.Data.Entities.Battle;
using Fabrit.Heroes.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fabrit.Heroes.Data.Entities.Hero
{
    public class Hero : IDataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<HeroPower> Powers { get; set; }
        public HeroPower MainPower { get; set; }
        public HeroType Type { get; set; }
        public int TypeId { get; set; }
        public virtual ICollection<HeroAlly> Allies { get; set; }
        public string AvatarPath { get; set; }
        public DateTime Birthday { get; set; }
        public int OverallStrength { get; set; }
        public bool IsBadGuy { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
      
        public int? UserId { get; set; }
        public virtual Fabrit.Heroes.Data.Entities.User.User User { get; set; }
        public ICollection<HeroBadge> Badges { get; set; }
        public ICollection<HeroBattle> Battles { get; set; }
        public DateTime LastTimeMoved { get; set; }
        public bool IsGod { get; set; }
    }

    public class HeroConfiguration : IEntityTypeConfiguration<Hero>
    {
        public void Configure(EntityTypeBuilder<Hero> builder)
        {
            builder.HasKey(hero => hero.Id);
            builder.Property(hero => hero.Name).IsRequired();
            builder.HasIndex(nameof(Hero.Name)).IsUnique();
        }
    }
}