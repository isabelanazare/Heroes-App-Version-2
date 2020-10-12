using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Hero
{
    public class RowHeroData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainPower { get; set; }
        public IEnumerable<string> Ally { get; set; }
        public IEnumerable<string> OtherPowers { get; set; }
        public IEnumerable<string> AllOrderedPowers { get; set; }
        public string AvatarPath { get; set; }
        public string Birthday { get; set; }
        public int OverallStrength { get; set; }
        public bool IsBadGuy { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
