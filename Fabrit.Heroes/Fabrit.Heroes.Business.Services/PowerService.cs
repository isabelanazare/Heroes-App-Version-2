using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Business.Power;
using Fabrit.Heroes.Data.Entities.Power;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services
{
    public class PowerService : IPowerService
    {
        private readonly HeroesDbContext _context;
        private readonly IElementService _elementService;

        public PowerService(HeroesDbContext context, IElementService elementService)
        {
            _context = context;
            _elementService = elementService;
        }

        public IAsyncEnumerable<Power> GetAll()
        {
            return _context.Powers
                        .Include(p => p.Heroes)
                        .Include(p => p.Element)
                        .AsAsyncEnumerable();
        }

        public async Task<Power> FindByName(string powerName)
        {
            var power = await _context.Powers.Where(p => p.Name.Equals(powerName))
                                           .FirstOrDefaultAsync();

            return power != null
                ? power
                : throw new EntityNotFoundException($"There is no power with name: {powerName}");
        }

        public async Task<Power> FindById(int id)
        {
            var power = await _context.Powers.Where(p => p.Id.Equals(id))
                                           .FirstOrDefaultAsync();

            return power != null
                ? power
                : throw new EntityNotFoundException($"There is no power with id: {id}");
        }

        public async Task<PowerDto> FindPowerDtoById(int id)
        {
            var power = await _context.Powers.Where(p => p.Id.Equals(id))
                                             .Select(p => new PowerDto
                                             {
                                                 Id = p.Id,
                                                 Details = p.Details,
                                                 ElementId = p.ElementId,
                                                 Element = p.Element.Type.ToString(),
                                                 MainTrait = p.MainTrait,
                                                 Name = p.Name,
                                                 Strength = p.Strength
                                             })
                                            .FirstOrDefaultAsync();

            return power != null
                ? power
                : throw new EntityNotFoundException($"There is no power with id: {id}");
        }

        public async Task UpdatePower(PowerDto powerDto)
        {
            await CheckPowerFields(powerDto);
            var powerForUpdate = await FindById(powerDto.Id);

            powerForUpdate.MainTrait = powerDto.MainTrait;
            powerForUpdate.Name = powerDto.Name;
            powerForUpdate.Strength = powerDto.Strength;
            powerForUpdate.Details = powerDto.Details;
            powerForUpdate.ElementId = powerDto.ElementId;

            _context.Powers.Update(powerForUpdate);
            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<RowPowerData> GetPowersTableData()
        {
            return _context.Powers
                .Include(p => p.Element)
                .Select(power => new RowPowerData
                {
                    Id = power.Id,
                    Name = power.Name,
                    MainTrait = power.MainTrait,
                    Element = power.Element.Type.ToString(),
                    Details = power.Details,
                    Strength = power.Strength
                })
                .AsAsyncEnumerable();
        }

        public async Task CreatePower(PowerDto powerDto)
        {
            await CheckPowerFields(powerDto);

            Power newPower = new Power
            {
                Name = powerDto.Name,
                Details = powerDto.Details,
                Strength = powerDto.Strength,
                MainTrait = powerDto.MainTrait,
                ElementId = powerDto.ElementId
            };

            await _context.AddAsync(newPower);
            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<string> GetPowersName()
        {
            var powersName = _context.Powers
                                     .Select(p => p.Name)
                                     .AsAsyncEnumerable();

            return powersName;
        }

        public async Task DeletePowerById(int id)
        {
            Power power = await _context.Powers.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (power == null)
            {
                throw new EntityNotFoundException($"There is no power with id: {id}");
            }

            _context.Powers.Remove(power);
            await _context.SaveChangesAsync();
        }

        private async Task CheckPowerFields(PowerDto powerDto)
        {
            await _elementService.FindById(powerDto.ElementId);
            if (string.IsNullOrEmpty(powerDto.Name) || string.IsNullOrEmpty(powerDto.Strength.ToString()) || string.IsNullOrEmpty(powerDto.MainTrait) || string.IsNullOrEmpty(powerDto.ElementId.ToString()) || string.IsNullOrEmpty(powerDto.Details))
            {
                throw new NullParameterException($"There are null parameters");
            }

            if (powerDto.Strength < 1 || powerDto.Strength > 100)
            {
                throw new OutOfRangeException(nameof(PowerDto.Strength));
            }
        }
    }
}
