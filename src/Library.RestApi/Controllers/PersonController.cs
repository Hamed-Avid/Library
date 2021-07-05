using Library.Services.People.Contracts;
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
    public class PersonController : ControllerBase
    {
        private readonly PersonService _service;
        public PersonController(PersonService service) => _service = service;
        [HttpPost]
        public async Task<int> Add(CreatePersonDto dto) => await _service.Add(dto);
    }
}
