using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Business.Villain;
using Fabrit.Heroes.Data.Entities.Battle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Battle
{
    public class BattleDto
    {
        public int Id { get; set; }
        public int InitiatorId { get; set; }
        public int OpponentId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<HeroDto> Heroes { get; set; }
        public IEnumerable<VillainDto> Villains { get; set; }
        public int HeroesStrength { get; set; }
        public int VillainsStrength { get; set; }
    }
}
