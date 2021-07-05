using Library.Entities;
using Library.Services.Trusteeship.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.EF.Trusteeship
{
    public class EFTrusteeRepository : TrusteeRepository
    {
        private readonly EFDataContext _context;
        public EFTrusteeRepository(EFDataContext context) => _context = context;

        public void Add(Trustee trustee) => _context.Trusteeship.Add(trustee);

        public async Task<Trustee> GetById(int id) =>
            await _context.Trusteeship.SingleOrDefaultAsync(_ => _.Id == id);
    }
}
