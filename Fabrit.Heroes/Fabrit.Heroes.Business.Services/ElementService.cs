  using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Business.Element;
using Fabrit.Heroes.Data.Entities.Power;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services
{
    public class ElementService : IElementService
    {
        private readonly HeroesDbContext _context;
        public ElementService(HeroesDbContext context)
        {
            _context = context;
        }
        public async Task<Element> FindById(int elementId)
        {
            var element = await _context.Elements.Where(e => e.Id == elementId)
                                                .FirstOrDefaultAsync();

            return element != null
                ? element
                : throw new EntityNotFoundException($"There is no hero with id: {elementId}");
        }

        public IAsyncEnumerable<ElementDto> GetAll()
        {
            return _context.Elements
                        .Select(el => new ElementDto
                        {
                            Id = el.Id,
                            Name = ((ElementType)el.Type).ToString()
                        })
                        .AsAsyncEnumerable();
        }
    }
}
