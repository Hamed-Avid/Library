using Library.Entities;
using System;

namespace Library.Services.Trusteeship.Contracts
{
    public class CreateTrusteeDto
    {
        public int BookId { get; set; }
        public int PersonId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
