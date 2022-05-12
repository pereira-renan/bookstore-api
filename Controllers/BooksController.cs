using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Diagnostics;
using bookstore_api.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Produces("application/json")]

    public class BooksController : ControllerBase
    {
        public readonly IBooksBusiness _booksBusiness;
        public BooksController(IBooksBusiness booksBusiness)
        {
            _booksBusiness = booksBusiness;
        }

        [HttpGet]
        public IEnumerable<BookData> GetAll()
        {
            return _booksBusiness.GetAll();
        }

        [HttpPost]
        [Route("{BookId}")]
        public async Task<BookData> CreateBook([FromBody] BookData bookData)
        {
            return await _booksBusiness.CreateBook(bookData);
        }

        [HttpGet]
        [Route("{BookId}")]
        public async Task<BookData> GetBookById([FromRoute] Guid BookId)
        {
            return await _booksBusiness.GetBookById(BookId);
        }

        [HttpPut]
        [Route("{BookId}")]
        public async Task<BookData> UpdateBookById([FromRoute] Guid BookId, [FromBody] BookData bookData)
        {
            return await _booksBusiness.UpdateBookById(BookId, bookData);
        }

        [HttpDelete]
        [Route("{BookId}")]
        public async Task<BookData> DeleteBookById([FromRoute] Guid BookId)
        {
            return await _booksBusiness.DeleteBookById(BookId);
        }
    }
}
