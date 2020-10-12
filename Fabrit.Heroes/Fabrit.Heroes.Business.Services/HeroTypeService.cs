using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Entities.Hero;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services
{
    public class HeroTypeService: IHeroTypeService
    {
        private readonly HeroesDbContext _context;

        public HeroTypeService(HeroesDbContext context)
        {
            _context = context;
        }

        public async Task<HeroType> FindById(int id)
        {
            var heroType = await _context.HeroTypes.Where(ht => ht.Id.Equals(id))
                                             .FirstOrDefaultAsync();

            return heroType != null
                ? heroType
                : throw new EntityNotFoundException($"No HeroType with id: {id}");
        }

        public IAsyncEnumerable<HeroTypeDto> GetAll()
        {
            return _context.HeroTypes
                        .Select(ht => new HeroTypeDto
                        {
                            Id = ht.Id,
                            Name = ht.Name
                        })
                        .AsAsyncEnumerable();
        }
    }
}
