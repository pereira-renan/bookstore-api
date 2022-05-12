using bookstore_api.Context;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using WebApplication1.Models;

namespace WebApplication1.Repository
{

    public class DatabaseRepository : IDatabaseRepository
    {
        public HttpClient ApiClient { get; private set; }

        private readonly BookContext _context;

        public DatabaseRepository(BookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _context.Database.EnsureCreated();
        }


        public async Task<BookDataResponse> InsertAll()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            List<BookData> books = new List<BookData>();
            List<string> searchTerms = new List<string> {
                "lord of the rings", "harry potter", "programming", "survival", "nature", "photography" , "design" , "design", "physics"};

            BookDataResponse result = new BookDataResponse();

            try
            {
                BookDataResponse data = new BookDataResponse();
                for (int i = 0; i < searchTerms.Count; i++)
                {
                    for (int b = 0; b < 5; b++)
                    {
                        var text = "https://www.googleapis.com/books/v1/volumes?q=" + searchTerms[i] + "&langRestrict=en&startIndex=" + 40 * b + "&maxResults=" + 40 * (b + 1) + "";
                        HttpResponseMessage response = await new HttpClient().GetAsync("https://www.googleapis.com/books/v1/volumes?q=" + searchTerms[i] + "&langRestrict=en&startIndex=" + 40 * b + "&maxResults=1");
                        if (response.IsSuccessStatusCode)
                        {
                            data = await response.Content.ReadFromJsonAsync<BookDataResponse>();
                            books.AddRange(data.Results);

                            continue;
                        }
                        else
                        {
                            ApiClient.Dispose();
                            throw new Exception(response.ReasonPhrase);
                        }
                    }
                }

                var booksForSale = books;

                List<BookData> booksAdded = new List<BookData>();

                foreach (var book in booksForSale)
                {
                    var bookInDb = _context.Books.FirstOrDefault(x => x.Id == book.Id);
                    if (bookInDb == null)
                    {
                        _context.Books.Add(book);
                        booksAdded.Add(book);
                    }
                }

                _context.SaveChanges();

                result.operationType = "Adding books from google API to database";
                result.totalResult = booksAdded.Count;
                result.Results = booksAdded;
                ApiClient.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                ApiClient.Dispose();
                throw new Exception(ex.Message);
            }
        }
    }
}
