using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Trusteeship.Contracts
{
    public interface TrusteeRepository
    {
        void Add(Trustee trustee);
        Task<Trustee> GetById(int id);
    }
}
