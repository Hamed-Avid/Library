using Library.Entities;
using Library.Services.Books.Contracts;
using Library.Services.Books.Exceptions;
using Library.Services.Categories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Books
{
    public class BookAppService : BookService
    {
        private readonly BookRepository _repository;
        private readonly CategoryRepository _category;
        private readonly UnitOfwork _unitOfwork;
        public BookAppService(BookRepository repository, UnitOfwork unitOfwork,
            CategoryRepository category)
        {
            _repository = repository;
            _category = category;
            _unitOfwork = unitOfwork;
        }

        public async Task<int> Add(CreateBookDto dto)
        {
            await GourdAgainstInvalidCategory(dto);
            var book = new Book
            {
                Title = dto.Title,
                Writer = dto.Writer,
                MinAge = dto.MinAge,
                MaxAge = dto.MaxAge,
                CategoryId = dto.CategoryId
            };
            _repository.Add(book);
            await _unitOfwork.Complete();
            return book.Id;
        }

        public async Task<List<GetBookDto>> GetByCategory(int categoryId)
        {
            return await _repository.GetByCategory(categoryId);
        }


        public async Task Update(int id, UpdateBookDto dto)
        {
            var book = await _repository.GetById(id);

            if (dto.Title != null)
                book.Title = dto.Title;
            else if (dto.Writer != null)
                book.Writer = dto.Writer;
            else if (dto.MinAge != null)
                book.MinAge = (byte)dto.MinAge;
            else if (dto.MaxAge != null)
                book.MaxAge = dto.MaxAge;
            else if (dto.CategoryId != null)
                book.CategoryId = (int)dto.CategoryId;

            //book.Title = dto.Title;
            //book.Writer = dto.Writer;
            //book.MinAge = dto.MinAge;
            //book.MaxAge = dto.MaxAge;
            //book.CategoryId = dto.CategoryId;

            await _unitOfwork.Complete();
        }

        private async Task GourdAgainstInvalidCategory(CreateBookDto dto)
        {
            if (!await _category.IsExistById(dto.CategoryId))
            {
                throw new InvalidCategoryException();
            }
        }
    }
}
