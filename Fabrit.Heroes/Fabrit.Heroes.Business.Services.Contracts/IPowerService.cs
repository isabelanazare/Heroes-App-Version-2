using Fabrit.Heroes.Data.Business.Power;
using Fabrit.Heroes.Data.Entities.Power;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IPowerService
    {
        Task<Power> FindByName(string powerName);
        Task<Power> FindById(int id);
        Task<PowerDto> FindPowerDtoById(int id);
        IAsyncEnumerable<RowPowerData> GetPowersTableData();
        Task CreatePower(PowerDto powerDto);
        IAsyncEnumerable<string> GetPowersName();
        Task DeletePowerById(int id);
        IAsyncEnumerable<Power> GetAll();
        Task UpdatePower(PowerDto powerDto);
    }
}
