using Library.Services.Trusteeship.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Trusteeship
{
    public class TrusteeAppService : TrusteeService
    {
        private readonly TrusteeRepository _repository;
        private readonly UnitOfwork _unitOfwork;
        public TrusteeAppService(TrusteeRepository repository, UnitOfwork unitOfwork)
        {
            _repository = repository;
            _unitOfwork = unitOfwork;
        }

    }
}
