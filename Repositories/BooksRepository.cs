using bookstore_api.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Data;


namespace WebApplication1.Repository
{

    public class BooksRepository : IBooksRepository
    {
        private readonly BookContext _context;

        public BooksRepository(BookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _context.Database.EnsureCreated();
        }

        public IEnumerable<BookData> GetAll()
        {
            var result = _context.Books
                .Where(x => x.SaleInfo.Saleability == "FOR_SALE")
                .Include(a => a.VolumeInfo)
                .Include(a => a.VolumeInfo.ReadingModes)
                .Include(a => a.VolumeInfo.PanelizationSummary)
                .Include(a => a.VolumeInfo.ImageLinks)
                .Include(b => b.SaleInfo)
                .Include(b => b.SaleInfo.RetailPrice)
                .Include(b => b.SaleInfo.ListPrice)
                .Include(c => c.AccessInfo)
                .Include(c => c.AccessInfo.Epub)
                .Include(c => c.AccessInfo.Pdf)
                .Include(c => c.AccessInfo)
                .Include(d => d.SearchInfo)
                .OrderByDescending(a => a.VolumeInfo.RatingsCount)
                .ToList();

            if (result.Count == 0)
            {
                return null;
            }
            return result;
        }

        public async Task<BookData> CreateBook([FromBody] BookData bookData)
        {
            if (await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.BookId == bookData.BookId) != null)
            {
                return null;
            }

            var result = await _context.Books.AddAsync(bookData);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BookData> GetBookById([FromRoute] Guid BookId)
        {
            var result = _context.Books
                   .Where(b => b.BookId == BookId)
                   .Include(a => a.VolumeInfo)
                   .ThenInclude(a => a.ReadingModes)
                   .Include(a => a.VolumeInfo)
                   .ThenInclude(a => a.PanelizationSummary)
                   .Include(a => a.VolumeInfo)
                   .ThenInclude(a => a.ImageLinks)

                   .Include(b => b.SaleInfo)
                   .ThenInclude(b => b.RetailPrice)
                   .Include(b => b.SaleInfo)
                   .ThenInclude(b => b.ListPrice)

                   .Include(c => c.AccessInfo)
                   .ThenInclude(c => c.Epub)
                   .Include(c => c.AccessInfo)
                   .ThenInclude(c => c.Pdf)

                   .Include(d => d.SearchInfo)
                   .FirstOrDefault();

            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<BookData> UpdateBookById([FromRoute] Guid BookId, [FromBody] BookData bookData)
        {
            if (BookId != bookData.BookId)
            {
                return null;
            }

            if (await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.BookId == bookData.BookId) == null)
            {
                return null;
            }

            var result = _context.Books.Update(bookData);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BookData> DeleteBookById([FromRoute] Guid BookId)
        {
            var result = _context.Books
                .Where(b => b.BookId == BookId)
                .FirstOrDefault();

            if (result == null)
            {
                return null;
            }

            try
            {
                _context.Books.Remove(result);
                _context.SaveChanges();
            }
            catch
            {
                return null;
            }

            return result;
        }
    }
}
