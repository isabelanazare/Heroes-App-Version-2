using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Hero
{
    public class HeroPowerDto
    {
        public int Id { get; set; }
        public int PowerId {get;set;}
        public string Name { get; set; }
        public string Details { get; set; }
        public int Strength { get; set; }
        public string Element { get; set; }
        public int ElementId { get; set; }
        public string MainTrait { get; set; }
        public string LastTrained { get; set; }
        public bool IsMainPower { get; set; }
    }
}
