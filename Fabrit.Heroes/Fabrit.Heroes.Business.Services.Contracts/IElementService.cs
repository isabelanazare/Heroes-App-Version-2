using Fabrit.Heroes.Data.Business.Element;
using Fabrit.Heroes.Data.Entities.Power;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IElementService
    {
        Task<Element> FindById(int id);
        IAsyncEnumerable<ElementDto> GetAll();
    }
}
