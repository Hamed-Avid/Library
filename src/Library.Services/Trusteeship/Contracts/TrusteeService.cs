using System;
using System.Threading.Tasks;

namespace Library.Services.Trusteeship.Contracts
{
    public interface TrusteeService
    {
        Task<int> Add(CreateTrusteeDto dto);
        Task Update(int id);
    }
}
