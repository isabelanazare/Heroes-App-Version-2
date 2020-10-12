using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Hero
{
    public class EntityTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBadGuy { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
