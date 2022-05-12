
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

public interface IBooksRepository
{
    IEnumerable<BookData> GetAll();
    Task<BookData> CreateBook([FromBody] BookData bookData);
    Task<BookData> GetBookById([FromRoute] Guid BookId);
    Task<BookData> UpdateBookById([FromRoute] Guid BookId, [FromBody] BookData bookData);
    Task<BookData> DeleteBookById([FromRoute] Guid BookId);

}