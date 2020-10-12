using Fabrit.Heroes.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Entities.Battle
{
    public class Battle : IDataEntity
    {
        public int Id { get; set; }
        public int InitiatorId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ICollection<HeroBattle> Heroes { get; set; }
    }
    public class BattleConfiguration : IEntityTypeConfiguration<Battle>
    {
        public void Configure(EntityTypeBuilder<Battle> builder)
        {
            builder.HasKey(battle => battle.Id);
            builder.Property(battle => battle.InitiatorId).IsRequired();
        }
    }
}
