using Fabrit.Heroes.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Entities.Hero
{
    public class HeroAlly : IDataEntity
    {
        public int Id { get; set; }

        public int AllyToId { get; set; }
        public virtual Hero AllyTo { get; set; }
        public int AllyFromId { get; set; }
        public virtual Hero AllyFrom { get; set; }
    }

    public class HeroAllyConfiguration : IEntityTypeConfiguration<HeroAlly>
    {
        public void Configure(EntityTypeBuilder<HeroAlly> builder)
        {
            builder
                .HasKey(ap => ap.Id);

            builder
                .HasOne(ap => ap.AllyFrom)
                .WithMany(hero => hero.Allies)
                .HasForeignKey(ha => ha.AllyFromId);
            builder
                .HasOne(ap => ap.AllyTo)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ha => ha.AllyToId);
        }
    }
}
