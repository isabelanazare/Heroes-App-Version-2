using Fabrit.Heroes.Data.Business.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Villain
{
    public class VillainPowersDto
    {
        public int VillainId { get; set; }
        public IEnumerable<int> PowerIds { get; set; }
    }
}
