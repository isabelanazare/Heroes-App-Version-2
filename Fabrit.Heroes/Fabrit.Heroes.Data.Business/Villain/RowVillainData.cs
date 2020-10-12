using System.Collections.Generic;

namespace Fabrit.Heroes.Data.Business.Villain
{
    public class RowVillainData
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
    }
}
