using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Business
{
    public class BooksBusiness : IBooksBusiness
    {
        private readonly IBooksRepository _booksRepository;
        public BooksBusiness(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public IEnumerable<BookData> GetAll()
        {
            return _booksRepository.GetAll();
        }
        public async Task<BookData> CreateBook([FromBody] BookData bookData)
        {
            return await _booksRepository.CreateBook(bookData);
        }
        public async Task<BookData> GetBookById([FromRoute] Guid BookId)
        {
            return await _booksRepository.GetBookById(BookId);
        }
        public async Task<BookData> UpdateBookById([FromRoute] Guid BookId, [FromBody] BookData bookData)
        {
            return await _booksRepository.UpdateBookById(BookId, bookData);
        }
        public async Task<BookData> DeleteBookById([FromRoute] Guid BookId)
        {
            return await _booksRepository.DeleteBookById(BookId);
        }
    }
}
