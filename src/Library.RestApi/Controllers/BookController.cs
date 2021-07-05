using Library.Services.Books.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _service;
        public BookController(BookService service) => _service = service;

        [HttpPost]
        public async Task<int> Add(CreateBookDto dto) => await _service.Add(dto);

        [HttpGet("CategoryId")]
        public async Task<List<GetBookDto>> GetByCategory(int CategoryId) 
            => await _service.GetByCategory(CategoryId);

        [HttpPut("bookId")]
        public async Task Update(int bookId, UpdateBookDto dto) => await _service.Update(bookId, dto);
    }
}
