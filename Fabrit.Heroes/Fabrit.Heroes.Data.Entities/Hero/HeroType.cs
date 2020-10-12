using Fabrit.Heroes.Infrastructure.Common.Data;

namespace Fabrit.Heroes.Data.Entities.Hero
{
    public enum EntityType
    {
        Fictional = 1,
        Real
    }

    public class HeroType : IDataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}