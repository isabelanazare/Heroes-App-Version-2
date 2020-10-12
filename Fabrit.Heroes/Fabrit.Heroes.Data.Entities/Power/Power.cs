using Fabrit.Heroes.Data.Entities.Hero;
using Fabrit.Heroes.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fabrit.Heroes.Data.Entities.Power
{
    public class Power : IDataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Strength { get; set; }
        public Element Element { get; set; }
        public int ElementId { get; set; }
        public ICollection<HeroPower> Heroes { get; set; }
        public string MainTrait { get; set; }
    }

    public class PowerConfiguartion : IEntityTypeConfiguration<Power>
    {
        public void Configure(EntityTypeBuilder<Power> builder)
        {
            builder
                .HasKey(power => power.Id);
            builder
                .HasOne(element => element.Element);
        }
    }

    public enum ElementType
    {
        Fire = 1,
        Earth = 2,
        Air = 3,
        Water = 4
    }
}