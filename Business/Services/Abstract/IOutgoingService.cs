using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Services.Abstract
{
    public interface IOutgoingService
    {
        Task<List<Outgoing>> GetAllAsync();
        Task<Outgoing> Get(int id);
        Task<IDataResult<Outgoing>> Add(Outgoing outgoing);
        Task<IResult> UpdateAsync(Outgoing outgoing);
        Task<IResult> Delete(Outgoing outgoing);
    }
}