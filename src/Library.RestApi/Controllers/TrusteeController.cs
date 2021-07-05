using Library.Services.Trusteeship.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrusteeController : ControllerBase
    {
        private readonly TrusteeService _service;
        public TrusteeController(TrusteeService service) => _service = service;

        [HttpPost]
        public async Task<int> Add(CreateTrusteeDto dto) => await _service.Add(dto);

        [HttpPut("id")]
        public async Task Update(int id) => await _service.Update(id);
    }
}
